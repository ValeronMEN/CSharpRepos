using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroCoins
{
    public class CoinsBalance
    {
        private string commonwealthName;
        public string CommonwealhName
        {
            get { return commonwealthName; }
        }
        private int coinsNumber;
        public int CoinsNumber
        {
            get { return coinsNumber; }
            set { coinsNumber = value; }
        }

        public CoinsBalance(string commonwealthName, int initialCoinBalance)
        {
            this.commonwealthName = commonwealthName;
            this.coinsNumber = initialCoinBalance;
        }
    }
}
