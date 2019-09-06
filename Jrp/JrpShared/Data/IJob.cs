namespace JrpShared.Data
{
    public interface IJob
    {
        string Title { get; }
        bool IsEMS { get; }
        bool IsPolice { get; }
        uint Pay { get; }
    }
}
