namespace JrpShared.Data
{
    public interface IItem
    {
        string Name { get; }
        uint Value { get; }
        bool IsLegal { get; }
        bool CanBeLooted { get; }
    }
}
