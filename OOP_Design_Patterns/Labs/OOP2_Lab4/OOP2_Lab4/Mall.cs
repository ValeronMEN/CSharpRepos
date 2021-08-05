using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2_Lab4
{
    class Mall
    {
        private IAscentStrategy ascentStrategy;

        public Mall() {
        }

        public void setWay(IAscentStrategy strategy)
        {
            ascentStrategy = strategy;
        }

        public void displayAccessibleWays()
        {
            int hours = Time.getHoursNow();
            
            Console.WriteLine("1. Staircase is available!");

            if (hours < 10 || hours > 22)
            {
                Console.WriteLine("2. Elevator and escalator are unavailable");
                return;
            }
            else
            {
                if (hours > 14 && hours < 15)
                {
                    Console.WriteLine("2. Elevator is unavailable");
                }
                else
                {
                    Console.WriteLine("2. Elevator is available!");
                }
                if (hours > 13 && hours < 14)
                {
                    Console.WriteLine("3. Escalator is unavailable");
                }
                else
                {
                    Console.WriteLine("3. Escalator is available!");
                }
            }
        }

        public void ascent()
        {
            if (ascentStrategy == null)
            {
                Console.WriteLine("Choose strategy!");
                return;
            }
            ascentStrategy.ascent();
        }
    }

    public interface IAscentStrategy
    {
        void ascent();
    }

    public class Staircase : IAscentStrategy
    {
        public Staircase() { }

        public void ascent()
        {
            Console.WriteLine("Going the staircase...");
        }
    }

    public class Escalator : IAscentStrategy
    {
        public Escalator()
        {
        }

        public void ascent()
        {
            int hours = Time.getHoursNow();
            if (hours < 10 || hours > 22 || (hours > 13 && hours < 14))
            {
                Console.WriteLine("Escalator is closed");
                return;
            }
            Console.WriteLine("Waiting on the escalator...");
        }
    }

    public class Elevator : IAscentStrategy
    {
        public Elevator()
        {
        }

        public void ascent()
        {
            int hours = Time.getHoursNow();
            if (hours < 10 || hours > 22 || (hours > 14 && hours < 15))
            {
                Console.WriteLine("Elevator is closed");
                return;
            }
            Console.WriteLine("Waiting in the elevator...");
        }
    }
    
    static class Time
    {
        static public int getHoursNow()
        {
            string[] strArr = DateTime.Now.ToShortTimeString().Split(':');
            return Convert.ToInt32(strArr[0]);
        }
    }
}
