using System;
using System.Collections.Generic;
using System.Text;

namespace BengansBowlingInterfaceLib.AbstractFactory
{
    public class KnockoutCompetition : ICompetition
    {
        public string GameMode => "Winner will advance to next round";
        public string Rules => "some rules";
    }
}
