using System;
using System.Collections.Generic;
using System.Text;

namespace BengansBowlingInterfaceLib.AbstractFactory
{
    public class GroupCompetitionFactory : AbsctractCompetitionFactory
    {
        public override ICompetition CreateCompetition()
        {
            return new GroupCompetition();
        }
    }
}
