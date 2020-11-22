using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

[assembly: InternalsVisibleTo("SettingsGenerator.Tests")]

namespace SettingsGenerator {
    internal static class JsonUtils {
        public static async Task<string> SchemaToClass(string schema) {
            var schemaObject = await JsonSchema.FromJsonAsync(schema);
            var generator = new CSharpGenerator(schemaObject, new CSharpGeneratorSettings {
                Namespace = "SettingsGenerator",
                GenerateDataAnnotations = false,
                GenerateJsonMethods = false,
                ClassStyle = CSharpClassStyle.Poco,
            });
            return generator.GenerateFile("AppSettings");
        }

        public static string JsonToSchema(string json) {
            var schema = NJsonSchema.JsonSchema.FromSampleJson(json);
            return schema.ToJson();
        }
    }
}