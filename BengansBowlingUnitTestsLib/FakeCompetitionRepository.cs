using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BengansBowlingDbLib.AbstractCompetitionFactory;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingUnitTestsLib
{
    public class FakeCompetitionRepository : ICompetitionRepository
    {
        private readonly List<Competition> _competitionList = new List<Competition>();
        private readonly List<PlayerCompetition> _playerCompetition = new List<PlayerCompetition>();
        public void Create(string name, string type)
        {
            AbsctractCompetitionFactory factory = null;
            if (type == "knockout")
                factory = new KnockoutCompetitionFactory();
            else
                factory = new GroupCompetitionFactory();

            var competition = factory.CreateCompetition();
            _competitionList.Add(new Competition
            {
                CompetitionId = _competitionList.FirstOrDefault() == null ? 1 : _competitionList.OrderByDescending(o => o.CompetitionId).FirstOrDefault().CompetitionId + 1,
                Name = name,
                GameMode = competition.GameMode,
                Rules = competition.Rules
            });
        }

        public void Create(string name, string type, decimal winnerPriceSum)
        {
            AbsctractCompetitionFactory factory = null;
            if (type == "knockout")
                factory = new KnockoutCompetitionFactory();
            else
                factory = new GroupCompetitionFactory();

            var competition = factory.CreateCompetition();
            _competitionList.Add(new Competition
            {
                CompetitionId = _competitionList.FirstOrDefault() == null ? 1 : _competitionList.OrderByDescending(o => o.CompetitionId).FirstOrDefault().CompetitionId + 1,
                Name = name,
                GameMode = competition.GameMode,
                Rules = competition.Rules,
                WinnerPriceSum = winnerPriceSum
            });
        }

        public void AddCompetitor(int competitionId, Player player)
        {
            var playerCompetition = new PlayerCompetition{PlayerId = player.PartyId, CompetitionId = competitionId};
            _playerCompetition.Add(playerCompetition);
            _competitionList.SingleOrDefault(c => c.CompetitionId == competitionId).Players.Add(playerCompetition);
        }

        public void AddMatch(int competitionId, Match match)
        {
            _competitionList.SingleOrDefault(c => c.CompetitionId == competitionId).Matches.Add(match);
        }

        public List<Match> GetMatches(int competitionId)
        {
            throw new NotImplementedException();
        }

        public List<Competition> All()
        {
            return _competitionList;
        }

        public Player Winner(int competitionId)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetCompetitors(int competitionId)
        {
            return _playerCompetition.Where(pc => pc.CompetitionId == competitionId).Select(p => p.Player).ToList();
        }
    }
}
