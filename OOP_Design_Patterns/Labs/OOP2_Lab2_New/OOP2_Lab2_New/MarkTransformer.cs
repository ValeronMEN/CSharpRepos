using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2_Lab2_New
{
    // Adaptee from 100 to 2-5
    class MarkTransformer
    {
        public virtual int transform(int points100)
        {
            int mark;
            if (points100 < 60)
            {
                mark = 2;
            }
            else if (points100 <= 69)
            {
                mark = 3;
            }
            else if (points100 <= 84)
            {
                mark = 4;
            }
            else
            {
                mark = 5;
            }
            //Console.WriteLine("Mark: "+mark);
            return mark;
        }
    }

    // Target; from 60 to String
    class MarkStringTransformer
    {
        public virtual string getHandwritten(int mark)
        {
            string handwrittenmark;
            if (mark == 2)
            {
                handwrittenmark = "two";
            }
            else if (mark == 3)
            {
                handwrittenmark = "three";
            }
            else if (mark == 4)
            {
                handwrittenmark = "four";
            }
            else if (mark == 5)
            {
                handwrittenmark = "five";
            }
            else
            {
                throw new Exception();
            }
            //Console.WriteLine("Mark: "+handwrittenmark);
            return handwrittenmark;
        }
    }

    //Adapter
    class MarkTransformerNewSystem : MarkStringTransformer
    {
        public override string getHandwritten(int points60)
        {
            int points100 = (int)get100from60(points60);
            MarkTransformer mst = new MarkTransformer();
            int mark = mst.transform(points100);
            return base.getHandwritten(mark);
        }

        public int get100from60(int points60)
        {
            return ((points60 * 100) / 60);
        }
    }
}
