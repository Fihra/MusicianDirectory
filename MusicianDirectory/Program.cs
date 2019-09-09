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

        static void MainMenu()
        {
            bool isLooping = true;
            do
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("[V]iew all musicians");
                Console.WriteLine("[S]earch directory");
                Console.WriteLine("[Q]uit");
                Console.Write("Enter Choice: ");
                string userInput = Console.ReadLine();
                Console.WriteLine("-----------------");

                switch (userInput.ToLower())
                {
                    case "v":
                        Console.WriteLine("Viewing all musicians");
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

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Musician's Directory!");
            //Establish Connection to MusicianDB
            var client = new MongoClient("mongodb://localhost:27017/MusicianDB");
            //Get Database, if it doesn't exist, create it
            var database = client.GetDatabase("MusicianDB");

            //Get Collection, it it doesn't exist, create it
            var musicianCollection = database.GetCollection<Musician>("Musicians", null);

            //Drop Database
            database.DropCollection("Musicians");

            //New entry
            musicianCollection.InsertOne(new Musician("Fabian", "Laud", 5));

            //Multiple entries
            Musician gary = new Musician("Gary", "Flute", 8);
            Musician shara = new Musician("Shara", "Cello", 15);
            musicianCollection.InsertMany(new List<Musician> { gary, shara });

            //count of existing records
            int totalMusicians = musicianCollection.AsQueryable().Count();

            //Console.WriteLine("Amount of Musicians in this database: {0}", totalMusicians);

            MainMenu();
            Console.WriteLine("Thank you for using the MusicianDirectory");
            //ShowNames(args).Wait();

            Console.ReadLine();
        }

    }
}
