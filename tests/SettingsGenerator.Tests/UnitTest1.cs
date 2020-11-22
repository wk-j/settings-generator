using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SettingsGenerator.Tests {
    public class UnitTest1 {
        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output) {
            _output = output;
        }

        private string GenerateSchema() {
            var json = @"{
                ""ConnectionString"": ""Host=localhost"",
                ""Alfresco"": {
                    ""Host"": ""http://localhost:8080"",
                    ""User"" : ""admin"",
                    ""Password"": ""admin""
                }
            }";

            var schema = JsonUtils.JsonToSchema(json);
            return schema;
        }

        [Fact]
        public void JsonToSchemaTest() {
            var schema = GenerateSchema();
            _output.WriteLine(schema);
        }

        [Fact]
        public async Task SchemaToClass() {
            var schema = GenerateSchema();
            var cls = await JsonUtils.SchemaToClass(schema);
            _output.WriteLine(cls);
        }
    }
}
