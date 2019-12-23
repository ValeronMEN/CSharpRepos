using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpMetrics
{
    class Program
    {
        static void Main(string[] args)
        {
            OpMetricsAnalyst opma = new OpMetricsAnalyst();
            //string filePath = @"C:\Users\AUL\Documents\locExampleDir\ArchiSteamFarm.cs";
            string dirPath = @"C:\Users\AUL\Documents\locExampleDir\";
            //opma.parseClassesFromOneFile(filePath);
            opma.parseClassesFromDirectoryFiles(dirPath, ".cs");
            Console.WriteLine();
            opma.parseMethodsInClasses();
            opma.countMHF();
            opma.countAHF();
            opma.countNOC();
            opma.countDIT();
            opma.countMIF();
            opma.countPOF();
            opma.countAIF();
            opma.displayClassTree();
            opma.displayMetrics();
        }
    }
}
