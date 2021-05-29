using System;
using System.Diagnostics;
using AliumInterpreter;

namespace AliumInterpreterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string source = "321";
            Stopwatch s = new Stopwatch();
            s.Start();
            Interpreter interpreter = new Interpreter(source);
            int result = interpreter.Interpret();
            s.Stop();
            Console.WriteLine($"Result: {result}, Time: {s.ElapsedTicks/10000f}");
        }
    }
}
