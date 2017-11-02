using System;
using System.Collections.Generic;
using System.Text;

namespace BengansBowlingInterfaceLib.AbstractFactory
{
    public class GroupCompetition : ICompetition
    {
        public string GameMode => "Players will all face eachother once. Player with highest amount of total wins will be champion";
        public string Rules => "some rules";
    }
}
