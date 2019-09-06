namespace JrpShared.Data
{
    public sealed class Session : ISession
    {
        public ICharacter Character { get; set; }
        public PlayerState State { get; set; }
        public bool IsFirstLogin { get; }

        public Session(ICharacter character, PlayerState state, bool isFirstLogin)
        {
            Character = character;
            State = state;
            IsFirstLogin = isFirstLogin;
        }
    }
}
