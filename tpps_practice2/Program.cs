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
            List<Eurozone> eurozones = new List<Eurozone>();

            // input
            while (true)
            {
                string countryCountStr = Console.ReadLine();
                int countryCount = Convert.ToInt32(countryCountStr);
                if (countryCount == 0)
                {
                    break;
                }
                else if (countryCount >= 1 && countryCount <= 20)
                {
                    Eurozone eurozone = new Eurozone();
                    for(int i=countryCount; i>0; i--)
                    {
                        string country = Console.ReadLine();
                        string[] countryParts = country.Split(' ');
                        if (countryParts.Length != 5)
                        {
                            i++;
                            Console.WriteLine("Bad country name and coordinates!");
                            continue;
                        }
                        string countryName = countryParts[0];
                        if (countryName.Length > 25)
                            throw new Exception("Length of the country name is more than 25 characters");
                        try
                        {
                            int countryXl = Convert.ToInt32(countryParts[1]);
                            int countryYl = Convert.ToInt32(countryParts[2]);
                            int countryXh = Convert.ToInt32(countryParts[3]);
                            int countryYh = Convert.ToInt32(countryParts[4]);
                            if (countryXl < 1 || countryXl > 10 || countryYl < 1 || countryYl > 10 || countryXh < 1 || countryXh > 10 || countryYh < 1 || countryYh > 10)
                                throw new Exception("Every coordinate must be from 1 to 10");
                            eurozone.addCommonwealth(new Commonwealth(countryName, countryXl, countryYl, countryXh, countryYh));
                        }
                        catch(Exception err)
                        {
                            throw new Exception("Coordinates aren't numbers " + err);
                        }
                    }
                    eurozones.Add(eurozone);
                }
                else
                {
                    throw new Exception("There can be no more countries than 20");
                }
            }

            // output
            int caseNumber = 1;
            foreach(Eurozone ez in eurozones)
            {
                Console.WriteLine("Case Number " + caseNumber);
                ez.setCityNeighbours();
                if (ez.Commonwealths.Count > 1)
                {
                    ez.checkCountriesConnection();
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
        }
    }
}
