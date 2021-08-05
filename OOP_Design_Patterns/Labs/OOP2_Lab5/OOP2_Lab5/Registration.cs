using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace OOP2_Lab5
{
    //a part of the receiver
    class Profile
    {
        private int id;
        public int Id
        {
            get { return id; }
        }
        private string fullname;
        public string Fullname
        {
            get { return fullname; }
        }
        private DateTime birthdayDate;
        public DateTime BirthdayDate
        {
            get { return birthdayDate; }
        }

        public Profile(int id)
        {
            this.id = id;
        }

        public void setData(string fullname, DateTime birthdayDate)
        {
            this.fullname = fullname;
            this.birthdayDate = birthdayDate;
        }

        public void cleanData()
        {
            this.fullname = null;
            this.birthdayDate = new DateTime();
        }

        public void display()
        {
            Console.Write("Id: "+ id);
            if (fullname != null)
            {
                Console.Write("; Full name: " + fullname + "; Birthday: "+ birthdayDate);
            }
            Console.WriteLine();
        }
    }

    //invoker
    class User
    {
        public void setExecutor(FillingFieldsCommandAbstract t, AbstractSystem e)
        {
            t._Executor = e;
        }

        public void execute(FillingFieldsCommandAbstract t, string fullname, DateTime birthdate, int id)
        {
            t.execute(fullname, birthdate, id);
        }

        public void unExecute(FillingFieldsCommandAbstract t, int id)
        {
            t.unExecute(id);
        }
    }

    //command
    abstract class FillingFieldsCommandAbstract
    {
        protected AbstractSystem executor;
        public AbstractSystem _Executor
        {
            set { executor = value; }
        }

        abstract public void execute(string fullname, DateTime birthdate, int id);
        abstract public void unExecute(int id);
    }

    //concrete command
    class FillingFieldsCommand : FillingFieldsCommandAbstract
    {
        override public void execute(string fullname, DateTime birthdate, int id)
        {
            if (executor != null)
            {
                executor.fillFields(fullname, birthdate, id);
            }
            else
            {
                throw new Exception();
            }
        }

        override public void unExecute(int id)
        {
            if (executor != null)
            {
                executor.cleanFields(id);
            }
            else
            {
                throw new Exception();
            }
        }
    }

    //receiver; executor
    abstract class AbstractSystem
    {
        abstract public void createProfile(int id);
        abstract public void fillFields(string fullname, DateTime birthdate, int id);
        abstract public void cleanFields(int id);
        abstract public void display();
    }

    class System : AbstractSystem
    {
        private List<Profile> list;

        public System()
        {
            list = new List<Profile>();
        }
        
        override public void createProfile(int id)
        {
            Profile profile = new Profile(id);
            list.Add(profile);
        }

        override public void fillFields(string fullname, DateTime birthdate, int id)
        {
            foreach(Profile prof in list)
            {
                if (prof.Id == id)
                {
                    prof.setData(fullname, birthdate);
                    return;
                }
            }
            throw new Exception();
        }

        override public void cleanFields(int id)
        {
            foreach (Profile prof in list)
            {
                if (prof.Id == id)
                {
                    prof.cleanData();
                    return;
                }
            }
            throw new Exception();
        }

        override public void display()
        {
            Console.WriteLine("There're profiles:");
            foreach (Profile prof in list)
            {
                prof.display();
            }
        }
    }
}
