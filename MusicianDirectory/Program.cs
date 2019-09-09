using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Bson;

namespace MusicianDirectory
{
    class Program
    {
        static void MainDirectory()
        {

        }

        static async Task ShowNames(string[] args)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("MusicianDB");
            var collection = db.GetCollection<Musician>("Musicians");
            var filter = Builders<Musician>.Filter.Empty;

            var result = collection.Find(filter).ToList();

            foreach(var mus in result)
            {
                Console.WriteLine("{0} -> {1}", mus.YearsOfExp, mus.YearsOfExp += 2);
            }

        }

        static void ViewMusicians()
        {
               
        }

        static void MainMenu()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("[A]ll musicians");
            Console.WriteLine("[N]ew musician");
            Console.WriteLine("[V]iew musician");
            Console.WriteLine("[U]pdate musician info");
            Console.WriteLine("[R]emove musician");
            Console.WriteLine("[S]earch directory");
            Console.WriteLine("[Q]uit");
            Console.Write("Enter Choice: ");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Musician's Directory!");

            var connectionString = "mongodb://localhost:27017/MusicianDB";
            //Establish Connection to MusicianDB
            var client = new MongoClient(connectionString);
            //Get Database, if it doesn't exist, create it
            var database = client.GetDatabase("MusicianDB");

            //Get Collection, it it doesn't exist, create it
            //var musicianCollection = database.GetCollection<Musician>("Musicians", null);

            //Drop Database
            //database.DropCollection("Musicians");

            //New entry
            //musicianCollection.InsertOne(new Musician("Fabian", "Laud", 5));

            //Multiple entries
            //Musician gary = new Musician("Gary", "Flute", 8);
            //Musician shara = new Musician("Shara", "Cello", 15);
            //musicianCollection.InsertMany(new List<Musician> { gary, shara });

            //count of existing records
            //int totalMusicians = musicianCollection.AsQueryable().Count();

            //Console.WriteLine("Amount of Musicians in this database: {0}", totalMusicians);

            bool isLooping = true;
            do
            {
                MainMenu();
                string userInput = Console.ReadLine();
                Console.WriteLine("-----------------");

                switch (userInput.ToLower())
                {
                    case "a":
                        Console.WriteLine("All musicians");
                        break;
                    case "n":
                        Console.WriteLine("New musician form");
                        break;
                    case "v":
                        Console.WriteLine("View musician");
                        break;
                    case "u":
                        Console.WriteLine("Update musician Info");
                        break;
                    case "r":
                        Console.WriteLine("Remove Musician");
                        break;
                    case "s":
                        Console.WriteLine("Searching directory");
                        break;
                    case "q":
                        Console.WriteLine("Quitting Application");
                        isLooping = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                    }
                } while (isLooping);

            Console.WriteLine("Thank you for using the MusicianDirectory");
            //ShowNames(args).Wait();

            Console.ReadLine();
        }

    }
}
