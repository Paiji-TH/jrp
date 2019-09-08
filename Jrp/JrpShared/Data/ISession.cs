namespace JrpShared.Data
{
    public enum PlayerState
    {
        Connecting,
        Connected,
    }

    public interface ISession
    {
        ICharacter Character { get; set; }
        PlayerState State { get; set; }
        bool IsNameHidden { get; set; }
    }
}
