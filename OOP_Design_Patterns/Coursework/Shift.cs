using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coursework
{
    [Serializable]
    public class Shift : ICloneable
    {
        private int number;
        private string month;
        private int year;
        private int exp;
        private String SKU;

        public Shift(int year, int number, string month)
        {
            this.year = year;
            this.month = month;
            this.number = number;
            exp = 0;
        }

        public Object Clone()
        {
            Object clone = null;
            try
            {
                clone = this.MemberwiseClone();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return clone;
        }

        public object DeepCopy()
        {
            object figure = null;
            using (MemoryStream tempStream = new MemoryStream())
            {
                BinaryFormatter binFormatter = new BinaryFormatter(null,
                    new StreamingContext(StreamingContextStates.Clone));

                binFormatter.Serialize(tempStream, this);
                tempStream.Seek(0, SeekOrigin.Begin);

                figure = binFormatter.Deserialize(tempStream);
            }
            return figure;
        }

        public String getSKU()
        {
            return SKU;
        }

        public void setSKU(string SKU)
        {
            this.SKU = SKU;
        }

        public void setYear(int year)
        {
            this.year = year;
        }

        public int getNumber()
        {
            return number;
        }

        public int getYear()
        {
            return year;
        }

        public String getMonth()
        {
            return month;
        }

        public void setExp(int exp)
        {
            this.exp = exp;
        }

        public int getExp()
        {
            return exp;
        }

        public void start()
        {
            ShiftFunctions sf = new ShiftFunctions();
            int result = sf.start();
            if (result != 0)
            {
                Console.WriteLine("Camp managing was finished! Congratulations!");
                Console.ReadKey();
                Console.Clear();
                this.setExp(result);
            }
            else
            {
                Console.WriteLine("Camp managing was terminated"); //Results wasn't saved
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    public class ShiftCache
    {
        private static Hashtable bookMap = new Hashtable();
        private static int maxShiftsValue = 5;

        public static int getMaxShiftsValue()
        {
            return maxShiftsValue;
        }

        public static Shift getShift(String bookCode)
        {
            Shift cachedBook = (Shift)bookMap[bookCode];
            return (Shift)cachedBook.DeepCopy();
        }

        public static void loadCache()
        {
            int year = 0;

            Shift june = new Shift(year, 1, "June");
            june.setSKU("1");
            bookMap[june.getSKU()] = june;

            Shift july = new Shift(year, 2, "July");
            july.setSKU("2");
            bookMap[july.getSKU()] = july;

            Shift august1 = new Shift(year, 3, "August");
            august1.setSKU("3");
            bookMap[august1.getSKU()] = august1;

            Shift august2 = new Shift(year, 4, "August");
            august2.setSKU("4");
            bookMap[august2.getSKU()] = august2;
        }
    }
}
