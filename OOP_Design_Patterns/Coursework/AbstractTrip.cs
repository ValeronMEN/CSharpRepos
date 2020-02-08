using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    interface ITrip
    {
        void goForATrip(Squad allSquads);
    }

    class AbstractTrip
    {
        ITrip tripInterface;

        public AbstractTrip(ITrip trip)
        {
            tripInterface = trip;
        }

        public void goForATrip(Squad allSquads)
        {
            tripInterface.goForATrip(allSquads);
        }
    }

    class HikingInTheMountains : ITrip
    {
        public void goForATrip(Squad allSquads)
        {
            Console.WriteLine("It's a hiking in the mountains");
            BasicFunctions.wait();
            Console.WriteLine("Camping at the foot of the mountains...");
            BasicFunctions.wait();
            Console.WriteLine("Listening to Vladimir Visozkii...");
            BasicFunctions.wait();
            Console.WriteLine("Climbing up to the top of the hill...");
            BasicFunctions.wait();
            Console.WriteLine("Lying down on the top of the hill...");
            BasicFunctions.wait();
            Console.WriteLine("Returning to the camp...");
            BasicFunctions.wait();
            BasicFunctions.gainExperience(allSquads, 1000, 2000);
        }
    }

    class CampingInTheWoods : ITrip
    {
        public void goForATrip(Squad allSquads)
        {
            Console.WriteLine("It's a camping in the woods");
            BasicFunctions.wait();
            Console.WriteLine("Collecting mushrooms...");
            BasicFunctions.wait();
            Console.WriteLine("Creating a small camp...");
            BasicFunctions.wait();
            Console.WriteLine("Cooking food...");
            BasicFunctions.wait();
            Console.WriteLine("Cleaning up the territory...");
            BasicFunctions.wait();
            Console.WriteLine("Returning to the camp...");
            BasicFunctions.wait();
            BasicFunctions.gainExperience(allSquads, 500, 1000);
        }
    }

    class WalkingAroundTheCity : ITrip
    {
        public void goForATrip(Squad allSquads)
        {
            Console.WriteLine("It's a walking around the city");
            BasicFunctions.wait();
            Console.WriteLine("Visiting museums...");
            BasicFunctions.wait();
            Console.WriteLine("Spending free time...");
            BasicFunctions.wait();
            Console.WriteLine("Alcohol checking...");
            BasicFunctions.wait();
            Console.WriteLine("Returning to the camp...");
            BasicFunctions.wait();
            BasicFunctions.gainExperience(allSquads, 0, 500);
        }
    }
}
