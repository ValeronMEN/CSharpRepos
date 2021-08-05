using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP2_Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Testing
            Work work1 = new Work("some text", "Babenko");
            Work work2 = new Work("some text", "Ivanov");
            Work work3 = new Work("some text", "Flac");
            Work work4 = new Work("some text", "Yella");
            Work work5 = new Work("some text", "Trash");
            AbstractTeacher teacher1 = new Teacher1();
            AbstractTeacher teacher2 = new Teacher2();
            AbstractTeacher teacher3 = new Teacher3();
            AbstractTeacher chain = new Chain();
            teacher1.Successor = teacher2;
            teacher2.Successor = teacher3;
            teacher3.Successor = chain;
            chain.Successor = teacher1;
            teacher1.testWork(work1);
            teacher1.testWork(work2);
            teacher1.testWork(work3);
            teacher1.testWork(work4);
            teacher1.testWork(work5);
            while (true)
            {
                if (work1.Mark != 0 && work2.Mark != 0 && work3.Mark != 0 && work4.Mark != 0 && work5.Mark != 0)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Testing has ended");
                    break;
                }
            }
            #endregion

            Console.WriteLine(" ");

            #region Registration
            System sys = new System();
            sys.createProfile(1);
            sys.createProfile(11);
            User valera = new User();
            User behrang = new User();
            FillingFieldsCommand command = new FillingFieldsCommand();
            valera.setExecutor(command, sys);
            valera.execute(command, "Valera Babenko Pavlovich", new DateTime(), 1);
            valera.unExecute(command, 1);
            valera.setExecutor(command, sys);
            valera.execute(command, "Behrang Behvandi Abdulahovich", new DateTime(), 11);

            sys.display();
            #endregion

            Console.WriteLine(" ");
        }
    }
}
