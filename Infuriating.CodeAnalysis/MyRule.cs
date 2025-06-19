using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;

namespace Infuriating.CodeAnalysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MyRule : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor MyRuleDescriptor = new DiagnosticDescriptor(
            "IMR0001",
            title: "My Rule Title",
            messageFormat: "Message: {0}",
            category: "Unknown",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => new ImmutableArray<DiagnosticDescriptor> { MyRuleDescriptor };

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeProperty, SyntaxKind.PropertyDeclaration);
        }

        private static void AnalyzeProperty(SyntaxNodeAnalysisContext context)
        {
            var propertySyntax = (PropertyDeclarationSyntax)context.Node;

            var diagnostic = Diagnostic.Create(MyRuleDescriptor, propertySyntax.GetLocation(), "this is a test");
            context.ReportDiagnostic(diagnostic);
        }
    }
}
