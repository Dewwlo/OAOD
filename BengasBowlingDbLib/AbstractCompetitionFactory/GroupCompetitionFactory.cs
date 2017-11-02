using BengansBowlingInterfaceLib;

namespace BengansBowlingDbLib.AbstractCompetitionFactory
{
    public class GroupCompetitionFactory : AbsctractCompetitionFactory
    {
        public override ICompetition CreateCompetition()
        {
            return new GroupCompetition();
        }
    }
}
