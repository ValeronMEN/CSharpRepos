using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2_Lab1
{
    enum Sections
    {
        A, B, C
    }

    abstract class Merchandise
    {
        protected string nameOfModel;
        protected double price;
        protected Sections section;
        protected int guaranteePeriodInMonths;

        public abstract double buy(int amount);
        public abstract void viewInfo();
    }

    class EconomicMerchandise : Merchandise
    {
        public EconomicMerchandise(string nameOfModel, double price, Sections section, int guaranteePeriodInMonths)
        {
            this.nameOfModel = nameOfModel;
            this.price = price;
            this.section = section;
            this.guaranteePeriodInMonths = guaranteePeriodInMonths;
        }

        public override double buy(int amount)
        {
            return price * amount;
        }

        public override void viewInfo()
        {
            Console.Write("Model name: " + nameOfModel + "\n" + 
                "Price: " + price + "\n" +
                "Section: " + section + "\n" +
                "Guarantee period (in month): " + guaranteePeriodInMonths + "\n");
        }
    }

    abstract class Decorator : Merchandise
    {
        protected Merchandise merch;

        public void setMerchandiseType(Merchandise baseMerchandise)
        {
            merch = baseMerchandise;
        }
        public override double buy(int amount)
        {
            if (merch != null)
            {
                return merch.buy(amount);
            }
            else
            {
                throw new Exception();
            }
        }
        public override void viewInfo()
        {
            if (merch != null)
            {
                merch.viewInfo();
            }
        }
    }

    class BucketDecorator : Decorator
    {
        private double sale = 0.1;
        private double radiusInMetersLower;
        private double radiusInMetersUpper;
        private double height = 0;

        public BucketDecorator(double radiusInMetersLower, double radiusInMetersUpper, double height)
        {
            this.radiusInMetersLower = radiusInMetersLower;
            this.radiusInMetersUpper = radiusInMetersUpper;
            this.height = height;
        }

        public override double buy(int amount)
        {
            return base.buy(amount) * (1 - sale);
        }

        public override void viewInfo()
        {
            base.viewInfo();
            Console.Write("Sale: " + (sale*100) + "%\n" +
                "Lower radius of bucket (in meters) " + radiusInMetersLower + "\n" +
                "Upper radius of bucket (in meters) " + radiusInMetersUpper + "\n" +
                "Height (in meters): " + height + "\n" +
                "Volume: " + getVolume() + "\n");
        }

        private double getVolume()
        {
            return (Math.PI * height * (Math.Pow(radiusInMetersLower, 2) + radiusInMetersLower * radiusInMetersUpper + Math.Pow(radiusInMetersUpper, 2)) / 3);
        }
    }

    class SpongeDecorator : Decorator
    {
        private string colour = "yellow";
        private double thickness;
        private double height;
        private double width;

        public SpongeDecorator(double height, double width, double thickness)
        {
            this.thickness = thickness;
            this.height = height;
            this.width = width;
        }

        public override void viewInfo()
        {
            base.viewInfo();
            Console.Write("Colour: " + colour + "\n" +
                "Height (in meters) " + height + "\n" +
                "Width (in meters) " + width + "\n" +
                "Thickness (in meters): " + thickness + "\n" +
                "Volume: " + getVolume() + "\n");
        }

        private double getVolume()
        {
            return height * width * thickness;
        }
    }
}
