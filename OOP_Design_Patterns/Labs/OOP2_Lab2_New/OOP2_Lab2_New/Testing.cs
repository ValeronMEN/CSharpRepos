using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2_Lab2_New
{
    class Student
    {
        private string name;
        public string Name
        {
            get { return name; }
        }
        private int[] marks;
        public int[] Marks
        {
            get { return marks;  }
        }

        public Student(string name)
        {
            this.name = name;
            marks = new int[4];
            for (int i=0; i<4; i++)
            {
                marks[i] = 0;
            }
        }

        public bool setMark(int module, int mark)
        {
            if (mark > 10 || mark < 0)
            {
                return false;
            }
            try
            {
               this.marks[(module - 1)] = mark;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                throw new ArgumentOutOfRangeException("Index parameter is out of range!", e);
            }
            return true;
        }

        public void display()
        {
            Console.WriteLine("Name: {0}", name);
            for(int i=0; i<4; i++)
            {
                if (this.marks[i] != 0)
                {
                    Console.WriteLine("{0} module - {1} points", (i+1), marks[i]);
                }
            }
        }
    }

    abstract class AbstractModule
    {
        abstract public void passTheModule(Student student, int module);
    }

    class Module : AbstractModule
    {
        public override void passTheModule(Student student, int module)
        {
            Random random = new Random();
            int mark = random.Next(0, 10);
            student.setMark(module, mark);
            if (mark < (10 * 0.6))
            {
                Console.WriteLine("{0}, today, you didn't pass {1} module", student.Name, module);
            }
            else
            {
                Console.WriteLine("{0}, today, you passed {1} module", student.Name, module);
            }
        }
    }

    class ProtectedModule : AbstractModule
    {
        Module moduleClass = new Module();

        public override void passTheModule(Student student, int module)
        {
            int[] arr = student.Marks;
            for (int i=0; i<(module-1); i++)
            {
                if (arr[i] < (10 * 0.6))
                {
                    Console.WriteLine("You didn't pass {0} module!", (i+1));
                    return;
                }
            }
            moduleClass.passTheModule(student, module);
        }
    }
}
