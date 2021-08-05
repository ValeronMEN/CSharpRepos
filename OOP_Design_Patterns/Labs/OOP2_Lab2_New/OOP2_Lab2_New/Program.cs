using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2_Lab2_New
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Marks
            MarkStringTransformer mt1 = new MarkTransformerNewSystem();
            mt1.getHandwritten(34);
            #endregion

            #region Testing
            Student valera = new Student("Valera");
            AbstractModule am1 = new Module();
            am1.passTheModule(valera, 1);
            am1.passTheModule(valera, 3);
            valera.display();
            AbstractModule am2 = new ProtectedModule();
            am2.passTheModule(valera, 4);
            am2.passTheModule(valera, 2);
            valera.display();
            #endregion
        }
    }
}
