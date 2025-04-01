using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace NBomber.Converter.Tests
{
    public static class ConverterTestHelper
    {
        public static void DeleteReportsIfExists()
        {
            if (Directory.Exists("reports"))
                Directory.Delete("reports", true);
        }

        public static string RemoveSpacesAndBackslashSymbols(string input)
        {
            input = input.Replace(" ", String.Empty);
            input = input.Replace("\n", String.Empty);
            input = input.Replace("\r", String.Empty);

            return input;
        }

        public static void RunCSharpCode(string code, string[] assemblies)
        {
            // Create a syntax tree from the code
            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            // Get the current app domain assemblies (this will include the core .NET libraries)
            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .Select(a => a.Location)
                .Where(l => l != null)
                .Distinct()
                .Select(r => MetadataReference.CreateFromFile(r))
                .ToList();

            foreach (var assembly in assemblies)
                references.Add(MetadataReference.CreateFromFile(assembly));

            // Create the C# compilation
            var compilation = CSharpCompilation.Create(
                "DynamicAssembly",  // Name of the assembly
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.ConsoleApplication));

            // Emit the compiled code to a memory stream
            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);

                if (result.Success)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    var assembly = Assembly.Load(ms.ToArray());

                    // Find the entry point (Main method) and invoke it
                    var entryPoint = assembly.EntryPoint;
                    if (entryPoint != null)
                    {
                        entryPoint.Invoke(null, null);
                    }
                }
                else
                {
                    foreach (var diagnostic in result.Diagnostics)
                    {
                        Console.WriteLine(diagnostic.ToString());
                    }
                }
            }
        }
    }
}
