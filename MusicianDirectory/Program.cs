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
        static async Task ShowNames(string[] args)
        {
            var client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("MusicianDB");
            var collection = db.GetCollection<BsonDocument>("Musicians");

            var list = await collection.Find(new BsonDocument()).ToListAsync();

            foreach(var mus in list)
            {
                Console.WriteLine(mus);
            }

          
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

            Console.WriteLine("Amount of Musicians in this database: {0}", totalMusicians);

            ShowNames(args).Wait();

            Console.ReadLine();
        }

    }
}
