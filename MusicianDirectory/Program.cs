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
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Musician's Directory!");
            //Console.ReadKey();

            var client = new MongoClient("mongodb://localhost:27017/MusicianDB");
            var database = client.GetDatabase("MusicianDB");
            Console.Out.WriteLine("Connection has been established.");

            var musicianCollection = database.GetCollection<Musician>("Musicians", null);

            database.DropCollection("Musicians");

            //New entry
            musicianCollection.InsertOne(new Musician("Fabian", "Laud"));

            Musician gary = new Musician("Gary", "Flute");
            Musician shara = new Musician("Shara", "Cello");
            musicianCollection.InsertMany(new List<Musician> { gary, shara });

            //count of existing records
            int totalMusicians = musicianCollection.AsQueryable().Count();

            Console.ReadLine();

        }

    }
}
