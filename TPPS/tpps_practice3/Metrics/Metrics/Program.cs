using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics
{
    class Program
    {
        static void Main(string[] args)
        {
            MetricsAnalyst ma = new MetricsAnalyst();
            int fileCounter = ma.getSentencesFromDirectory(@"C:\Users\AUL\Documents\locExampleDir\", ".cs");
            Console.WriteLine("Amount of files to handle: " + fileCounter);
            int loc = ma.getLinesOfCode();
            Console.WriteLine("LOC: " + loc);
            string codeFilePath = @"C:\Users\AUL\Documents\locExampleDir\lines.txt";
            ma.createLinesFile(codeFilePath);

            Console.WriteLine("Blank LOC: " + ma.getBlankLinesOfCode());
            int cloc = ma.getCommentsLinesOfCode();
            Console.WriteLine("CLOC: " + cloc);
            Console.WriteLine("Level of commentary usage: " + (float)cloc/(float)loc);

            Console.WriteLine("Logical LOC: " + ma.getLogicalLinesOfCodeKeywords());
        }
    }
}
