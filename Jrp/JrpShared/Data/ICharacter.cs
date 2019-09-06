using System.Collections.Generic;

namespace JrpShared.Data
{
    public interface ICharacter
    {
        string Name { get; }
        uint Cash { get; set; }
        uint Credit { get; set; }
        IJob Job { get; set; }
        ICollection<IItem> Inventory { get; set; }
        ISkin Skin { get; set; }
    }
}
