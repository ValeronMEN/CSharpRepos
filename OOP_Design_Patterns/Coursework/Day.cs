using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coursework
{
    abstract class AbstractDay
    {
        protected IEveningStrategy eveningStrategy;

        public void setEvening(IEveningStrategy str)
        {
            eveningStrategy = str;
        }

        public abstract void checkDay(Squad allSquads, IFreeTimeEvent freeTimeEvent);
    }

    abstract class Decorator : AbstractDay
    {
        protected AbstractDay abstDay;

        public void setAdditionOn(AbstractDay baseDay)
        {
            abstDay = baseDay;
        }

        public override void checkDay(Squad allSquads, IFreeTimeEvent freeTimeEvent)
        {
            if (abstDay != null)
                abstDay.checkDay(allSquads, freeTimeEvent);
        }
    }

    class DayPlusNightDecorator : Decorator
    {
        public override void checkDay(Squad allSquads, IFreeTimeEvent freeTimeEvent)
        {
            base.checkDay(allSquads, freeTimeEvent);
            Console.WriteLine("22:00 - waking up...");
            BasicFunctions.wait();
            Console.WriteLine("22:20 - going out...");
            BasicFunctions.wait();
            Console.WriteLine("23:30 - counting the children...");
            BasicFunctions.wait();
            Console.WriteLine("00:00 - returning to the buildings...");
            BasicFunctions.wait();
            Console.WriteLine("00:30 - going to sleep...");
            BasicFunctions.wait();
            BasicFunctions.gainExperience(allSquads, 0, 300);
            Console.WriteLine("Night has finished");
        }
    }

    class Day : AbstractDay
    {
        public override void checkDay(Squad allSquads, IFreeTimeEvent freeTimeEvent)
        {
            Console.WriteLine("08:00 - waking up...");
            BasicFunctions.wait();
            Console.WriteLine("08:20 - taking a toilet...");
            BasicFunctions.wait();
            Console.WriteLine("08:30 - training...");
            BasicFunctions.wait();
            Console.WriteLine("08:40 - lining up on the yard...");
            BasicFunctions.wait();
            Console.WriteLine("09:00 - taking a breakfast...");
            BasicFunctions.wait();
            Console.WriteLine("09:00 - checking the cleaning...");
            BasicFunctions.wait();
            BasicFunctions.gainExperience(allSquads, 0, 300);
            freeTimeEvent.eventPrevQuietHour(allSquads);
            Console.WriteLine("16:30 - taking a lunch...");
            BasicFunctions.wait();
            freeTimeEvent.eventAfterQuietHour(allSquads);
            Console.WriteLine("19:00 - taking a dinner...");
            BasicFunctions.wait();
            eveningStrategy.manageEvening(allSquads);
            BasicFunctions.wait();
            Console.WriteLine("21:30 - taking the latest lunch...");
            BasicFunctions.wait();
            Console.WriteLine("22:00 - going to sleep...");
            BasicFunctions.gainExperience(allSquads, 0, 100);
            Console.WriteLine("Day has finished");
        }
    }

    class BasicFunctions
    {
        static public void gainExperience(Squad allSquads, int leftLimitExp, int rightLimitExp)
        {
            if (leftLimitExp > rightLimitExp)
            {
                throw new Exception("Failed limits!");
            }
            allSquads.setExp(leftLimitExp, rightLimitExp);
            allSquads.createAnEvent();
        }

        static public void wait()
        {
            Random rd = new Random();
            int times = rd.Next(1, 5);
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("...");
                Thread.Sleep(200);
            }
        }

        static public void manageMiddayAndQuietHour(Squad allSquads)
        {
            Console.WriteLine("13:00 - taking a midday...");
            BasicFunctions.wait();
            Console.WriteLine("13:30 - starting the free time...");
            BasicFunctions.wait();
            Console.WriteLine("14:00 - ending the free time...");
            Console.WriteLine("14:00 - starting a quiet hour...");
            BasicFunctions.wait();
            Console.WriteLine("16:00 - ending the quiet hour...");
            Console.WriteLine("16:00 - waking up...");
            BasicFunctions.wait();
            BasicFunctions.gainExperience(allSquads, 50, 100);
        }

        static public void manageEveningFreeTime(Squad allSquads)
        {
            Console.WriteLine("17:00 - starting the free time...");
            BasicFunctions.wait();
            Console.WriteLine("19:00 - ending the free time...");
            BasicFunctions.gainExperience(allSquads, 75, 100);
        }
    }
}
