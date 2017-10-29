using System;
using System.Collections.Generic;
using System.Text;

namespace BengansBowlingLib
{
    public class FrameStateMemento
    {
        private readonly FrameState _frameState;
        private readonly string _copyOfCurrentThrow;
        private readonly string _copyOfPreviousThrow;
        private readonly string _copyOfSecondPreviousThrow;

        public FrameStateMemento(FrameState frameState)
        {
            _frameState = frameState;
            _copyOfCurrentThrow = frameState.CurrentThrow;
            _copyOfPreviousThrow = frameState.PreviousThrow;
            _copyOfSecondPreviousThrow = frameState.SecondPreviousThrow;
        }

        public virtual void RestoreState()
        {
            _frameState.CurrentThrow = _copyOfCurrentThrow;
            _frameState.PreviousThrow = _copyOfPreviousThrow;
            _frameState.SecondPreviousThrow = _copyOfSecondPreviousThrow;
        }
    }
}
