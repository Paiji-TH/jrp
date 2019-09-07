using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JrpShared.Data
{
    public sealed class Character : ICharacter
    {
        public string Name { get; }
        public uint Cash { get; set; }
        public uint Credit { get; set; }
        public IJob Job { get; set; }
        public ICollection<IItem> Inventory { get; set; }
        public ISkin Skin { get; set; }

        [JsonConstructor]
        public Character(string name, uint cash, uint credit, IJob job, ICollection<IItem> inventory, ISkin skin)
        {
            Name = name;
            Cash = cash;
            Credit = credit;
            Job = job;
            Inventory = inventory;
            Skin = skin;
        }

        public Character(string name, IJob job, ISkin skin)
        {
            Name = name;
            Cash = 0;
            Credit = 0;
            Job = job;
            Inventory = new Collection<IItem>();
            Skin = skin;
        }
    }
}
