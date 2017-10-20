using System.Collections.Generic;
using AccountabilityInterfacesLib;
using AccountabilityLib;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

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

        public List<Party> GetAllParties => _partyRepository.All();

        public void CreateCompetition(string name, decimal? winnerPriceSum  = null)
        {
            if (winnerPriceSum == null)
                _competitionRepository.Create(name);
            else
                _competitionRepository.Create(name, (decimal)winnerPriceSum);
        }

        public void AddPlayerToCompetition(int competitionId, Party party) => _competitionRepository.AddCompetitor(competitionId, party);

        public List<Competition> GetAllCompetitions() => _competitionRepository.All();

        public List<Party> GetAllCompetitionCompetitors(int matchId) => _competitionRepository.GetCompetitors(matchId);

        public void CreateMatch(List<Party> players, TimePeriod timePeriod, Lane lane, Competition competition = null)
        {
            if (competition == null)
                _matchRepository.Create(players, timePeriod, lane);
            else
                _matchRepository.Create(players, timePeriod, lane, competition);
        }

        public List<Party> GetMatchCompetitors(int matchId) => _matchRepository.GetCompetitors(matchId);
    }
}
