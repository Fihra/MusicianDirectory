﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace MusicianDirectory
{
    class Program
    {
        static void Musicdb()
        {
            var connectionString = "mongodb://localhost:27017/MusicianDB";
            //Establish Connection to MusicianDB
            var client = new MongoClient(connectionString);
            //Get Database, if it doesn't exist, create it
            var database = client.GetDatabase("MusicianDB");
        }

        static async Task ShowNames(string[] args)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("MusicianDB");
            var collection = db.GetCollection<Musician>("Musicians");
            var filter = Builders<Musician>.Filter.Empty;

            var result = collection.Find(filter).ToList();
            
            
        }
        static void MainMenu()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("[A]ll musicians");
            Console.WriteLine("[N]ew musician");
            Console.WriteLine("[U]pdate musician info");
            Console.WriteLine("[R]emove musician");
            Console.WriteLine("[S]earch directory");
            Console.WriteLine("[Q]uit");
            Console.Write("Enter Choice: ");
        }

        static void AllMusicians()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("MusicianDB");
            var collection = db.GetCollection<Musician>("Musicians");
            var filter = Builders<Musician>.Filter.Empty;

            var result = collection.Find(filter).ToList();
            int totalMusicians = collection.AsQueryable().Count();
            Console.WriteLine("All Musicians");
            if (totalMusicians < 1)
            {
                Console.WriteLine("There are no musicians.");
            }
            else
            {
                foreach (var mus in result)
                {
                    Console.WriteLine("Name: {0}", mus.Name);
                    Console.WriteLine("Main Instrument: {0}", mus.Instrument);
                    Console.WriteLine("Years of Experience: {0}", mus.YearsOfExp);
                    Console.WriteLine("");
                }
            }
        }

        static void NewMusician()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("MusicianDB");
            var collection = db.GetCollection<Musician>("Musicians");
            var filter = Builders<Musician>.Filter.Empty;

            Console.WriteLine("New Musician Form");
            Console.Write("Name: ");
            string nameInput = Console.ReadLine();
            Console.Write("Instrument: ");
            string instrumentInput = Console.ReadLine();
            Console.Write("Years of Experience: ");
            string yearsOfExpInput = Console.ReadLine();
            int yearsOfExpInputconverted = Convert.ToInt32(yearsOfExpInput);

            collection.InsertOne(new Musician(nameInput, instrumentInput, yearsOfExpInputconverted));
        }

        static void UpdateMusician()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("MusicianDB");
            var collection = db.GetCollection<Musician>("Musicians");
            var filter = Builders<Musician>.Filter.Empty;

            var result = collection.Find(filter).ToList();
            int totalMusicians = collection.AsQueryable().Count();
            Console.WriteLine("Update Musician Info");
            if (totalMusicians < 1)
            {
                Console.WriteLine("There are no musicians to update.");
            }
            else
            {
                int choices = 1;

                List<Musician> musicians = new List<Musician>();
                foreach (var mus in result)
                {
                    Console.WriteLine("[{0}]", choices);
                    Console.WriteLine("Name: {0}", mus.Name);
                    Console.WriteLine("Main Instrument: {0}", mus.Instrument);
                    Console.WriteLine("Years of Experience: {0}", mus.YearsOfExp);
                    Console.WriteLine("");
                    musicians.Add(mus);
                    choices++;
                }
                Console.Write("Select choice: ");
                int choiceInput = Convert.ToInt32(Console.ReadLine());

                var musicianSelection = musicians[choiceInput - 1];

                Console.WriteLine("Name: {0}", musicianSelection.Name);
                Console.WriteLine("Main Instrument: {0}", musicianSelection.Instrument);
                Console.WriteLine("Years of Experience: {0}", musicianSelection.YearsOfExp);
                bool isLooping = true;
                do
                {
                    Console.WriteLine("What would you like to change? [N]ame, [I]nstrument, [Y]ears of Experience, [B]ack");
                    Console.Write("Choice: ");
                    string userInput = Console.ReadLine();
                    switch (userInput.ToLower())
                    {
                        case "n":
                            Console.Write("Enter new name: ");
                            string newNameInput = Console.ReadLine();
                            var nameFilter = Builders<Musician>.Filter.Eq("_id", musicianSelection.Id);
                            var update = Builders<Musician>.Update.Set("Name", newNameInput);
                            collection.UpdateOne(nameFilter, update);
                            break;
                        case "i":
                            Console.Write("Enter new instrument: ");
                            string newInstrumentInput = Console.ReadLine();
                            break;
                        case "y":
                            Console.Write("Enter new # of years: ");
                            int newYearsInput = Convert.ToInt32(Console.ReadLine());
                            break;
                        case "b":
                            isLooping = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                } while (isLooping);
                
            }
        }

        static void RemoveMusician()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("MusicianDB");
            var collection = db.GetCollection<Musician>("Musicians");
            var filter = Builders<Musician>.Filter.Empty;

            var result = collection.Find(filter).ToList();
            int totalMusicians = collection.AsQueryable().Count();
            Console.WriteLine("Removing Musician");
            if (totalMusicians < 1)
            {
                Console.WriteLine("There are no musicians to delete.");
            }
            else
            {
                int choices = 1;

                List<Musician> musicians = new List<Musician>();
                foreach (var mus in result)
                {
                    Console.WriteLine("[{0}]", choices);
                    Console.WriteLine("Name: {0}", mus.Name);
                    Console.WriteLine("Main Instrument: {0}", mus.Instrument);
                    Console.WriteLine("Years of Experience: {0}", mus.YearsOfExp);
                    Console.WriteLine("");
                    musicians.Add(mus);
                    choices++;
                }
                Console.Write("Select choice: ");
                int choiceInput = Convert.ToInt32(Console.ReadLine());

                var musicianSelection = musicians[choiceInput - 1];

                Console.WriteLine(musicianSelection.Name);

                collection.DeleteOne(m => m.Id == musicianSelection.Id);
                musicians.Remove(musicianSelection);
            }
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
                        AllMusicians();
                        break;
                    case "n":
                        NewMusician();
                        break;
                    case "u"://TODO
                        UpdateMusician();
                        break;
                    case "r":
                        RemoveMusician();
                        break;
                    case "s"://TODO
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
