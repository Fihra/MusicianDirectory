using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MusicianDirectory
{
    class Musician
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Instrument { get; set; }

        public Musician(string name, string instrument)
        {
            this.Name = name;
            this.Instrument = instrument;
        }
    }
}
