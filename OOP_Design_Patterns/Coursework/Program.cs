using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coursework
{
    class Program
    {
        static void Main(string[] args)
        {
            int year = 2017;
            int numberOfShift = 1;
            List<Shift> shifts = new List<Shift>();

            Console.Title = "Summer Camp Simulator 9000";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            ShiftCache.loadCache();
            Console.Clear();

            Console.WriteLine("\n\n\n\n\n\n\n\n");
            drawAstericksLine();
            Console.WriteLine("Hello, It's Summer Camp Simulator 9000. Here you can train yourself to conduct your own camp. Press a key to continue");
            drawAstericksLine();
            drawSpaceLine();
            Console.ReadKey();
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Please, enter a number to choose the action");
                Console.WriteLine("1. Create a shift");
                Console.WriteLine("2. Exit from the program");
                if (shifts.Count != 0) {
                    for (int i=0; i<shifts.Count; i++)
                    {
                        int currentExp = shifts[i].getExp();
                        if (currentExp != 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        Console.Write("{0}. Enter the shift: {1}, {2}, {3}", (i + 3), shifts[i].getYear(),
                            shifts[i].getMonth(), shifts[i].getNumber());
                        if (currentExp != 0)
                        {
                            Console.Write(" (Completed. Result: {0})", currentExp);
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.WriteLine("");
                    }
                }
                Console.Write("> ");
                string request = Console.ReadLine();
                Console.Clear();
                switch (request)
                {
                    case "1":
                        Shift newShift = (Shift)ShiftCache.getShift(numberOfShift.ToString());
                        newShift.setYear(year);
                        numberOfShift++;
                        if (numberOfShift == ShiftCache.getMaxShiftsValue())
                        {
                            year++;
                            numberOfShift = 1;
                        }
                        shifts.Add(newShift);
                        break;
                    case "2":
                        Environment.Exit(0);
                        break;
                    default:
                        if (shifts != null)
                        {
                            bool broken = false;
                            for (int i=0; i<shifts.Count; i++)
                            {
                                string numberOfAction = (i + 3).ToString();
                                if (request.Equals(numberOfAction))
                                {
                                    if (shifts[i].getExp() != 0)
                                    {
                                        break;
                                    }
                                    shifts[i].start();
                                    broken = true;
                                    break;
                                }
                            }
                            if (broken)
                            {
                                break;
                            }
                        }
                        Console.WriteLine("Bad request! Press a key");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        static private void drawAstericksLine()
        {
            for (int i=0; i<80; i++)
            {
                Console.Write("*");
            }
        }

        static private void drawSpaceLine()
        {
            for (int i = 0; i < 40; i++)
            {
                Console.Write(" ");
            }
        }
    }
}
