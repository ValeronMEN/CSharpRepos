using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroCoins
{
    public class City
    {
        private int xCoord;
        public int XCoord
        {
            get { return xCoord; }
        }
        private int yCoord;
        public int YCoord
        {
            get { return yCoord; }
        }
        private List<CoinsBalance> coinsBalances;
        public List<CoinsBalance> CoinsBalances
        {
            get { return coinsBalances; }
        }
        private List<CoinsBalance> newCoinsBalances; // this list is used to prevent getting a coin through the entire eurozone to another city in one day

        private List<City> neighbours;
        public List<City> Neighbours
        {
            get { return neighbours; }
        }
        private string commonwealthName;
        public string CommonwealthName
        {
            get { return commonwealthName; }
        }

        private const int InitialCityCoinBalance = 1000000;
        private const int RepresentativePortion = 1000;

        public City(int xCoord, int yCoord, string commonwealthName)
        {
            this.xCoord = xCoord;
            this.yCoord = yCoord;

            this.commonwealthName = commonwealthName;
            CoinsBalance thisCountryCoinsBalance = new CoinsBalance(commonwealthName, InitialCityCoinBalance);
            coinsBalances = new List<CoinsBalance>();
            newCoinsBalances = new List<CoinsBalance>();
            neighbours = new List<City>();
            coinsBalances.Add(thisCountryCoinsBalance);
        }

        public void addNeighbour(City city)
        {
            neighbours.Add(city);
        }

        public int getNeighboursCount()
        {
            return neighbours.Count;
        }

        public void distributeCoinsToNeighbours()
        {
            foreach (CoinsBalance cb in coinsBalances)
            {
                int numberOfCoinsToDistribute = (int)(Math.Floor(cb.CoinsNumber / (float)RepresentativePortion)); // Floor!
                if (numberOfCoinsToDistribute == 0)
                    continue;

                foreach (City neighbour in this.neighbours)
                {
                    neighbour.addNewCoin(new CoinsBalance(cb.CommonwealhName, numberOfCoinsToDistribute));
                    cb.CoinsNumber -= numberOfCoinsToDistribute;
                }
            }
        }

        public void addNewCoin(CoinsBalance newCoinsBalance)
        {
            newCoinsBalances.Add(newCoinsBalance);
        }

        public void mergeOldAndNewCoins()
        {
            bool isMatch;
            foreach (CoinsBalance ncb in newCoinsBalances)
            {
                isMatch = false;
                foreach (CoinsBalance cb in coinsBalances)
                {
                    if (cb.CommonwealhName.Equals(ncb.CommonwealhName))
                    {
                        cb.CoinsNumber += ncb.CoinsNumber;
                        isMatch = true;
                        break;
                    }
                }
                if (!isMatch)
                    coinsBalances.Add(ncb);
            }

            newCoinsBalances.Clear();
        }
    }
}
