using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OOP2_Lab5
{
    public class Work
    {
        private int mark;
        public int Mark
        {
            set { mark = value; }
            get { return mark; }
        }
        private string text;
        private string author;
        public string Author
        {
            get { return author; }
        }

        public Work(string text, string author)
        {
            this.text = text;
            this.author = author;
            this.mark = 0;
        }
    }

    abstract public class AbstractTeacher
    {
        public Thread testing;
        protected AbstractTeacher _successor;
        public AbstractTeacher Successor
        {
            set { _successor = value; }
        }

        abstract public void testWork(Work work);
    }

    public class Teacher1 : AbstractTeacher
    {
        override public void testWork(Work work)
        {
            if (testing == null || testing.IsAlive == false)
            {
                testing = new Thread(delegate ()
                {
                    Thread.Sleep(1000);
                    Random rand = new Random();
                    work.Mark = rand.Next(1, 5);
                    Console.WriteLine("1st teacher: The work was tested. " + work.Author + " had " + work.Mark + " points");
                });
                testing.Start();
            }
            else
            {
                Console.WriteLine("Out of 1st!");
                if (_successor != null)
                {
                    _successor.testWork(work);
                }
                else
                {
                    throw new ApplicationException("ChainOfResponsibility  object exhausted all successors without call being handled.");
                }
            }
        }
    }

    public class Teacher2 : AbstractTeacher
    {
        override public void testWork(Work work)
        {
            if (testing == null || testing.IsAlive == false)
            {
                testing = new Thread(delegate ()
                {
                    Thread.Sleep(2000);
                    Random rand = new Random();
                    work.Mark = rand.Next(1, 5);
                    Console.WriteLine("2nd teacher: The work was tested. " + work.Author + " had " + work.Mark + " points");
                });
                testing.Start();
            }
            else
            {
                Console.WriteLine("Out of 2nd!");
                if (_successor != null)
                {
                    _successor.testWork(work);
                }
                else
                {
                    throw new ApplicationException("ChainOfResponsibility  object exhausted all successors without call being handled.");
                }
            }
        }
    }

    public class Teacher3 : AbstractTeacher
    {
        override public void testWork(Work work)
        {
            if (testing == null || testing.IsAlive == false)
            {
                testing = new Thread(delegate ()
                {
                    Thread.Sleep(3000);
                    Random rand = new Random();
                    work.Mark = rand.Next(1, 5);
                    Console.WriteLine("3rd teacher: The work was tested. " + work.Author + " had " + work.Mark + " points");
                });
                testing.Start();
            }
            else
            {
                Console.WriteLine("Out of 3rd!");
                if (_successor != null)
                {
                    _successor.testWork(work);
                }
                else
                {
                    throw new ApplicationException("ChainOfResponsibility  object exhausted all successors without call being handled.");
                }
            }
        }
    }

    //this class holds a work to prevent overflowing the stack
    public class Chain : AbstractTeacher
    {
        override public void testWork(Work work) {
            Thread.Sleep(100);
            _successor.testWork(work);
        }
    }
}
