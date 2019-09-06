namespace JrpShared.Data
{
    public sealed class Session : ISession
    {
        public ICharacter Character { get; set; }
        public PlayerState State { get; set; }

        public Session(ICharacter character, PlayerState state)
        {
            Character = character;
            State = state;
        }
    }
}
