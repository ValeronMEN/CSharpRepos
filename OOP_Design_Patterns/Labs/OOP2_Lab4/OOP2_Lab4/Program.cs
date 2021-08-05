using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2_Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            #region
            int[] coordsX = { 1, 1, 1 };
            int[] coordsY = { 1, 2, 3 };
            Ship ship = new Ship(coordsX, coordsY);
            ship.display();
            ship.hit(1, 1);
            ship.display();
            ship.hit(1, 3);
            ship.display();
            ship.hit(1, 2);
            ship.display();
            #endregion

            Console.WriteLine("");

            #region
            Mall mall = new Mall();
            mall.ascent();
            mall.displayAccessibleWays();
            mall.setWay(new Elevator());
            mall.ascent();
            mall.setWay(new Escalator());
            mall.ascent();
            mall.setWay(new Staircase());
            mall.ascent();
            #endregion
        }
    }
}
