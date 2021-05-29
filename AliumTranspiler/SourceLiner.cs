using System;
using System.Collections.Generic;
using System.Text;

namespace AliumTranspiler
{
    public class SourceLiner
    {
        public SourceLiner(string source)
        {
            List<string> clearedLines = new List<string>();
            string[] lines = source.Split(Environment.NewLine);
            foreach(string line in lines)
            {
                bool keep = false;
                foreach(char c in line)
                {
                    if (c != ' ')
                        keep = true;
                }
                if (keep) clearedLines.Add(line);
            }
            foreach(string cl in clearedLines)
            {
                Console.WriteLine(cl);
            }
        }
    }
}
