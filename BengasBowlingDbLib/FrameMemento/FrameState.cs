namespace BengansBowlingDbLib.FrameMemento
{
    public class FrameState
    {
        private string _currentThrow;
        internal string PreviousThrow;
        internal string SecondPreviousThrow;

        public FrameState()
        {
            _currentThrow = "None";
            PreviousThrow = "None";
            SecondPreviousThrow = "None";
        }

        public virtual string CurrentThrow
        {
            set
            {
                SecondPreviousThrow = PreviousThrow;
                PreviousThrow = _currentThrow;
                _currentThrow = value;
            }
            get => CurrentThrow;
        }
    }
}
