using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroCoins
{
    public class Eurozone
    {
        private List<Commonwealth> commonwealths;
        public List<Commonwealth> Commonwealths
        {
            get { return commonwealths; }
        }
        private const int NumberOfNeighbours = 4; // one to the north, east, west and south

        public Eurozone()
        {
            commonwealths = new List<Commonwealth>();
        }

        public void addCommonwealth(Commonwealth country)
        {
            commonwealths.Add(country);
        }

        // links every city to its neighbours
        public void setCityNeighbours()
        {
            foreach (Commonwealth currentCountry in this.commonwealths)
            {
                foreach (City currentCity in currentCountry.Cities)
                {
                    if (currentCity.getNeighboursCount() == NumberOfNeighbours)
                        continue;
                    foreach (Commonwealth anotherCountry in this.commonwealths)
                    {
                        foreach (City anotherCity in anotherCountry.Cities)
                        {
                            if (currentCity.Equals(anotherCity))
                                continue;
                            if ((currentCity.XCoord + 1 == anotherCity.XCoord && currentCity.YCoord == anotherCity.YCoord) ||
                                (currentCity.XCoord - 1 == anotherCity.XCoord && currentCity.YCoord == anotherCity.YCoord) ||
                                (currentCity.XCoord == anotherCity.XCoord && currentCity.YCoord + 1 == anotherCity.YCoord) ||
                                (currentCity.XCoord == anotherCity.XCoord && currentCity.YCoord - 1 == anotherCity.YCoord))
                                currentCity.addNeighbour(anotherCity);
                        }
                    }
                }
            }
        }

        // checks if the entered countries are neighbours to each other
        public bool checkCountriesConnection()
        {
            foreach (Commonwealth currentCountry in this.commonwealths)
            {
                foreach (City currentCity in currentCountry.Cities)
                {
                    foreach (City neighbour in currentCity.Neighbours)
                    {
                        if (String.Compare(currentCity.CommonwealthName, neighbour.CommonwealthName) != 0)
                        {
                            currentCountry.linkedFlag = true;
                            break;
                        }
                    }
                    if (currentCountry.linkedFlag)
                        break;
                }
                if (!currentCountry.linkedFlag)
                {
                    Console.WriteLine("Bad configuration of the countries. There's no connection of one of the commonwealth to the others");
                    return false;
                }
            }
            return true;
        }

        // distributes one representative portion of coins to the cities' neighbours
        public void distributeCoins()
        {
            // distribute coins to neighbours in 'new coins' lists
            foreach (Commonwealth currentCountry in this.commonwealths)
            {
                foreach (City currentCity in currentCountry.Cities)
                {
                    currentCity.distributeCoinsToNeighbours();
                }
            }

            // merge old and new ones
            foreach (Commonwealth currentCountry in this.commonwealths)
            {
                foreach (City currentCity in currentCountry.Cities)
                {
                    currentCity.mergeOldAndNewCoins();
                }
            }
        }

        public void sortCountries()
        {
            CommonwealthComparer cc = new CommonwealthComparer();

            this.commonwealths.Sort(cc);
        }

        // checks complete cities and countries
        public bool checkCoinsDistribution(int days)
        {
            bool toReturn = true;
            foreach (Commonwealth currentCountry in this.commonwealths)
            {
                int completeCityCount = 0;
                foreach (City currentCity in currentCountry.Cities)
                {
                    if(currentCity.CoinsBalances.Count == commonwealths.Count)
                    {
                        completeCityCount++;
                    }
                }
                if (completeCityCount == currentCountry.Cities.Count && !currentCountry.completeFlag)
                {
                    currentCountry.completeFlag = true;
                    Console.WriteLine(currentCountry.Name + " " + days);
                }
                if (completeCityCount != currentCountry.Cities.Count)
                    toReturn = false;
            }
            return toReturn;
        }
    }

    public class CommonwealthComparer : IComparer<Commonwealth>
    {
        public int Compare(Commonwealth x, Commonwealth y)
        {
            return String.Compare(x.Name, y.Name);
        }
    }
}
