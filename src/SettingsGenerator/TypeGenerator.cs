using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SettingsGenerator {
    [Generator]
    internal class TypeGenerator : ISourceGenerator {
        public void Execute(GeneratorExecutionContext context) {
            var settings = "appsettings.json";
            var json = File.ReadAllText(settings);
            var schema = JsonUtils.JsonToSchema(json);
            var settingsSource = JsonUtils.SchemaToClass(schema).GetAwaiter().GetResult();

            var sourceText = SourceText.From(settingsSource, Encoding.UTF8);
            context.AddSource("AppSettings", sourceText);
        }

        public void Initialize(GeneratorInitializationContext context) {
        }
    }
}