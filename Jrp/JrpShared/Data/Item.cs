namespace JrpShared.Data
{
    public sealed class Item : IItem
    {
        public string Name { get; }
        public uint Value { get; }
        public bool IsLegal { get; }
        public bool CanBeLooted { get; }

        public Item(string name, uint value, bool isLegal, bool canBeLooted)
        {
            Name = name;
            Value = value;
            IsLegal = isLegal;
            CanBeLooted = canBeLooted;
        }
    }
}
