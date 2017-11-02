using BengansBowlingInterfaceLib;

namespace BengansBowlingDbLib.AbstractCompetitionFactory
{
    public class KnockoutCompetitionFactory : AbsctractCompetitionFactory
    {
        public override ICompetition CreateCompetition()
        {
            return new KnockoutCompetition();
        }
    }
}
