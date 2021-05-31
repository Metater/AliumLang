using System;
using System.Diagnostics;
using System.IO;
using AliumInterpreter;

namespace AliumInterpreterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string[] lines = File.ReadAllLines(@"C:\Users\Connor\Desktop\testing.alium");
            string source = "";
            foreach (string line in lines)
                source += line;

            Stopwatch s = new Stopwatch();
            s.Start();
            int result = 0;
            Interpreter interpreter = new Interpreter(source);
            result = interpreter.Interpret();
            s.Stop();
            Console.WriteLine($"Result: {result}, Time: {s.ElapsedTicks/10000f}");
        }
    }
}
