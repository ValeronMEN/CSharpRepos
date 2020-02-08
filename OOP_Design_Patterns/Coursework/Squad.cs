using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coursework
{
    enum Sex { Male, Female };
    enum State { Duty, Common };

    abstract class Human
    {
        protected string name;
        protected string surname;
        protected Sex sex;
        protected int exp;
        protected int age;
        protected HumanState state;
        protected DutyState dutyState = new DutyState();
        protected CommonState commonState = new CommonState();

        public Human(string name)
        {
            this.name = name;
        }

        public abstract void add(Human c);
        public abstract void remove(Human c);
        public abstract void display(int depth);
        public abstract int getExp();
        public abstract void fillSquad(Human[] components);
        public abstract void setExp(int ll, int lr);
        public abstract string getType();
        public abstract bool setState(string squadName, State state);
        public abstract List<string> createAnEvent();

        public string getName()
        {
            return name;
        }

        public int getAge()
        {
            return age;
        }
    }

    class Squad : Human
    {
        private List<Human> _children = new List<Human>();

        public Squad(string name) : base(name)
        {
            state = commonState;
            exp = 0;
        }

        public bool isEmpty()
        {
            if (_children.Count == 0)
            {
                return true;
            }
            return false;
        }

        public override bool setState(string squadName, State state)
        {
            if (state == State.Duty)
            {
                foreach (Human component in _children)
                {
                    if (component.getType() == "Squad")
                    {
                        if (squadName == component.getName())
                        {
                            component.setState(squadName, State.Duty);
                            this.state = dutyState;
                            return true;
                        }
                    }
                    else
                    {
                        component.setState(squadName, State.Duty);
                    }
                }
                return false;
            }
            else if (state == State.Common)
            {
                foreach (Human component in _children)
                {
                    component.setState(null, State.Common);
                }
                this.state = commonState;
                return true;
            }
            return false;
        }

        public override string getType()
        {
            return "Squad";
        }

        public override void add(Human component)
        {
            _children.Add(component);
        }

        public override void fillSquad(Human [] components)
        {
            for (int i=0; i < components.Length; i++)
            {
                _children.Add(components[i]);
            }
        }

        public override void remove(Human component)
        {
            _children.Remove(component);
        }

        public override void display(int depth)
        {
            Console.Write(new String('-', depth) + name);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(", Total exp: "+ getExp().ToString());
            Console.ForegroundColor = ConsoleColor.Black;
            foreach (Human component in _children)
                component.display(depth + 8);
        }

        public override int getExp()
        {
            exp = 0;
            foreach (Human component in _children)
                exp += component.getExp();
            return exp;
        }

        public int getSquadExp()
        {
            return (getExp() / _children.Count);
        }

        public override void setExp(int ll, int lr)
        {
            foreach (Human component in _children)
                component.setExp(ll, lr);
        }

        public override List<string> createAnEvent()
        {
            List<string> list = new List<string>();
            string[] fullnames = new string[_children.Count * 22];
            int pos = 0;
            bool isSquad = true;
            foreach (Human component in _children)
            {
                List<string> childrenList = component.createAnEvent();
                foreach (string fullname in childrenList)
                {
                    if (component.getType() != "Squad")
                    {
                        list.Add(fullname);
                        isSquad = true;
                    }
                    else
                    {
                        fullnames[pos] = fullname;
                        pos++;
                        isSquad = false;
                    }
                }
            }
            if (isSquad)
            {
                return list;
            }
            else
            {
                int length = pos;
                if (pos % 2 == 1)
                {
                    length = pos - 1;
                }
                if (length == 0)
                {
                    return null;
                }
                for (int i=0; i<length; i = (i + 2))
                {
                    Console.WriteLine("There's the conflict between " + fullnames[i] + " and " + fullnames[i+1]);
                }
                return null;
            }
        }
    }

    class Counselor : Human
    {
        public Counselor(string name, string surname, Sex sex, int age) : base(name)
        {
            state = commonState;
            this.surname = surname;
            this.sex = sex;
            this.age = age;
            exp = 0;
        }

        public override void add(Human c) { }
        public override void remove(Human c) { }
        public override void fillSquad(Human[] components) { }

        public override void display(int depth)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.Write(new String('-', depth));
            Console.Write("{0} {1}, Exp: {2}, Sex: {3}, Age: {4}", name, surname, exp.ToString(), sex, age);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" ");
            if (this.state == dutyState)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Duty");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine(" ");
            }
        }
        
        public override int getExp()
        {
            return exp;
        }

        public override void setExp(int ll, int lr)
        {
            Random rd = new Random();
            Thread.Sleep(1);
            if (state != dutyState)
            {
                exp += (int)((rd.Next(ll, lr)) / 2);
            }
            else
            {
                exp += rd.Next(ll, lr);
            }
        }

        public override bool setState(string squadName, State state)
        {
            if (state == State.Duty)
            {
                this.state = dutyState;
            }
            else if (state == State.Common)
            {
                this.state = commonState;
            }
            return true;
        }

        public override string getType()
        {
            return "Counselor";
        }

        public override List<string> createAnEvent()
        {
            return new List<string>();
        }
    }

    class Child : Human
    {
        public Child(string name, string surname, Sex sex, int age) : base(name)
        {
            state = commonState;
            this.surname = surname;
            this.sex = sex;
            this.age = age;
            exp = 0;
        }

        public override void add(Human c) { }
        public override void remove(Human c) { }
        public override void fillSquad(Human[] components) { }

        public override void display(int depth)
        {
            Console.Write(new String('-', depth));
            Console.Write("{0} {1}, ", name, surname);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Exp: {0}", exp.ToString());
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(", Sex: {0}, Age: {1} ", sex, age);

            if (this.state == dutyState)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Duty");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine(" ");
            }
        }
        
        public override int getExp()
        {
            return exp;
        }

        public override void setExp(int ll, int lr)
        {
            Random rd = new Random();
            Thread.Sleep(1);
            if (state == dutyState)
            {
                exp += ((rd.Next(ll, lr)) * 2);
            }
            else
            {
                exp += rd.Next(ll, lr);
            }
        }

        public override bool setState(string squadName, State state)
        {
            if (state == State.Duty)
            {
                this.state = dutyState;
            }
            else if (state == State.Common)
            {
                this.state = commonState;
            }
            return true;
        }

        public override string getType()
        {
            return "Child";
        }

        public override List<string> createAnEvent()
        {
            List<string> list = new List<string>();
            if (state.getConflictPossibility(this.exp))
            {
                string fullname = name + " " + surname;
                list.Add(fullname);
            }
            return list;
        }
    }
}
