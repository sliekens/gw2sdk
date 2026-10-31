using System.Diagnostics.CodeAnalysis;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace GuildWars2.Generators;

/// <summary>Generates <c>CreateKnownConverter</c> for the JSON converter factories of the immutable value collections.</summary>
/// <remarks>The factories create a closed <c>Converter&lt;T&gt;</c> for the collection's element type. With
/// <c>MakeGenericType</c>, Native AOT can only do that for reference types (shared canonical code): a value-type
/// instantiation must have been compiled ahead of time. The generated code creates the converters for the value-type
/// elements (and dictionary keys/values) used anywhere in the SDK's own types, plus a short list of common types, with
/// <c>new</c>, so Native AOT compiles them. It also roots one reference-type instantiation of each converter, which
/// provides the canonical code that <c>MakeGenericType</c> uses for all other reference types (and, for dictionaries,
/// for reference-type values with a rooted value-type key). See https://github.com/sliekens/gw2sdk/issues/521.
/// </remarks>
[Generator]
[SuppressMessage("Style", "IDE0028", Justification = "Collection expressions would drop the comparers of these collections.")]
public sealed class ImmutableValueCollectionConverterGenerator : IIncrementalGenerator
{
    private const string Namespace = "GuildWars2.Collections";

    private static readonly (string Collection, string Factory)[] ListLikeFactories =
    [
        ("IImmutableValueList`1", "ImmutableValueListJsonConverterFactory"),
        ("ImmutableValueList`1", "ImmutableValueListJsonConverter"),
        ("ImmutableValueArray`1", "ImmutableValueArrayJsonConverterFactory"),
        ("IImmutableValueSet`1", "ImmutableValueSetJsonConverterFactory"),
        ("ImmutableValueSet`1", "ImmutableValueSetJsonConverter")
    ];

    private static readonly (string Collection, string Factory)[] DictionaryFactories =
    [
        ("IImmutableValueDictionary`2", "ImmutableValueDictionaryJsonConverterFactory"),
        ("ImmutableValueDictionary`2", "ImmutableValueDictionaryJsonConverter")
    ];

    // Value types that consumers are likely to use as elements, besides the ones the SDK uses itself. Every value-type
    // instantiation costs roughly 190 KB in a Native AOT binary (the collection types and their interface methods are
    // compiled for it), so this list is deliberately short; rooting every SDK enum and Extensible<TEnum> would add more
    // than 20 MB. Keep it in sync with the message in ImmutableValueCollectionConverters.NotPrecompiled.
    private static readonly string[] PrimitiveElements =
    [
        "bool", "int", "long", "double", "decimal", "global::System.Guid", "global::System.DateTime",
        "global::System.DateTimeOffset", "global::System.TimeSpan", "global::GuildWars2.Coin"
    ];

    private static readonly string[] PrimitiveKeys = ["int", "string"];

    private static readonly string[] PrimitiveValues = ["bool", "int", "long", "double", "decimal", "global::GuildWars2.Coin"];

    // Rooting one reference-type instantiation provides the shared canonical code for every reference type
    private const string ReferenceTemplate = "string";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(context.CompilationProvider, static (spc, compilation) => Execute(compilation, spc));
    }

    private static void Execute(Compilation compilation, SourceProductionContext context)
    {
        Dictionary<INamedTypeSymbol, bool> definitions = new(SymbolEqualityComparer.Default);
        foreach ((string collection, _) in ListLikeFactories)
        {
            if (compilation.GetTypeByMetadataName($"{Namespace}.{collection}") is { } definition)
            {
                definitions[definition] = false;
            }
        }

        foreach ((string collection, _) in DictionaryFactories)
        {
            if (compilation.GetTypeByMetadataName($"{Namespace}.{collection}") is { } definition)
            {
                definitions[definition] = true;
            }
        }

        if (definitions.Count != ListLikeFactories.Length + DictionaryFactories.Length)
        {
            // Not the SDK assembly
            return;
        }

        SortedSet<string> elements = new(StringComparer.Ordinal);
        SortedSet<string> pairs = new(StringComparer.Ordinal);
        HashSet<ITypeSymbol> visited = new(SymbolEqualityComparer.IncludeNullability);

        void Visit(ITypeSymbol? type)
        {
            if (type is null || !visited.Add(type))
            {
                return;
            }

            switch (type)
            {
                case IArrayTypeSymbol array:
                    Visit(array.ElementType);
                    break;
                case INamedTypeSymbol { IsGenericType: true } named:
                    if (definitions.TryGetValue(named.OriginalDefinition, out bool isDictionary) && IsUsable(named))
                    {
                        if (isDictionary)
                        {
                            if (named.TypeArguments[0].IsValueType || named.TypeArguments[1].IsValueType)
                            {
                                pairs.Add($"{Display(named.TypeArguments[0])}, {Display(named.TypeArguments[1])}");
                            }
                        }
                        else if (named.TypeArguments[0].IsValueType)
                        {
                            elements.Add(Display(named.TypeArguments[0]));
                        }
                    }

                    foreach (ITypeSymbol argument in named.TypeArguments)
                    {
                        Visit(argument);
                    }

                    break;
                default:
                    break;
            }
        }

        foreach (INamedTypeSymbol type in AllTypes(compilation.Assembly.GlobalNamespace))
        {
            foreach (ISymbol member in type.GetMembers())
            {
                switch (member)
                {
                    case IPropertySymbol property:
                        Visit(property.Type);
                        break;
                    case IFieldSymbol field:
                        Visit(field.Type);
                        break;
                    case IMethodSymbol method:
                        Visit(method.ReturnType);
                        foreach (IParameterSymbol parameter in method.Parameters)
                        {
                            Visit(parameter.Type);
                        }

                        break;
                    default:
                        break;
                }
            }
        }

        foreach (string primitive in PrimitiveElements)
        {
            elements.Add(primitive);
        }

        elements.Add(ReferenceTemplate);

        foreach (string key in PrimitiveKeys)
        {
            foreach (string value in PrimitiveValues)
            {
                pairs.Add($"{key}, {value}");
            }

            pairs.Add($"{key}, {ReferenceTemplate}");
        }

        foreach ((_, string factory) in ListLikeFactories)
        {
            context.AddSource($"{Namespace}.{factory}.g.cs", SourceText.From(Generate(factory, elements, "Type elementType", "elementType == typeof({0})", "{0}"), Encoding.UTF8));
        }

        foreach ((_, string factory) in DictionaryFactories)
        {
            context.AddSource($"{Namespace}.{factory}.g.cs", SourceText.From(GenerateDictionary(factory, pairs), Encoding.UTF8));
        }
    }

    private static string Generate(string factory, IEnumerable<string> elements, string parameters, string condition, string arguments)
    {
        StringBuilder cases = new();
        foreach (string element in elements)
        {
            cases.AppendLine($"        if ({string.Format(condition, element)})");
            cases.AppendLine("        {");
            cases.AppendLine($"            return new Converter<{string.Format(arguments, element)}>();");
            cases.AppendLine("        }");
            cases.AppendLine();
        }

        return $$"""
                 // <auto-generated/>
                 #nullable enable

                 using System;
                 using System.Text.Json.Serialization;

                 namespace {{Namespace}};

                 partial class {{factory}}
                 {
                     private static partial JsonConverter? CreateKnownConverter({{parameters}})
                     {
                 {{cases}}        return null;
                     }
                 }

                 """;
    }

    private static string GenerateDictionary(string factory, IEnumerable<string> pairs)
    {
        StringBuilder cases = new();
        foreach (string pair in pairs)
        {
            string[] types = pair.Split([", "], 2, StringSplitOptions.None);
            cases.AppendLine($"        if (keyType == typeof({types[0]}) && valueType == typeof({types[1]}))");
            cases.AppendLine("        {");
            cases.AppendLine($"            return new Converter<{pair}>();");
            cases.AppendLine("        }");
            cases.AppendLine();
        }

        return $$"""
                 // <auto-generated/>
                 #nullable enable

                 using System;
                 using System.Text.Json.Serialization;

                 namespace {{Namespace}};

                 partial class {{factory}}
                 {
                     private static partial JsonConverter? CreateKnownConverter(Type keyType, Type valueType)
                     {
                 {{cases}}        return null;
                     }
                 }

                 """;
    }

    private static IEnumerable<INamedTypeSymbol> AllTypes(INamespaceSymbol root)
    {
        Stack<INamespaceOrTypeSymbol> pending = new();
        pending.Push(root);
        while (pending.Count > 0)
        {
            INamespaceOrTypeSymbol current = pending.Pop();
            if (current is INamedTypeSymbol type)
            {
                yield return type;
            }

            foreach (ISymbol member in current.GetMembers())
            {
                if (member is INamespaceOrTypeSymbol child)
                {
                    pending.Push(child);
                }
            }
        }
    }

    // Closed over types that generated code in the SDK assembly can name
    private static bool IsUsable(INamedTypeSymbol type)
    {
        foreach (ITypeSymbol argument in type.TypeArguments)
        {
            if (!IsNameable(argument))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsNameable(ITypeSymbol type)
    {
        switch (type)
        {
            case ITypeParameterSymbol:
            case IErrorTypeSymbol:
                return false;
            case IArrayTypeSymbol array:
                return IsNameable(array.ElementType);
            case INamedTypeSymbol named:
                for (INamedTypeSymbol? current = named; current is not null; current = current.ContainingType)
                {
                    if (current.DeclaredAccessibility is Accessibility.Private or Accessibility.Protected or Accessibility.ProtectedAndInternal)
                    {
                        return false;
                    }
                }

                foreach (ITypeSymbol argument in named.TypeArguments)
                {
                    if (!IsNameable(argument))
                    {
                        return false;
                    }
                }

                return true;
            default:
                return false;
        }
    }

    private static string Display(ITypeSymbol type)
    {
        ITypeSymbol withoutAnnotation = type.IsReferenceType ? type.WithNullableAnnotation(NullableAnnotation.NotAnnotated) : type;
        return withoutAnnotation.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }
}
