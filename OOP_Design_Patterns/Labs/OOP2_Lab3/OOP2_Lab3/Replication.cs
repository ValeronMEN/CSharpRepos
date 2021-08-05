using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OOP2_Lab3
{
    [Serializable]
    class Page
    {
        public int pageNumber;
        public string text;
    }

    [Serializable]
    public class Book : ICloneable
    {
        private String SKU;
        private String name;
        private String author;
        private int numberOfPages;
        private Page[] pages;
        private string issueSeries;
        private string publishingHouse;
        private int issueNumber;
        
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

        public String getName()
        {
            return name;
        }

        public String getSKU()
        {
            return SKU;
        }

        public void setSKU(String _sku)
        {
            SKU = _sku;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public String getAuthor()
        {
            return author;
        }

        public void setAuthor(String author)
        {
            this.author = author;
        }

        public void getPages()
        {
            for (int i = 0; i < numberOfPages; i++)
            {
                Console.WriteLine(pages[i].text);
            }
            Console.WriteLine(" ");
        }

        public void setPages(string [] pagesArr)
        {
            int length = pagesArr.Length;
            if (length <= 0)
            {
                throw new Exception();
            }
            this.numberOfPages = length;
            pages = new Page[length];
            for (int i = 0; i< length; i++)
            {
                pages[i] = new Page();
                pages[i].pageNumber = i + 1;
                pages[i].text = pagesArr[i];
            }
        }

        public String getIssueSeries()
        {
            return issueSeries;
        }

        public int getIssueNumber()
        {
            return issueNumber;
        }

        public void setSeriesAndNumberOfIssue(String series, int number)
        {
            this.issueNumber = number;
            this.issueSeries = series;
        }

        public string getPublishingHouse()
        {
            return publishingHouse;
        }

        public void setPublishingHouse(string house)
        {
            publishingHouse = house;
        }

        public void getInfo()
        {
            Console.WriteLine("Name: "+name+"; Author: "+author);
            Console.WriteLine("Publishing house: " + publishingHouse + "; Series: " + issueSeries+"; Issue number: "+issueNumber+";");
            Console.WriteLine("Number of pages: " + numberOfPages + "; Text:\n");
            this.getPages();
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
    }

    public class ProductCache
    {
        private static Hashtable bookMap = new Hashtable();

        public static Book getBook(String bookCode)
        {
            Book cachedBook = (Book)bookMap[bookCode];
            //return (Book)cachedBook.Clone();
            return (Book)cachedBook.DeepCopy();
        }

        public static void loadCache()
        {
            Book b1 = new Book();
            string [] text1 = { "hi", "bye", "i'm good" };
            b1.setPages(text1);
            b1.setName("Oliver Twist");
            b1.setAuthor("Charles Dickens");
            b1.setSKU("B1");
            b1.setPublishingHouse("Valerooon-man");
            b1.setSeriesAndNumberOfIssue("sd89", 89);
            bookMap[b1.getSKU()] = b1;

            Book b2 = new Book();
            string [] text2 = { "damn", "i'm", "good" };
            b2.setPages(text2);
            b2.setName("The Sea Wolf");
            b2.setAuthor("Jack London");
            b2.setSKU("B2");
            b2.setPublishingHouse("Valerooon-man");
            b2.setSeriesAndNumberOfIssue("hfhfhhg", 112);
            bookMap[b2.getSKU()] = b2;

            Book b3 = new Book();
            string [] text3 = { "my", "life" };
            b3.setPages(text3);
            b3.setName("The call of the wild");
            b3.setAuthor("Jack London");
            b3.setSKU("B3");
            b3.setPublishingHouse("Valerooon-man");
            b3.setSeriesAndNumberOfIssue("ere34", 11);
            bookMap[b3.getSKU()] = b3;

            Book b4 = new Book();
            string[] text4 = { "my2", "life2" };
            b4.setPages(text4);
            b4.setName("The call of the wild");
            b4.setAuthor("Jack London NN");
            b4.setSKU("B4");
            b4.setPublishingHouse("Valerooon-man");
            b4.setSeriesAndNumberOfIssue("ere34", 11);
            bookMap[b4.getSKU()] = b4;
        }
    }
}
