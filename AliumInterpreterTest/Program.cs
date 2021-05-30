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

            string source = "1+1";

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
