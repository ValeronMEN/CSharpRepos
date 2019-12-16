using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroCoins
{
    public class Coin
    {
        private string commonwealthName;
        public string CommonwealhName
        {
            get { return commonwealthName; }
        }

        public Coin(string commonwealthName)
        {
            this.commonwealthName = commonwealthName;
        }
    }
}
