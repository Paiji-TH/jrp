namespace JrpShared.Data
{
    public sealed class Job : IJob
    {
        public string Title { get; }
        public bool IsEMS { get; }
        public bool IsPolice { get; }
        public uint Pay { get; }

        public Job(string title, bool isEMS, bool isPolice, uint pay)
        {
            Title = title;
            IsEMS = isEMS;
            IsPolice = isPolice;
            Pay = pay;
        }
    }
}
