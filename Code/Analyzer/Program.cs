using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Linq;

namespace Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\niels\Desktop\WebApplication";
            var controllers = Directory.GetFiles(path, "*Controller.cs", SearchOption.AllDirectories);

            foreach (var controller in controllers)
            {
                var code = new StreamReader(controller).ReadToEnd();
                SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
                AnalyseController(tree);
            }

            Console.ReadLine();
        }

        private static void AnalyseController(SyntaxTree tree)
        {
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var publicmethods = root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(x => x.Modifiers.Any(SyntaxKind.PublicKeyword));

            foreach (var method in publicmethods)
            {
                var attributes = method.AttributeLists.SelectMany(x => x.Attributes);
                if (attributes.Any(x => x.Name.ToString() == "HttpPost"))
                {
                    //Validate that ValidateForgeryToken is also present.
                }

                var invocations = method.DescendantNodes().OfType<MemberAccessExpressionSyntax>().Where(x => x.Name.ToFullString() == "ModelState.IsValid").ToList();
                //If no invocations are found flag method for not validating modelstate.
            }

        }
    }
}
