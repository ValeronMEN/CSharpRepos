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
        private const int numberOfNeighbours = 4; // one to the north, east, west and south

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
                    if (currentCity.getNeighboursCount() == numberOfNeighbours)
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
        public void checkCountriesConnection()
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
                    throw new Exception("Bad configuration of the countries. There's no connection of one of the commonwealth to the others");
            }
        }

        // distributes one representative portion of coins to the cities' neighbours
        public void distributeCoins()
        {
            foreach (Commonwealth currentCountry in this.commonwealths)
            {
                foreach (City currentCity in currentCountry.Cities)
                {
                    currentCity.distributeCoinsToNeighbours();
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
                    List<Coin> differentCoinsInCity = new List<Coin>();
                    foreach (Coin currentCoin in currentCity.Coins)
                    {
                        if (differentCoinsInCity.Count == 0)
                        {
                            differentCoinsInCity.Add(currentCoin);
                        }
                        else if (differentCoinsInCity.Count == commonwealths.Count)
                        {
                            break;
                        }
                        else
                        {
                            int exampleCoinCount = 0;
                            foreach (Coin exampleCoin in differentCoinsInCity)
                            {
                                if (String.Compare(exampleCoin.CommonwealhName, currentCoin.CommonwealhName) != 0)
                                    exampleCoinCount++;
                            }
                            if (exampleCoinCount == differentCoinsInCity.Count)
                            {
                                differentCoinsInCity.Add(currentCoin);
                            }
                        }
                    }
                    if (differentCoinsInCity.Count == commonwealths.Count)
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
