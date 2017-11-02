using BengansBowlingInterfaceLib;

namespace BengansBowlingDbLib.AbstractCompetitionFactory
{
    public class KnockoutCompetition : ICompetition
    {
        public string GameMode => "Winner will advance to next round";
        public string Rules => "some rules";
    }
}
