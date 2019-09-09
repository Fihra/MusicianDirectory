using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MusicianDirectory
{
    class Musician
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("instrument")]
        public string Instrument { get; set; }
        [BsonElement("yearsOfExp")]
        public int YearsOfExp { get; set; }

        public Musician(string name, string instrument, int yearsOfExp)
        {
            this.Name = name;
            this.Instrument = instrument;
            this.YearsOfExp = yearsOfExp;
        }
    }
}
