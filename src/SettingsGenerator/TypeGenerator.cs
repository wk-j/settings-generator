using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;

namespace SettingsGenerator {
    [Generator]
    internal class TypeGenerator : ISourceGenerator {
        private const string settingsAttributeText = @"
using System;
[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class AppSettingsAttribute : Attribute {
    public string FileName { set;get;}
}
";

        private AdditionalText GetJsonSettings(GeneratorExecutionContext context, string fileName) =>
            context.AdditionalFiles
                .FirstOrDefault(x => x.Path.EndsWith(fileName));

        private List<INamedTypeSymbol> GetClassSymbals(GeneratorExecutionContext context, SyntaxReceiver receiver) {
            var options = (context.Compilation as CSharpCompilation)
                .SyntaxTrees[0].Options as CSharpParseOptions;

            var tree = CSharpSyntaxTree.ParseText(SourceText.From(settingsAttributeText, Encoding.UTF8), options);
            var compilation = context.Compilation.AddSyntaxTrees(tree);
            var attributeSymbol = compilation.GetTypeByMetadataName("AppSettingsAttribute");
            var classSymbals = new List<INamedTypeSymbol>();

            foreach (var item in receiver.CandidateClasses) {
                var model = compilation.GetSemanticModel(item.SyntaxTree);
                var classSymbal = model.GetDeclaredSymbol(item);
                if (classSymbal.GetAttributes().Any(x => x.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default))) {
                    classSymbals.Add(classSymbal);
                }
            }
            return classSymbals;
        }

        private SourceText CreateAppSettingsSource(INamedTypeSymbol classSymbol, GeneratorExecutionContext context) {
            var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
            var att = classSymbol.GetAttributes().FirstOrDefault(x => x.AttributeClass.Name == "AppSettingsAttribute");
            var fileNameAttribute = att.NamedArguments[0];
            var fileNameValue = fileNameAttribute.Value.Value as string;

            var settingFile = GetJsonSettings(context, fileNameValue);
            var json = settingFile.GetText().ToString();
            var classDefinition = new Json2Class.ClassGenerator().JsonToClasses(json, new Json2Class.ClassOptions {
                Namespace = namespaceName,
                ClassName = classSymbol.Name,
                Partial = true,
                ReferencClassSuffix = "Options"
            });

            return SourceText.From(classDefinition, Encoding.UTF8);
        }

        public void Execute(GeneratorExecutionContext context) {
            InjectAttribute(context);

            if (context.SyntaxReceiver is not SyntaxReceiver receiver) {
                return;
            }

            var symbols = GetClassSymbals(context, receiver);
            foreach (var item in symbols) {
                var source = CreateAppSettingsSource(item, context);
                context.AddSource(item.Name + ".AppSettings.g.cs", source);
            }
        }

        public void Initialize(GeneratorInitializationContext context) {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        private void InjectAttribute(GeneratorExecutionContext context) {
            context.AddSource("AppSettings.Attribute.g.cs", SourceText.From(settingsAttributeText, Encoding.UTF8));
        }
    }
}