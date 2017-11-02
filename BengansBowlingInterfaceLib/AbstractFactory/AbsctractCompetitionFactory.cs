using System;
using System.Collections.Generic;
using System.Text;

namespace BengansBowlingInterfaceLib.AbstractFactory
{
    public abstract class AbsctractCompetitionFactory
    {
        public abstract ICompetition CreateCompetition();
    }
}
