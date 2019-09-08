namespace JrpShared.Data
{
    public sealed class Session : ISession
    {
        public ICharacter Character { get; set; }
        public PlayerState State { get; set; }
        public bool IsNameHidden { get; set; }

        public Session(ICharacter character, PlayerState state, bool isNameHidden = false)
        {
            Character = character;
            State = state;
            IsNameHidden = isNameHidden;
        }
    }
}
