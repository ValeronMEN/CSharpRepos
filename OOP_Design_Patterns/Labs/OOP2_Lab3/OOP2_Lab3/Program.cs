using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2_Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Furniture
            Buyer valera = new Buyer("Valera");
            Food keep = new Food();
            keep.getInfo();
            valera.Money = 10000;
            Furniture.getCosts();
            Furniture furniture = new Furniture();
            furniture.createOrder(valera, "SiciliaFactory");
            furniture.createOrder(valera, null);
            furniture.executeOrder();
            furniture.freezeFood(keep);
            keep.getInfo();
            #endregion

            Console.WriteLine(" ");

            #region Replication
            ProductCache.loadCache();
            Book clonedBook1 = (Book)ProductCache.getBook("B1");
            clonedBook1.getInfo();
            Book clonedBook2 = (Book)ProductCache.getBook("B1");
            string [] text = { "Death", "Der" };
            clonedBook2.setPages(text);
            clonedBook2.getInfo();
            clonedBook1.getInfo();

            Book clonedBook3 = (Book)ProductCache.getBook("B1");
            clonedBook3.getInfo();
            #endregion
        }
    }
}
