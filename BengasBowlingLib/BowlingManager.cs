using System;
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
        private readonly ITimePeriodRepository _timePeriodRepository;

        public BowlingManager(
            IPartyRepository partyRepository, 
            ICompetitionRepository competitionRepository, 
            IMatchRepository matchRepository,
            ITimePeriodRepository timePeriodRepository)
        {
            _partyRepository = partyRepository;
            _competitionRepository = competitionRepository;
            _matchRepository = matchRepository;
            _timePeriodRepository = timePeriodRepository;
        }

        public void CreateParty(string name, string legalId) => _partyRepository.Create(name, legalId);

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

        public void AddMatchToCompetition(int competitionId, Match match) => _competitionRepository.AddMatch(competitionId, match);

        public void CreateMatch(List<Party> players, TimePeriod timePeriod, Lane lane, Competition competition = null)
        {
            if (competition == null)
                _matchRepository.Create(players, timePeriod, lane);
            else
                _matchRepository.Create(players, timePeriod, lane, competition);
        }

        public List<Party> GetMatchCompetitors(int matchId) => _matchRepository.GetCompetitors(matchId);

        public List<Match> GetAllMatches() => _matchRepository.All();

        public void CreateTimePeriod(DateTime fromDate, DateTime toDate) => _timePeriodRepository.Create(fromDate, toDate);

        public List<TimePeriod> GetAllTimePeriods() => _timePeriodRepository.All();
    }
}
