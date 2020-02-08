using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Coursework
{
    class ShiftFunctions
    {
        private Sex identifySex(string sex)
        {
            if (sex.Equals("f"))
            {
                return Sex.Female;
            }
            else if (sex.Equals("m"))
            {
                return Sex.Male;
            }
            else
            {
                throw new Exception("Unknown sex!");
            }
        }

        private Stack<Child> deserializeChildren(string path)
        {
            string[] children = File.ReadAllLines(path);
            Child[] childrenArr = new Child[children.Length];
            for (int i = 0; i < children.Length; i++)
            {
                string[] childData = children[i].Split(' ');
                Child newChild = new Child(childData[0], childData[1], identifySex(childData[2]), Int32.Parse(childData[3]));
                childrenArr[i] = newChild;
            }
            for (int j = 0; j < childrenArr.Length; j++)
            {
                for (int i = 0; i < childrenArr.Length; i++)
                {
                    if (childrenArr[i].getAge() >= childrenArr[j].getAge()) 
                    {
                        Child temp = childrenArr[j];
                        childrenArr[j] = childrenArr[i];
                        childrenArr[i] = temp;

                    }
                }
            }
            Stack<Child> stack = new Stack<Child>();
            for (int i = 0; i < childrenArr.Length; i++)
            {
                stack.Push(childrenArr[i]);
            }
            return stack;
        }

        private Stack<Counselor> deserializeCounselors(string path)
        {
            string[] counselors = File.ReadAllLines(path);
            Counselor[] counselorsArr = new Counselor[counselors.Length];
            for (int i = 0; i < counselors.Length; i++)
            {
                string[] counselorData = counselors[i].Split(' ');
                Counselor newChild = new Counselor(counselorData[0], counselorData[1], identifySex(counselorData[2]), Int32.Parse(counselorData[3]));
                counselorsArr[i] = newChild;
            }
            Stack <Counselor> stack = new Stack<Counselor>();
            for (int i = 0; i < counselorsArr.Length; i++)
            {
                stack.Push(counselorsArr[i]);
            }
            return stack;
        }

        public int start()
        {
            string childrenPath = @"C:\Users\DrLove\Documents\Visual Studio 2015\Projects\Coursework\Coursework\data\children.txt";
            string counselorsPath = @"C:\Users\DrLove\Documents\Visual Studio 2015\Projects\Coursework\Coursework\data\counselors.txt";
            const int childrenInSquad = 20;
            const int counselorsInSquad = 2;
            bool isDutySquadExisting = false;
            IFreeTimeEvent dayEvent = new TypicalDay();
            int days = 0;
            Squad allSquads = new Squad("Squads");
            Stack<Child> stackOfChildren = deserializeChildren(childrenPath);
            Stack<Counselor> stackOfCounselors = deserializeCounselors(counselorsPath);
            bool night = false;
            int daysInShift = 5;

            while (true)
            {
                Console.WriteLine("Please, enter a number to choose the action\n1. Create a squad\n2. Exit from the shift\n3. Display all of the people in camp\n4. Conduct a day");
                if (dayEvent.getEventName() != "Trip")
                {
                    Console.WriteLine("5. Set the Camping Trip");
                }
                else
                {
                    Console.WriteLine("5. Unset the Camping Trip");
                }
                if (dayEvent.getEventName() != "Olympics")
                {
                    Console.WriteLine("6. Set the Olympics");
                }
                else
                {
                    Console.WriteLine("6. Unset the Olympics");
                }
                if (isDutySquadExisting)
                {
                    Console.WriteLine("7. Unset the duty squad");
                }
                else
                {
                    Console.WriteLine("7. Set a duty squad");
                }
                if (night)
                {
                    Console.WriteLine("8. Unset the Night Mode");
                }
                else
                {
                    Console.WriteLine("8. Set the Night Mode");
                }

                Console.Write("> ");
                string request = Console.ReadLine();
                Console.Clear();
                switch (request)
                {
                    case "1":
                        if (stackOfChildren.Count >= childrenInSquad && stackOfCounselors.Count >= counselorsInSquad)
                        {
                            Console.Write("Enter a name for the new squad\n> ");
                            string squadName = Console.ReadLine();
                            Squad newSquad = new Squad(squadName);
                            Human[] squad = new Human[(childrenInSquad + counselorsInSquad)];
                            for (int i=0; i < counselorsInSquad; i++)
                            {
                                squad[i] = stackOfCounselors.Pop();
                            }
                            for (int i = counselorsInSquad; i < (childrenInSquad + counselorsInSquad); i++)
                            {
                                squad[i] = stackOfChildren.Pop();
                            }
                            newSquad.fillSquad(squad);
                            allSquads.add(newSquad);
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("There're no solicitous humans");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;
                    case "2":
                        return 0;
                    case "3":
                        allSquads.display(0);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        if (allSquads.isEmpty())
                        {
                            Console.WriteLine("Error! There isn't any squad in the camp. Press a key");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        Console.Write("Choose an evening event\n1. Go cinema\n2. Go disco\n3. Have a squad meeting\n> ");
                        string evenEvent = Console.ReadLine();
                        Console.Clear();
                        IEveningStrategy strategy = null;
                        bool broken = false;
                        switch (evenEvent)
                        {
                            case "1": strategy = new Cinema(); break;
                            case "2": strategy = new Disco(); break;
                            case "3": strategy = new SquadMeeting(); break;
                            default:
                                Console.WriteLine("Error! Incorrect event. Press a key");
                                Console.ReadKey();
                                Console.Clear();
                                broken = true;
                                break;
                        }
                        if (broken)
                        {
                            break;
                        }
                        if (night)
                        {
                            AbstractDay abstDay = new Day();
                            abstDay.setEvening(strategy);
                            Decorator dayPlusNight = new DayPlusNightDecorator();
                            dayPlusNight.setAdditionOn(abstDay);
                            dayPlusNight.checkDay(allSquads, dayEvent);
                        }
                        else
                        {
                            Day day = new Day();
                            day.setEvening(strategy);
                            day.checkDay(allSquads, dayEvent);
                        }
                        Console.ReadKey();
                        Console.Clear();
                        days++;
                        break;
                    case "5":
                        if (dayEvent.getEventName() == "Trip")
                        {
                            dayEvent = new TypicalDay();
                        }
                        else if (dayEvent.getEventName() == "Olympics")
                        {
                            Console.Clear();
                            Console.WriteLine("There already exists the Olympics");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            dayEvent = new Trip();
                        }
                        break;
                    case "6":
                        if (dayEvent.getEventName() == "Olympics")
                        {
                            dayEvent = new TypicalDay();
                        }
                        else if (dayEvent.getEventName() == "Trip")
                        {
                            Console.Clear();
                            Console.WriteLine("There already exists the Camping Trip");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            dayEvent = new Olympics();
                        }
                        break;
                    case "7":
                        if (!isDutySquadExisting)
                        {
                            Console.Clear();
                            Console.WriteLine("Enter a name of existing squad");
                            if (!allSquads.setState(Console.ReadLine(), State.Duty))
                            {
                                Console.Clear();
                                Console.WriteLine("Bad name of squad!\nPress a key");
                                Console.ReadKey();
                            }
                            else
                            {
                                isDutySquadExisting = true;
                            }
                            Console.Clear();
                        }
                        else
                        {
                            allSquads.setState(null, State.Common);
                            isDutySquadExisting = false;
                        }
                        break;
                    case "8":
                        if (night)
                            night = false;
                        else
                            night = true;
                        break;
                    default: break;
                }
                if (days == daysInShift)
                {
                    return allSquads.getExp();
                }
            }
        }
    }
}
