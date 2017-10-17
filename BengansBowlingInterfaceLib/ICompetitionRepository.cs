using System.Collections.Generic;
using AccountabilityLib;
using BengasBowlingLib;

namespace BengansBowlingInterfaceLib
{
    public interface ICompetitionRepository
    {
        List<Competition> GetAllCompetitions();
        List<Competition> GetCompetitions(string term);
        Party GetCompetitionWinner(int competitionId);
        List<Party> GetCompetitors(int competitionId);
    }
}
