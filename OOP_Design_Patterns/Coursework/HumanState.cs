using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coursework
{
    abstract class HumanState
    {
        abstract public bool getConflictPossibility(int Exp);
    }

    class DutyState : HumanState
    {
        public DutyState() { }

        public override bool getConflictPossibility(int exp)
        {
            return false;
        }
    }

    class CommonState : HumanState
    {
        public CommonState() { }

        public override bool getConflictPossibility(int exp)
        {
            Random rd = new Random();
            Thread.Sleep(1);
            int rdresult = rd.Next(0, exp);
            if (exp > 100)
            {
                if (rdresult < (int)(exp * (0.1)))
                {
                    return true;
                }
            }
            else if (exp > 1000)
            {
                if (rdresult < (int)(exp * (0.05)))
                {
                    return true;
                }
            }
            else if (exp > 1500)
            {
                if (rdresult < (int)(exp * (0.03)))
                {
                    return true;
                }
            }
            else if (exp > 2000)
            {
                if (rdresult < (int)(exp * (0.02)))
                {
                    return true;
                }
            }
            else if (exp > 3000)
            {
                if (rdresult < (int)(exp * (0.01)))
                {
                    return true;
                }
            }
            else if (exp > 5000)
            {
                if (rdresult < (int)(exp * (0.005)))
                {
                    return true;
                }
            }
            else if (exp > 7000)
            {
                if (rdresult < (int)(exp * (0.001)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
