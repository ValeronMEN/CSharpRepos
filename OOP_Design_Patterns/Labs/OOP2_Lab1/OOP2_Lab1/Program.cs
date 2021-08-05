using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Camera test: (type any key)");
            Console.ReadKey();
            Console.WriteLine();

            #region Camera test
            Camera cam = new Camera();
            cam.takePhoto(LightingLevel.Low, FlashButton.AutoSet);
            cam.takePhoto(LightingLevel.High, FlashButton.On);
            cam.takePhoto(LightingLevel.High, FlashButton.Off);
            cam.takePhoto(LightingLevel.High, FlashButton.AutoSet);
            cam.takePhoto(LightingLevel.Medium, FlashButton.AutoSet);
            Console.ReadKey();
            #endregion

            Console.Write("\nEconomic merchandise test: (type any key)");
            Console.ReadKey();
            Console.WriteLine("\n");

            #region Merchandise test
            EconomicMerchandise em1 = new EconomicMerchandise("TitaniumHole", 13.2, Sections.A, 17);
            Decorator D1 = new BucketDecorator(0.5, 0.6, 0.4);
            D1.setMerchandiseType(em1);
            D1.viewInfo();
            int d1amount = 14;
            double d1price = D1.buy(d1amount);
            Console.Write("Amount: " + d1amount + "\nPaid price: " + d1price + "\n\n");
            EconomicMerchandise em2 = new EconomicMerchandise("SoftSkinnyRag", 13, Sections.B, 1);
            Decorator D2 = new SpongeDecorator(0.5, 0.6, 0.4);
            D2.setMerchandiseType(em2);
            D2.viewInfo();
            int d2amount = 10;
            double d2price = D2.buy(d2amount);
            Console.Write("Amount: " + d2amount + "\nPaid price: " + d2price + "\n\n");
            #endregion
        }
    }
}
