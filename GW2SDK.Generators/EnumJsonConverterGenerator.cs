using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Text;

namespace GW2SDK.Generators;

[Generator]
public class EnumJsonConverterGenerator : ISourceGenerator
{
    public EnumJsonConverterGenerator()
    {
        Debugger.Launch();
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        Debugger.Launch();
        context.RegisterForSyntaxNotifications(() => new EnumSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        Debugger.Launch();
        if (context.SyntaxReceiver is not EnumSyntaxReceiver receiver)
            return;

        var enums = receiver.EnumDeclarations;

        var sourceBuilder = new StringBuilder("""
            using System;
            using System.Text.Json;
            using System.Text.Json.Serialization;

            namespace GuildWars2;

            public static class EnumJsonConverters
            {
                public static void RegisterConverters(JsonSerializerOptions options)
                {

            """);

        foreach (var enumDeclaration in enums)
        {
            var enumName = enumDeclaration.Identifier.Text;
            var namespaceName = GetNamespace(enumDeclaration);
            string fullyQualifiedEnumName;
            if (string.IsNullOrEmpty(namespaceName))
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor(
                    "ENUM001", "Missing Namespace", $"Enum '{enumName}' is missing a namespace.", "EnumJsonConverterGenerator", DiagnosticSeverity.Warning, true), enumDeclaration.GetLocation()));
                fullyQualifiedEnumName = $"global::{enumName}";
            }
            else
            {
                fullyQualifiedEnumName = $"{namespaceName}.{enumName}";
            }
            sourceBuilder.AppendLine($@"
            options.Converters.Add(new ExtensibleEnumJsonConverter<{fullyQualifiedEnumName}>());
");
        }

        sourceBuilder.Append("""

                }
            }

            public class ExtensibleEnumJsonConverter<TEnum> : JsonConverter<Extensible<TEnum>> where TEnum : struct, Enum
            {
                public override Extensible<TEnum> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                {
                    var name = reader.GetString();
                    return new Extensible<TEnum>(name!);
                }

                public override void Write(Utf8JsonWriter writer, Extensible<TEnum> value, JsonSerializerOptions options)
                {
                    writer.WriteStringValue(value.ToString());
                }
            }

            """);

        context.AddSource("EnumJsonConverters.g.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }

    private string GetNamespace(EnumDeclarationSyntax enumDeclaration)
    {
        var namespaceDeclaration = enumDeclaration.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
        return namespaceDeclaration?.Name.ToString() ?? string.Empty;
    }

    private class EnumSyntaxReceiver : ISyntaxReceiver
    {
        public List<EnumDeclarationSyntax> EnumDeclarations { get; } = new List<EnumDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is EnumDeclarationSyntax enumDeclaration)
            {
                EnumDeclarations.Add(enumDeclaration);
            }
        }
    }
}
