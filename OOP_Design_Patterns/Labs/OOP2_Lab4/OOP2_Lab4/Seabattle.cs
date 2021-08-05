using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2_Lab4
{
    class Ship
    {
        private ShipFragment[] fragments;
        private int health;
        private int healthStart;

        public Ship(int [] coordsX, int [] coordsY)
        {
            int maxHealth = 4;
            if (coordsX.Length <= maxHealth && coordsY.Length <= maxHealth && coordsX.Length == coordsY.Length &&
                coordsX.Length >= 0 && coordsY.Length >= 0)
            {
                healthStart = health = coordsX.Length;
                fragments = new ShipFragment[health];
                for (int i = 0; i < health; i++)
                {
                    fragments[i] = new ShipFragment(coordsX[i], coordsY[i]);
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public void hit(int x, int y)
        {
            int killed = 0;
            for (int i = 0; i < fragments.Length; i++)
            {
                if (x == fragments[i].PosX && y == fragments[i].PosY)
                {
                    fragments[i].setWoundedState();
                }
                if (fragments[i].getName() == "Wounded")
                {
                    killed++;
                }
            }
            if (killed == healthStart)
            {
                Console.WriteLine("... and killed");
                for (int i = 0; i < fragments.Length; i++)
                {
                    fragments[i].setKilledState();
                }
            }
        }

        public void display()
        {
            for (int i = 0; i < fragments.Length; i++)
            {
                fragments[i].display();
            }
        }
    }

    class ShipFragment
    {
        private int posX;
        private int posY;
        public int PosX
        {
            get { return posX; }
        }
        public int PosY
        {
            get { return posY; }
        }

        private ShipFragmentHealthState currentState;
        ShipFragmentStateUnscathed unscathedShipState = new ShipFragmentStateUnscathed();
        ShipFragmentStateWounded woundedShipState = new ShipFragmentStateWounded();
        ShipFragmentStateKilled killedShipState = new ShipFragmentStateKilled();

        public ShipFragment(int x, int y)
        {
            currentState = unscathedShipState;
            posX = x;
            posY = y;
        }

        public void display()
        {
            currentState.display();
            //Console.Write("  "+posX+","+posY);
        }

        public void setWoundedState()
        {
            currentState = woundedShipState;
            Console.WriteLine("Ship was hit");
        }

        public void setKilledState()
        {
            currentState = killedShipState;
        }

        public string getName()
        {
            return currentState.getName();
        }
    }

    abstract class ShipFragmentHealthState
    {
        protected string stateName;
        abstract public void display();
        public string getName()
        {
            return stateName;
        }
    }

    class ShipFragmentStateUnscathed : ShipFragmentHealthState
    {
        public ShipFragmentStateUnscathed()
        {
            stateName = "Unscathed";
        }

        override public void display()
        {
            Console.WriteLine("+");
        }
    }

    class ShipFragmentStateWounded : ShipFragmentHealthState
    {
        public ShipFragmentStateWounded()
        {
            stateName = "Wounded";
        }

        override public void display()
        {
            Console.WriteLine("~");
        }
    }

    class ShipFragmentStateKilled : ShipFragmentHealthState
    {
        public ShipFragmentStateKilled()
        {
            stateName = "Killed";
        }

        override public void display()
        {
            Console.WriteLine("x");
        }
    }
}
