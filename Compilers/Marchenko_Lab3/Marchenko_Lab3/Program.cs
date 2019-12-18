using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marchenko_Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 35;

            testFiveFiles();
        }

        private static void testOneFile()
        {
            displayFile("input5.txt");
            Console.WriteLine();

            LexicAnalyst la = new LexicAnalyst("input5.txt", "keywords.txt", "delimiters.txt");
            la.analyze();
            la.display();
            Console.WriteLine();

            Dictionary<string, Dictionary<string, int>> tables = la.getTables();

            SyntaxAnalyst synta = new SyntaxAnalyst(tables, la.getLexemList());
            synta.start();
            synta.display();
            Console.ReadKey();
            Console.WriteLine();

            if (!synta.IsError)
            {
                SemanticAnalyst sema = new SemanticAnalyst(tables, synta.getTree());
                sema.launch();
                sema.display();
                Console.ReadKey();
            }
        }

        private static void testFiveFiles()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("\nTEST {0}:\n", i);

                string fileName = "input" + i + ".txt";
                displayFile(fileName);
                Console.WriteLine();

                LexicAnalyst la = new LexicAnalyst(fileName, "keywords.txt", "delimiters.txt");
                la.analyze();
                la.display();
                Console.WriteLine();

                Dictionary<string, Dictionary<string, int>> tables = la.getTables();

                SyntaxAnalyst synta = new SyntaxAnalyst(tables, la.getLexemList());
                synta.start();
                synta.display();
                Console.ReadKey();
                Console.WriteLine();

                if (!synta.IsError)
                {
                    SemanticAnalyst sema = new SemanticAnalyst(tables, synta.getTree());
                    sema.launch();
                    sema.display();
                    Console.ReadKey();
                }
            }
        }

        private static void displayFile(string filePath)
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(filePath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
                file.Close();
            }
        }
    }
}
