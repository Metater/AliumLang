using System;
using System.IO;

namespace AliumTranspiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string source = File.ReadAllText(@"C:\Users\Connor\Desktop\AliumLang\source.alium");
            SourceLiner sourceLiner = new SourceLiner(source);
        }
    }
}
