using System;
using System.Collections.Generic;
using System.Linq;
using BengansBowlingDbLib.AbstractCompetitionFactory;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingDbLib
{
    public class SqlCompetitionRepository : ICompetitionRepository
    {
        private readonly BengansBowlingContext _context;
        public SqlCompetitionRepository(BengansBowlingContext context)
        {
            _context = context;
        }

        public void Create(string name, string type)
        {
            AbsctractCompetitionFactory factory = null;
            if (type == "knockout")
                factory = new KnockoutCompetitionFactory();
            else
                factory = new GroupCompetitionFactory();

            var competition = factory.CreateCompetition();
            _context.Competitions.Add(new Competition
            {
                Name = name,
                GameMode = competition.GameMode,
                Rules = competition.Rules
            });
            _context.SaveChanges();
        }

        public void Create(string name, string type, decimal winnerPriceSum)
        {
            AbsctractCompetitionFactory factory = null;
            if (type == "knockout")
                factory = new KnockoutCompetitionFactory();
            else
                factory = new GroupCompetitionFactory();

            var competition = factory.CreateCompetition();
            _context.Competitions.Add(new Competition
            {
                Name = name,
                GameMode = competition.GameMode,
                Rules = competition.Rules,
                WinnerPriceSum = winnerPriceSum
            });
            _context.SaveChanges();
        }

        public void AddCompetitor(int competitionId, Player player)
        {
            _context.PlayerCompetitions.Add(new PlayerCompetition
            {
                Player = player,
                Competition = _context.Competitions.SingleOrDefault(c => c.CompetitionId == competitionId)
            });
            _context.SaveChanges();
        }

        public void AddMatch(int competitionId, Match match)
        {
            _context.Competitions.SingleOrDefault(c => c.CompetitionId == competitionId).Matches.Add(match);
            _context.SaveChanges();
        }

        public List<Match> GetMatches(int competitionId)
        {
            return _context.Competitions.SingleOrDefault(c => c.CompetitionId == competitionId).Matches;
        }

        public List<Competition> All()
        {
            return _context.Competitions.ToList();
        }

        public Player Winner(int competitionId)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetCompetitors(int competitionId)
        {
            return _context.PlayerCompetitions.Where(pm => pm.CompetitionId == competitionId).Select(p => p.Player).ToList();
        }
    }
}
