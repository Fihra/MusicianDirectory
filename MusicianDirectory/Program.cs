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
        //static async Task ShowNames(string[] args)
        {
            var client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("MusicianDB");
            var collection = db.GetCollection<Musician>("Musicians");
            var filter = Builders<Musician>.Filter.Empty;

            var result = collection.Find(filter).ToList();

            foreach(var mus in result)
            {
                Console.WriteLine("{0} -> {1}", mus.YearsOfExp, mus.YearsOfExp += 2);
            }

            //await collection.Find(FilterDefinition<BsonDocument>.Empty).ForEachAsync(doc => Console.WriteLine(doc));
            //await collection.Find(filter).ForEachAsync(doc => Console.WriteLine(doc));

            //await collection.Find(musician => musician.YearsOfExp > 4).ForEachAsync(musician => Console.WriteLine("{0} played {1} for {2} years.", musician.Name, musician.Instrument, musician.YearsOfExp));



            //var query =
            //    from mus in collection.AsQueryable<Musician>()
            //    where mus.Name == "Fabian"
            //    select mus;

            //foreach(var musician in query)
            //{
            //    Console.WriteLine(musician);
            //}


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
            //ShowNames();

            Console.ReadLine();
        }

    }
}
