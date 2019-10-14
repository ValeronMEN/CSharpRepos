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
        private List<Coin> coins;
        public List<Coin> Coins
        {
            get { return coins; }
        }
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

        private const int initialCityCoinBalance = 1000000;

        public City(int xCoord, int yCoord, string commonwealthName)
        {
            this.xCoord = xCoord;
            this.yCoord = yCoord;

            this.commonwealthName = commonwealthName;
            Coin coin = new Coin(commonwealthName);
            coins = new List<Coin>();
            neighbours = new List<City>();
            for (int i=0; i<initialCityCoinBalance; i++)
            {
                coins.Add(coin);
            }
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
            int numberOfCoinsToDistribute = (int)(Math.Floor(this.coins.Count / 1000.0));
            foreach (City neighbour in this.neighbours)
            {
                for (int i = 0; i < numberOfCoinsToDistribute; i++)
                {
                    var rand = new Random();
                    int index = rand.Next(0, coins.Count);
                    neighbour.addCoin(coins.ElementAt<Coin>(index));
                    coins.RemoveAt(index);
                }
            }
        }

        public void addCoin(Coin coin)
        {
            this.coins.Add(coin);
        }
    }
}
