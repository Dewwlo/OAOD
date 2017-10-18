using System.Collections.Generic;
using AccountabilityInterfacesLib;
using AccountabilityLib;
using BengansBowlingInterfaceLib;

namespace BengansBowlingLib
{
    public class BowlingManager
    {
        private readonly IPartyRepository _partyRepository;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IMatchRepository _matchRepository;

        public BowlingManager(IPartyRepository partyRepository, ICompetitionRepository competitionRepository, IMatchRepository matchRepository)
        {
            _partyRepository = partyRepository;
            _competitionRepository = competitionRepository;
            _matchRepository = matchRepository;
        }

        public void CreateParty(string name, string legalId)
        {
            _partyRepository.Create(name, legalId);
        }

        public int PartyCount => _partyRepository.All().Count;

        public void CreateCompetition(string name, decimal? winnerPriceSum)
        {
            if (winnerPriceSum == null)
                _competitionRepository.Create(name);
            else
                _competitionRepository.Create(name, (decimal)winnerPriceSum);
        }

        public void CreateMatch(List<Party> players, TimePeriod timePeriod, int laneId, int? competitionId)
        {
            if (competitionId == null)
                _matchRepository.Create(players, timePeriod, laneId);
            else
                _matchRepository.Create(players, timePeriod, laneId, (int)competitionId);
        }
    }
}
