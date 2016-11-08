using System;
using CodeMash.Net;
using PresentConnection.Internship7.Iot.Domain;

namespace ConsoleApplication
{
    [CollectionName("Actors")]
    public class Person : EntityBase
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            CreateManufacturer();

            Console.ReadLine();
        }

        public static void CreateManufacturer()
        {
            var manufacturer = new Manufacturer()
            {
                Name = "Raspberry PI",
                Description = "...",
                IsVisible = true,
                UniqueName = "raspberry-pi",
                Url = "raspberry-pi"
            };

            Db.InsertOne(manufacturer);

            Console.WriteLine($"Manufactuerer inserted into database with id: {manufacturer.Id}");

        }
    }
}
