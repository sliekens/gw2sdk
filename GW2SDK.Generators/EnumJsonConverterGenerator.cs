using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace GW2SDK.Generators;

[Generator]
public class EnumJsonConverterGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new EnumSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not EnumSyntaxReceiver receiver)
        {
            return;
        }

        var enumTypes = new List<string>();

        foreach (var enumDeclaration in receiver.EnumDeclarations)
        {
            var model = context.Compilation.GetSemanticModel(enumDeclaration.SyntaxTree);
            var enumSymbol = model.GetDeclaredSymbol(enumDeclaration);
            if (enumSymbol is not INamedTypeSymbol namedTypeSymbol)
            {
                continue;
            }

            if (namedTypeSymbol.DeclaredAccessibility != Accessibility.Public)
            {
                continue;
            }

            var namespaceName = enumSymbol.ContainingNamespace.ToDisplayString();
            if (!namespaceName.StartsWith("GuildWars2"))
            {
                continue;
            }

            var enumName = enumSymbol.Name;
            enumTypes.Add($"{namespaceName}.{enumName}");
            var source = GenerateEnumJsonConverter(enumName, namespaceName, namedTypeSymbol);
            context.AddSource(
                $"{namespaceName}.{enumName}JsonConverter.g.cs",
                SourceText.From(source, Encoding.UTF8)
            );
        }

        var factorySource = GenerateExtensibleEnumJsonConverterFactory(enumTypes);
        context.AddSource(
            "GuildWars2.ExtensibleEnumJsonConverterFactory.g.cs",
            SourceText.From(factorySource, Encoding.UTF8)
        );
    }

    private string GenerateEnumJsonConverter(
        string enumName,
        string namespaceName,
        INamedTypeSymbol enumSymbol
    )
    {
        var enumValues = enumSymbol.GetMembers()
            .Where(m => m.Kind == SymbolKind.Field)
            .OfType<IFieldSymbol>()
            .Where(f => f.ConstantValue != null)
            .Select(f => f.Name)
            .ToList();

        var readCases = new StringBuilder();
        foreach (var value in enumValues)
        {
            readCases.AppendLine(
                $$"""
                  
                         if (reader.ValueTextEquals(nameof({{enumName}}.{{value}})))
                         {
                             return {{enumName}}.{{value}};
                         }
                  """
            );
        }

        var writeCases = new StringBuilder();
        foreach (var value in enumValues)
        {
            writeCases.AppendLine(
                $"""
                            {enumName}.{value} => nameof({enumName}.{value}),
                 """
            );
        }

        return $$"""
                 #nullable enable

                 using System;
                 using System.Text.Json;
                 using System.Text.Json.Serialization;

                 namespace {{namespaceName}};

                 internal sealed class {{enumName}}JsonConverter : JsonConverter<{{enumName}}>
                 {
                     public override {{enumName}} Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                     {
                         {{readCases}}
                         throw new JsonException();
                     }
                 
                     public override void Write(Utf8JsonWriter writer, {{enumName}} value, JsonSerializerOptions options)
                     {
                         writer.WriteStringValue(value switch
                         {
                 {{writeCases}}
                             _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
                         });
                     }
                 }

                 """;
    }

    private string GenerateExtensibleEnumJsonConverterFactory(List<string> enumTypes)
    {
        var cases = new StringBuilder();
        foreach (var enumType in enumTypes)
        {
            cases.AppendLine(
                $$"""
                          if (enumType == typeof({{enumType}}))
                          {
                              return new ExtensibleEnumJsonConverter<{{enumType}}>();
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

                 internal sealed class ExtensibleEnumJsonConverterFactory : JsonConverterFactory
                 {
                     public override bool CanConvert(Type typeToConvert)
                     {
                         if (!typeToConvert.IsGenericType)
                         {
                             return false;
                         }
                 
                         return typeToConvert.GetGenericTypeDefinition() == typeof(Extensible<>);
                     }
                 
                     public override JsonConverter? CreateConverter(
                         Type typeToConvert,
                         JsonSerializerOptions options
                     )
                     {
                         var enumType = typeToConvert.GetGenericArguments()[0];
                 {{cases}}
                         return null;
                     }
                 }
                 """;
    }

    private class EnumSyntaxReceiver : ISyntaxReceiver
    {
        public List<EnumDeclarationSyntax> EnumDeclarations { get; } = [];

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is EnumDeclarationSyntax enumDeclaration)
            {
                EnumDeclarations.Add(enumDeclaration);
            }
        }
    }
}
