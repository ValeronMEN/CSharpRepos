using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP2_Lab3
{
    class Food
    {
        private bool frozen;

        public Food()
        {
            frozen = false;
        }

        public void freeze()
        {
            frozen = true;
        }
        
        public void getInfo()
        {
            Console.WriteLine("Frozen: "+frozen);
        }
    }

    class Buyer
    {
        private string name;
        private int money;
        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public Buyer(string name)
        {
            this.name = name;
            money = 0;
        }
    }

    abstract class FurnitureFactory
    {
        public abstract LivingRoomFurniture createLivingRoomFurniture();
        public abstract KitchenFurniture createKitchenFurniture();
        public abstract UpholsteredFurniture createUpholsteredFurniture();
    }

    class ValeronManFactory : FurnitureFactory
    {
        public static int getCost()
        {
            return 10000;
        }

        public override LivingRoomFurniture createLivingRoomFurniture()
        {
            return new Closet();
        }
        public override KitchenFurniture createKitchenFurniture()
        {
            return new PlasticRefrigerator();
        }
        public override UpholsteredFurniture createUpholsteredFurniture()
        {
            return new WoodenCouch();
        }
    }

    class SiciliaFactory : FurnitureFactory
    {
        public static int getCost()
        {
            return 30000;
        }

        public override LivingRoomFurniture createLivingRoomFurniture()
        {
            return new MirrorCloset();
        }
        public override KitchenFurniture createKitchenFurniture()
        {
            return new MetalRefrigerator();
        }
        public override UpholsteredFurniture createUpholsteredFurniture()
        {
            return new Sofa();
        }
    }

    abstract class LivingRoomFurniture
    {

    }

    abstract class KitchenFurniture
    {
        public abstract void freezeFood(Food keep);
    }

    abstract class UpholsteredFurniture
    {
        
    }

    class Closet : LivingRoomFurniture
    {

    }

    class MirrorCloset : LivingRoomFurniture
    {

    }

    class PlasticRefrigerator : KitchenFurniture
    {
        public override void freezeFood(Food keep)
        {
            Console.WriteLine("Waiting 5 seconds...");
            Thread.Sleep(5000);
            keep.freeze();
        }
    }

    class MetalRefrigerator : KitchenFurniture
    {
        public override void freezeFood(Food keep)
        {
            Console.WriteLine("Waiting 2 seconds...");
            Thread.Sleep(2000);
            keep.freeze();
        }
    }

    class WoodenCouch : UpholsteredFurniture
    {

    }

    class Sofa : UpholsteredFurniture
    {

    }

    class Furniture
    {
        private LivingRoomFurniture livRoomFur;
        private KitchenFurniture kitFur;
        private UpholsteredFurniture uphFur;
        FurnitureFactory factory;

        public void createOrder(Buyer buyer, string wish)
        {
            switch (wish)
            {
                case "ValeronManFactory":
                    if (buyer.Money >= ValeronManFactory.getCost())
                    {
                        Console.WriteLine("ValeronManFactory was created");
                        factory = new ValeronManFactory();
                    }
                    else
                    {
                        Console.WriteLine("There's no money for your wish!");
                    }
                    break;
                case "SiciliaFactory":
                    if (buyer.Money >= SiciliaFactory.getCost())
                    {
                        factory = new SiciliaFactory();
                        Console.WriteLine("SicilaFactory was created");
                    }
                    else
                    {
                        Console.WriteLine("There's no money for your wish!");
                    }
                    break;
                default:
                    if (buyer.Money >= SiciliaFactory.getCost())
                    {
                        factory = new SiciliaFactory();
                        Console.WriteLine("SiciliaFactory was created");
                        break;
                    }
                    if (buyer.Money >= ValeronManFactory.getCost())
                    {
                        factory = new ValeronManFactory();
                        Console.WriteLine("ValeronManFactory was created");
                        break;
                    }
                    Console.WriteLine("There's no money for buying!");
                    break;
            }
        }
        
        public void executeOrder()
        {
            if (factory != null)
            {
                livRoomFur = factory.createLivingRoomFurniture();
                kitFur = factory.createKitchenFurniture();
                uphFur = factory.createUpholsteredFurniture();
            }
            else
            {
                Console.WriteLine("Unknown factory!");
            }
        }

        public static void getCosts()
        {
            Console.WriteLine("ValeronManFactory - " + ValeronManFactory.getCost() + "$\n"+
                "SiciliaFactory - " + SiciliaFactory.getCost() + "$");
        }

        public void freezeFood(Food keep)
        {
            if (kitFur != null)
            {
                kitFur.freezeFood(keep);
            }
            else
            {
                Console.WriteLine("Unknown kitchen!");
            }
        }
    }
}