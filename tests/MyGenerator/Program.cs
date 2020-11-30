using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace MyGenerator {
    [Generator]
    public class HelloWorldGenerator : ISourceGenerator {
        public void Execute(GeneratorExecutionContext context) {

            var template = @"
using System;
namespace MyGenerator {
    public static class HelloWorld {
        public static void SayHello() {
            Console.WriteLine(""Hello from generate code"");
            {_generate_}
        }
    }
}
            ";

            var generated = new StringBuilder();
            var syntaxTree = context.Compilation.SyntaxTrees;
            foreach (var tree in syntaxTree) {
                generated.Append($@"Console.WriteLine(@"" - {tree.FilePath}"");");
            }

            var text = generated.ToString();
            var source = template.Replace("{_generate_}", text);

            context.AddSource("HelloGenerator", SourceText.From(source, Encoding.UTF8));

        }

        public void Initialize(GeneratorInitializationContext context) {

        }
    }

    class Program {
        static void Main(string[] args) {

        }
    }
}