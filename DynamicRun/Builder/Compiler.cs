using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace DynamicRun.Builder
{
    internal class Compiler
    {
        public const string HeaderCodePath = @"C:\Code\DynamicRun\Sources\header.txt";

        public byte[] Compile(string filepath)
        {
            var headerCode = File.ReadAllText(HeaderCodePath);
            var sourceCode = headerCode + File.ReadAllText(filepath);

            using var peStream = new MemoryStream();
            var result = GenerateCode(sourceCode).Emit(peStream);

            if (!result.Success)
            {
                var failures = result.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error).ToList();

                var errorBuilder = new StringBuilder();
                failures.ForEach(diagnostic => errorBuilder.AppendLine($"{diagnostic.Id}: {diagnostic.GetMessage()}"));

                throw new InvalidOperationException($"Compilation failed with the following errors: {errorBuilder}");
            }

            peStream.Seek(0, SeekOrigin.Begin);

            return peStream.ToArray();
        }

        private static CSharpCompilation GenerateCode(string sourceCode)
        {
            var codeString = SourceText.From(sourceCode);
            var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp10);

            var parsedSyntaxTree = SyntaxFactory.ParseSyntaxTree(codeString, options);

            var references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location), 
                MetadataReference.CreateFromFile(typeof(Opportunity).Assembly.Location)
            };
            
            Assembly.GetEntryAssembly()?.GetReferencedAssemblies().ToList()
                .ForEach(a => references.Add(MetadataReference.CreateFromFile(Assembly.Load(a).Location)));

            return CSharpCompilation.Create("matching.dll",
                new[] { parsedSyntaxTree }, 
                references: references, 
                options: new CSharpCompilationOptions(OutputKind.ConsoleApplication, 
                    optimizationLevel: OptimizationLevel.Release,
                    assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
        }
    }
}