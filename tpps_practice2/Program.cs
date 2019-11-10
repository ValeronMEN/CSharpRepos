using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroCoins
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Starting program...");
                if (runProgram())
                    break;
            }
        }

        private static bool runProgram()
        {
            List<Eurozone> eurozones = new List<Eurozone>();

            // input
            while (true)
            {
                string countryCountStr = Console.ReadLine();
                int countryCount;
                try
                {
                    countryCount = Convert.ToInt32(countryCountStr);
                }
                catch (FormatException err)
                {
                    Console.WriteLine("Error: countries count is incorrect");
                    return false;
                }

                if (countryCount == 0)
                    break;
                else if (countryCount < 1 || countryCount > 20)
                {
                    Console.WriteLine("Error: there can be no more countries than 20");
                    return false;
                }

                Eurozone eurozone = new Eurozone();
                for (int i = countryCount; i > 0; i--)
                {
                    string country = Console.ReadLine();
                    string[] countryParts = country.Split(' ');
                    if (countryParts.Length != 5)
                    {
                        i++;
                        Console.WriteLine("Bad country name and coordinates! Write it down again");
                        continue;
                    }
                    string countryName = countryParts[0];
                    if (countryName.Length > 25)
                    {
                        Console.WriteLine("Error: length of the country name is more than 25 characters");
                        return false;
                    }
                    int countryXl, countryYl, countryXh, countryYh;
                    try
                    {
                        countryXl = Convert.ToInt32(countryParts[1]);
                        countryYl = Convert.ToInt32(countryParts[2]);
                        countryXh = Convert.ToInt32(countryParts[3]);
                        countryYh = Convert.ToInt32(countryParts[4]);
                    }
                    catch (FormatException err)
                    {
                        Console.WriteLine("Error: coordinates aren't numbers");
                        return false;
                    }
                    if (countryXl < 1 || countryXl > 10 || countryYl < 1 || countryYl > 10 || countryXh < 1 || countryXh > 10 || countryYh < 1 || countryYh > 10)
                    {
                        Console.WriteLine("Error: every coordinate must be from 1 to 10");
                        return false;
                    }
                    eurozone.addCommonwealth(new Commonwealth(countryName, countryXl, countryYl, countryXh, countryYh));
                }
                eurozones.Add(eurozone);
            }

            // output
            int caseNumber = 1;
            foreach (Eurozone ez in eurozones)
            {
                Console.WriteLine("Case Number " + caseNumber);
                ez.setCityNeighbours();
                if (ez.Commonwealths.Count > 1 && !ez.checkCountriesConnection())
                {
                    return false;
                }
                ez.sortCountries();
                int days = 0;
                while (true)
                {
                    if (ez.checkCoinsDistribution(days))
                        break;
                    ez.distributeCoins();
                    days++;
                }
                caseNumber++;
            }

            return true;
        }
    }
}
