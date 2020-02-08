using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    interface IFreeTimeEvent
    {
        void eventPrevQuietHour(Squad allSquads);
        void eventAfterQuietHour(Squad allSquads);
        string getEventName();
    }

    class Olympics : IFreeTimeEvent
    {
        public void eventPrevQuietHour(Squad allSquads)
        {
            Console.WriteLine("10:00 - starting the Olympics...");
            Console.WriteLine("12:45 - pausing the Olympics...");
            BasicFunctions.manageMiddayAndQuietHour(allSquads);
        }

        public void eventAfterQuietHour(Squad allSquads)
        {
            Console.WriteLine("17:00 - continuing the Olympics...");
            BasicFunctions.wait();
            Console.WriteLine("19:00 - ending the Olympics...");
            BasicFunctions.gainExperience(allSquads, 300, 400);
        }

        public string getEventName()
        {
            return "Olympics";
        }
    }

    class Trip : IFreeTimeEvent
    {
        public void eventPrevQuietHour(Squad allSquads)
        {
            Console.WriteLine("10:00 - starting the Camping Trip...");
            int exp = allSquads.getSquadExp();
            AbstractTrip absTrip;
            if (exp < 30000)
            {
                absTrip = new AbstractTrip(new WalkingAroundTheCity());
            }
            else if (exp < 60000)
            {
                absTrip = new AbstractTrip(new CampingInTheWoods());
            }
            else
            {
                absTrip = new AbstractTrip(new HikingInTheMountains());
            }
            absTrip.goForATrip(allSquads);
            Console.WriteLine("16:00 - ending the Camping Trip...");
        }

        public void eventAfterQuietHour(Squad allSquads)
        {
            BasicFunctions.manageEveningFreeTime(allSquads);
        }

        public string getEventName()
        {
            return "Trip";
        }
    }

    class TypicalDay : IFreeTimeEvent
    {
        public void eventPrevQuietHour(Squad allSquads)
        {
            Console.WriteLine("10:00 - starting the free time...");
            BasicFunctions.wait();
            Console.WriteLine("12:45 - ending the free time...");
            BasicFunctions.wait();
            BasicFunctions.gainExperience(allSquads, 130, 200);
            BasicFunctions.manageMiddayAndQuietHour(allSquads);
        }

        public void eventAfterQuietHour(Squad allSquads)
        {
            BasicFunctions.manageEveningFreeTime(allSquads);
        }

        public string getEventName()
        {
            return "TypicalDay";
        }
    }
}
