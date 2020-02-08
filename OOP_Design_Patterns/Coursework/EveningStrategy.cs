using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    interface IEveningStrategy
    {
        void manageEvening(Squad allSquads);
    }

    class Cinema : IEveningStrategy
    {
        public Cinema() { }
        public void manageEvening(Squad allSquads)
        {
            Console.WriteLine("20:00 - going to cinema...");
            BasicFunctions.gainExperience(allSquads, 0, 10);
        }
    }

    class Disco : IEveningStrategy
    {
        public Disco() { }
        public void manageEvening(Squad allSquads)
        {
            Console.WriteLine("20:00 - going to disco...");
            BasicFunctions.gainExperience(allSquads, 10, 40);
        }
    }

    class SquadMeeting : IEveningStrategy
    {
        public SquadMeeting() { }
        public void manageEvening(Squad allSquads)
        {
            Console.WriteLine("20:00 - having a squad meeting...");
            BasicFunctions.gainExperience(allSquads, 40, 60);
        }
    }
}
