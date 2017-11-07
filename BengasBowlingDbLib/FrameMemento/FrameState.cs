namespace BengansBowlingDbLib.FrameMemento
{
    public class FrameState
    {
        private string _currentThrow;
        private string _previousThrow;
        private string _secondPreviousThrow;

        public FrameState()
        {
            _currentThrow = "None";
            _previousThrow = "None";
            _secondPreviousThrow = "None";
        }

        public virtual string CurrentThrow
        {
            set
            {
                _secondPreviousThrow = PreviousThrow;
                _previousThrow = _currentThrow;
                _currentThrow = value;
            }
            get => CurrentThrow;
        }

        public virtual string PreviousThrow
        {
            get => _previousThrow;
        }
        public virtual string SecondPreviousThrow
        {
            get => _secondPreviousThrow;
        }
    }
}
