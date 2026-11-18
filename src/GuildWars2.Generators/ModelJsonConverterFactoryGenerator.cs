using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace GuildWars2.Generators;

/// <summary>Generates <c>ModelJsonConverterFactory</c>, the public factory that constructs the SDK's internal JSON
/// converters. Consumer source generators can construct the factory; they cannot construct the internal converters.
/// See https://github.com/sliekens/gw2sdk/issues/520.</summary>
[Generator]
public sealed class ModelJsonConverterFactoryGenerator : IIncrementalGenerator
{
    private const string ConverterMetadataName = "System.Text.Json.Serialization.JsonConverter`1";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(context.CompilationProvider, static (production, compilation) => Execute(compilation, production));
    }

    private static void Execute(Compilation compilation, SourceProductionContext production)
    {
        if (compilation.GetTypeByMetadataName(ConverterMetadataName) is not INamedTypeSymbol jsonConverter)
        {
            return;
        }

        // A collection expression would drop the comparer.
#pragma warning disable IDE0028
        Dictionary<string, string> converters = new(StringComparer.Ordinal);
#pragma warning restore IDE0028
        foreach (SyntaxTree tree in compilation.SyntaxTrees)
        {
            SemanticModel model = compilation.GetSemanticModel(tree);
            foreach (ClassDeclarationSyntax declaration in tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                if (model.GetDeclaredSymbol(declaration) is not INamedTypeSymbol symbol)
                {
                    continue;
                }

                if (!TryGetConverter(symbol, jsonConverter, out string? modelType, out string? converterType))
                {
                    continue;
                }

                AddConverter(converters, modelType, converterType, declaration.GetLocation());
            }
        }

        foreach (SyntaxTree tree in compilation.SyntaxTrees)
        {
            SemanticModel model = compilation.GetSemanticModel(tree);
            foreach (EnumDeclarationSyntax declaration in tree.GetRoot().DescendantNodes().OfType<EnumDeclarationSyntax>())
            {
                if (model.GetDeclaredSymbol(declaration) is not INamedTypeSymbol symbol)
                {
                    continue;
                }

                if (symbol.DeclaredAccessibility != Accessibility.Public)
                {
                    continue;
                }

                string? namespaceName = symbol.ContainingNamespace?.ToDisplayString();
                if (namespaceName is null || !namespaceName.StartsWith("GuildWars2", StringComparison.Ordinal))
                {
                    continue;
                }

                string modelType = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                string converterType = $"global::{namespaceName}.{symbol.Name}JsonConverter";
                AddConverter(converters, modelType, converterType, declaration.GetLocation());
            }
        }

        production.AddSource(
            "GuildWars2.ModelJsonConverterFactory.g.cs",
            SourceText.From(Generate(converters), Encoding.UTF8)
        );
    }

    private static bool TryGetConverter(
        INamedTypeSymbol symbol,
        INamedTypeSymbol jsonConverter,
        out string modelType,
        out string converterType
    )
    {
        modelType = "";
        converterType = "";
        if (symbol.DeclaredAccessibility == Accessibility.Public
            || symbol.IsGenericType
            || symbol.ContainingType is not null
            || symbol.BaseType is not INamedTypeSymbol baseType
            || !SymbolEqualityComparer.Default.Equals(baseType.OriginalDefinition, jsonConverter))
        {
            return false;
        }

        string? namespaceName = symbol.ContainingNamespace?.ToDisplayString();
        if (namespaceName is null || !namespaceName.StartsWith("GuildWars2", StringComparison.Ordinal))
        {
            return false;
        }

        if (baseType.TypeArguments[0] is not INamedTypeSymbol { IsGenericType: false } converted)
        {
            return false;
        }

        modelType = converted.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        converterType = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        return true;
    }

    private static void AddConverter(
        Dictionary<string, string> converters,
        string modelType,
        string converterType,
        Location location
    )
    {
        if (converters.TryGetValue(modelType, out string? existing))
        {
            if (existing != converterType)
            {
                throw new InvalidOperationException($"'{existing}' and '{converterType}' both convert '{modelType}' ({location}).");
            }

            return;
        }

        converters.Add(modelType, converterType);
    }

    private static string Generate(Dictionary<string, string> converters)
    {
        StringBuilder canConvert = new();
        StringBuilder create = new();
        // RarityColor is a static palette. Its attribute uses the Rarity enum converter, so it is not a JsonConverter<RarityColor>.
        canConvert.AppendLine(
            """
                        if (typeToConvert == typeof(global::GuildWars2.Items.RarityColor))
                        {
                            return true;
                        }

            """
        );
        create.AppendLine(
            """
                        if (typeToConvert == typeof(global::GuildWars2.Items.RarityColor))
                        {
                            return new global::GuildWars2.Items.RarityJsonConverter();
                        }

            """
        );
        foreach (KeyValuePair<string, string> entry in converters.OrderBy(static pair => pair.Key, StringComparer.Ordinal))
        {
            canConvert.AppendLine(
                $$"""
                            if (typeToConvert == typeof({{entry.Key}}))
                            {
                                return true;
                            }

                """
            );
            create.AppendLine(
                $$"""
                            if (typeToConvert == typeof({{entry.Key}}))
                            {
                                return new {{entry.Value}}();
                            }

                """
            );
        }

        return $$"""
                 #nullable enable

                 using System;
                 using System.Text.Json;
                 using System.Text.Json.Serialization;

                 namespace GuildWars2;

                 public sealed partial class ModelJsonConverterFactory
                 {
                     private static readonly ExtensibleEnumJsonConverterFactory ExtensibleEnums = new();

                     /// <inheritdoc />
                     public override bool CanConvert(Type typeToConvert)
                     {
                         ThrowHelper.ThrowIfNull(typeToConvert);
                         if (ExtensibleEnums.CanConvert(typeToConvert))
                         {
                             return true;
                         }

                 {{canConvert}}
                         return false;
                     }

                     /// <inheritdoc />
                     public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
                     {
                         ThrowHelper.ThrowIfNull(typeToConvert);
                         if (ExtensibleEnums.CanConvert(typeToConvert))
                         {
                             return ExtensibleEnums.CreateConverter(typeToConvert, options)
                                 ?? throw new InvalidOperationException($"No JSON converter is registered for {typeToConvert}.");
                         }

                 {{create}}
                         throw new InvalidOperationException($"No JSON converter is registered for {typeToConvert}.");
                     }
                 }

                 """;
    }
}
