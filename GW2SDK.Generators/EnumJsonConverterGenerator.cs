using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text;

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

        var enums = receiver.EnumDeclarations;

        var registrations = new StringBuilder();
        foreach (var enumDeclaration in enums)
        {
            var enumName = enumDeclaration.Identifier.Text;
            var namespaceName = GetNamespace(enumDeclaration);
            string fullyQualifiedEnumName;
            if (string.IsNullOrEmpty(namespaceName))
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor(
                    "GWSDK001", "Missing Namespace", $"Enum '{enumName}' is missing a namespace.", "EnumJsonConverterGenerator", DiagnosticSeverity.Warning, true), enumDeclaration.GetLocation()));
                fullyQualifiedEnumName = $"global::{enumName}";
            }
            else
            {
                fullyQualifiedEnumName = $"{namespaceName}.{enumName}";
            }

            if (fullyQualifiedEnumName.StartsWith("GuildWars2", StringComparison.Ordinal))
            {
                registrations.AppendLine($"        Register<{fullyQualifiedEnumName}>();");
            }
        }

        var sourceBuilder = new StringBuilder($$"""
            namespace GuildWars2;
            
            internal partial class ExtensibleEnumJsonConverterFactory
            {
                private static partial void RegisterEnums()
                {
            {{registrations}}
                    static void Register<TEnum>() where TEnum : struct, Enum
                    {
                        Converters[typeof(TEnum)] = new ExtensibleEnumJsonConverter<TEnum>();
                    }
                }
            }
            """);

        context.AddSource("EnumJsonConverters.g.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }

    private string GetNamespace(EnumDeclarationSyntax enumDeclaration)
    {
        var namespaceDeclaration = enumDeclaration.Ancestors().OfType<FileScopedNamespaceDeclarationSyntax>().FirstOrDefault();
        return namespaceDeclaration?.Name.ToString() ?? string.Empty;
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
