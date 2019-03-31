using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XMLProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            //Tova ne ni trqbva ama v sluchai che naprimer imame 1,23 a ne 1.23
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            XDocument doc = XDocument.Load("products.xml");
            var enc = doc.Declaration.Encoding;
            Console.WriteLine(enc + "\n");
            Console.WriteLine(doc.Root.Elements().Count() + "\n");

            var products = doc.Root.Elements().Take(5);

            foreach (var product in products)
            {
                string name = product.Element("name").Value;
                decimal price = decimal.Parse(product.Element("price").Value);

                Console.WriteLine($"{name}: ${price}");
            }

            Console.WriteLine("//-------------------------------------------------------------------");

            var productsAbove1200 = doc.Root.Elements()
                .Where(p => decimal.Parse(p.Element("price").Value) > 1200).Take(5);

            foreach (var product in productsAbove1200)
            {
                string name = product.Element("name").Value;
                decimal price = decimal.Parse(product.Element("price").Value);

                Console.WriteLine($"{name}: ${price}");
            }

            Console.WriteLine("//-------------------------------------------------------------------");

            XDocument doc2 = new XDocument(new XElement("products"));
            doc2.Root.Add(new XElement("product", new XAttribute("type", "medicine"),
                new XElement("name", "Analgin"), new XElement("price", 4.5m)));

            doc2.Save("custom.xml");

            var productAnalgin = doc2.Root.Elements()
                .Where(p => p.Element("name").Value == "Analgin").FirstOrDefault();

            Console.WriteLine($"Name: {productAnalgin.Element("name").Value}, Price: ${productAnalgin.Element("price").Value}");

            Console.WriteLine("//-------------------------------------------------------------------");

            XmlSerializer serializer = new XmlSerializer(typeof(Products));

            using (FileStream stream = new FileStream("products.xml", FileMode.Open))
            {
                var productsFromXml = (Products)serializer.Deserialize(stream);

                var filtered = productsFromXml.Product.Where(p => p.price > 1000);
                Console.WriteLine(filtered.FirstOrDefault(e => e.price > 1200).name);
            }

            Console.WriteLine("//-------------------------------------------------------------------");

            XmlSerializer serializerLibrary = new XmlSerializer(typeof(Library));

            var library = new Library()
            {
                Name = "Centralna",
                Address = "Sofia 1000",
                Books = new List<Book>()
                {
                    new Book()
                    {
                        Author = new Author() { Name = "Koelio", Nationality = "Brazil"},
                        Language = "english",
                        Price = 10,
                        Title = "Alhimika"
                    },
                    new Book()
                    {
                        Author = new Author() { Name = "Orhan Pamuk", Nationality = "Turkey"},
                        Language = "english",
                        Price = 10,
                        Title = "Harun i moreto ot prikazki"
                    }
                }
            };

            using (FileStream stream2 = new FileStream("library.xml", FileMode.Create))
            {
                serializerLibrary.Serialize(stream2, library);
            }
        }
    }
}
