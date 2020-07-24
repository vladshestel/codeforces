using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyModel;

namespace runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var handler = ConstructHandler();

            handler.Invoke(args);
        }

        private static RootCommand ConstructHandler()
        {
            var configuration = new RootCommand
            {
                new Option<FileInfo>
                (
                    alias: "--issue",
                    description: "Path to an issue file"
                )
                {
                    IsRequired = true
                }.ExistingOnly()
            };

            configuration.Handler = CommandHandler.Create<FileInfo>(
                Handle
            );
            
            return configuration;
        }

        private static void Handle(FileInfo issue)
        {
            var content = File.ReadAllText(issue.FullName);
            var syntaxTree = CSharpSyntaxTree.ParseText(content);
            var root = syntaxTree.GetCompilationUnitRoot();

            var trustedAssembliesPaths = (AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") as string)
                .Split(Path.PathSeparator);

            var requiredNamespaces = new List<string>
            {
                "netstandard",
                "mscorlib",
                "System",
                "System.Runtime",
                "System.Private.CoreLib",
                "System.Console",
                //"System.Core",
            };
            
            foreach (var usingStatement in root.Usings)
            {
                if (!requiredNamespaces.Contains(usingStatement.Name.ToString()))
                {
                    requiredNamespaces.Add(usingStatement.Name.ToString());
                }
            }

            var assemblyPaths = new List<string>();
            foreach (var requiredAssembly in requiredNamespaces)
            {
                
                var path = trustedAssembliesPaths.First(
                    fullPath => fullPath.EndsWith(requiredAssembly + ".dll")
                );

                if (path != null)
                {
                    assemblyPaths.Add(path);
                }
            }

            var references = assemblyPaths
                .Select(path => MetadataReference.CreateFromFile(path))
                .ToArray();
            /*
            var references = DependencyContext.Default.CompileLibraries
                //.SelectMany(library => library.ResolveReferencePaths())
                .First(library => library.Name == "Microsoft.NETCore.App")
                .ResolveReferencePaths()
                .Select(assembly => MetadataReference.CreateFromFile(assembly))
                .ToArray();
            */
            /*
            var basePath = Path.GetDirectoryName(typeof(object).Assembly.Location);
            var references = new MetadataReference[]
            {
                //AppContext.
                MetadataReference.CreateFromFile(Path.Combine(basePath, "netstandard.dll")), 
                MetadataReference.CreateFromFile(Path.Combine(basePath, "System.dll")),
                MetadataReference.CreateFromFile(Path.Combine(basePath, "System.Runtime.dll"))
                //MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                //MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                //MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location),
                //MetadataReference.CreateFromFile(typeof(Stream).Assembly.Location),
                //MetadataReference.CreateFromFile(Assembly.Load("netstandard").Location)
            };
            */

            var compilation = CSharpCompilation.Create(
                issue.Name,
                new[] {syntaxTree},
                references,
                options: new CSharpCompilationOptions(
                    OutputKind.ConsoleApplication,
                    optimizationLevel: OptimizationLevel.Release,
                    assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default
                )
            );

            var bytes = LoadCodeToMemory(compilation);

            using (var memory = new MemoryStream(bytes))
            {
                var assemblyLoadContext = new AssemblyLoadContext(null, true);

                var assembly = assemblyLoadContext.LoadFromStream(memory);
                var entry = assembly.EntryPoint;
                var invokeResult = entry.Invoke(null, new object[]{ new string[]{} });
                
                assemblyLoadContext.Unload();
            }
        }

        private static byte[] LoadCodeToMemory(CSharpCompilation compilation)
        {
            using (var memory = new MemoryStream())
            {
                var result = compilation.Emit(memory);

                if (!result.Success)
                {
                    Console.WriteLine("Compilation done with error.");
                    var failures = result.Diagnostics.Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (var diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }

                    return null;
                }
                
                Console.WriteLine("Compilation done without any error.");
                
                memory.Seek(0, SeekOrigin.Begin);

                return memory.ToArray();
            }
        }
    }
}
