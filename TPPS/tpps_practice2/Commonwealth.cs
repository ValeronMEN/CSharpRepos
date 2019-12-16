using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroCoins
{
    public class Commonwealth
    {
        private List<City> cities;
        public List<City> Cities
        {
            get { return cities; }
        }
        private string name;
        public string Name
        {
            get { return name; }
        }
        private int xl;
        public int Xl
        {
            get { return xl; }
        }
        private int yl;
        public int Yl
        {
            get { return yl; }
        }
        private int xh;
        public int Xh
        {
            get { return xh; }
        }
        private int yh;
        public int Yh
        {
            get { return yh; }
        }
        public bool linkedFlag;
        public bool completeFlag;

        public Commonwealth(string name, int xl, int yl, int xh, int yh)
        {
            this.name = name;
            this.xl = xl;
            this.yl = yl;
            this.xh = xh;
            this.yh = yh;
            linkedFlag = false;
            completeFlag = false;

            cities = new List<City>();
            for (int i=xl; i<=xh; i++)
            {
                for (int j=yl; j<=yh; j++)
                {
                    cities.Add(new City(i, j, name));
                }
            }
        }
    }
}
