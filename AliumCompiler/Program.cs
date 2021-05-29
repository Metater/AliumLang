using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CSharp;
using System.IO;
using System.Collections.Generic;

namespace AliumCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            string output = File.ReadAllText(Directory.GetCurrentDirectory() + @"\output.conf");

            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = true,
                OutputAssembly = output
            };

            string[] sourcePaths = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\sources.conf");
            List<string> sources = new List<string>();
            foreach(string sourcePath in sourcePaths)
            {
                string source = File.ReadAllText(sourcePath);
                sources.Add(source);
            }
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, sources.ToArray());
        }
    }
}
