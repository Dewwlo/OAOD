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
        private readonly IPlayerRepository _playerRepository;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly ITimePeriodRepository _timePeriodRepository;
        private readonly ISeriesRepository _seriesRepository;

        public BowlingManager(
            IPlayerRepository playerRepository, 
            ICompetitionRepository competitionRepository, 
            IMatchRepository matchRepository,
            ITimePeriodRepository timePeriodRepository,
            ISeriesRepository seriesRepository)
        {
            _playerRepository = playerRepository;
            _competitionRepository = competitionRepository;
            _matchRepository = matchRepository;
            _timePeriodRepository = timePeriodRepository;
            _seriesRepository = seriesRepository;
        }

        public void CreatePlayer(string name, string legalId) => _playerRepository.Create(name, legalId);

        public List<Player> GetAllParties => _playerRepository.All();

        public void CreateCompetition(string name, string type, decimal? winnerPriceSum  = null)
        {
            if (winnerPriceSum == null)
                _competitionRepository.Create(name, type);
            else
                _competitionRepository.Create(name, type, (decimal)winnerPriceSum);
        }

        public void AddPlayerToCompetition(int competitionId, Player player) => _competitionRepository.AddCompetitor(competitionId, player);

        public List<Competition> GetAllCompetitions() => _competitionRepository.All();

        public List<Player> GetAllCompetitionCompetitors(int matchId) => _competitionRepository.GetCompetitors(matchId);

        public void AddMatchToCompetition(int competitionId, Match match) => _competitionRepository.AddMatch(competitionId, match);

        public void CreateMatch(List<Player> players, TimePeriod timePeriod, Lane lane, Competition competition = null)
        {
            if (competition == null)
                _matchRepository.Create(players, timePeriod, lane);
            else
                _matchRepository.Create(players, timePeriod, lane, competition);
        }

        public List<Player> GetMatchCompetitors(int matchId) => _matchRepository.GetCompetitors(matchId);

        public List<Match> GetAllMatches() => _matchRepository.All();

        public void CreateTimePeriod(DateTime fromDate, DateTime toDate) => _timePeriodRepository.Create(fromDate, toDate);

        public List<TimePeriod> GetAllTimePeriods() => _timePeriodRepository.All();

        public void CreateSeries(Player player, Match match) => _seriesRepository.Create(player, match);

        public void AddSeriesScore(int seriesId, int score) => _seriesRepository.AddScore(seriesId, score);

        public Player GetMatchWinner(int matchId) => _matchRepository.Winner(matchId);

        public int GetSeriesScore(int [,] scoreArray = null, int[,] frameTen = null) => _seriesRepository.CalculateSeriesScore(scoreArray, frameTen);
        public Player GetYearChampion(int year) => _matchRepository.YearChampion(year);
    }
}
