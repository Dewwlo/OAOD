using System;
using System.Collections.Generic;
using System.Linq;
using AccountabilityLib;
using BengansBowlingDbLib;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingLib
{
    public class SqlCompetitionRepository : ICompetitionRepository
    {
        private readonly BengansBowlingContext _context;
        public SqlCompetitionRepository(BengansBowlingContext context)
        {
            _context = context;
        }

        public void Create(string name)
        {
            _context.Competitions.Add(new Competition
            {
                Name = name,
                Competitors = new List<Party>(),
                Matches = new List<Match>()
            });
            _context.SaveChanges();
        }

        public void Create(string name, decimal winnerPriceSum)
        {
            _context.Competitions.Add(new Competition
            {
                Name = name,
                WinnerPriceSum = winnerPriceSum,
                Competitors = new List<Party>(),
                Matches = new List<Match>()
            });
            _context.SaveChanges();
        }

        public void AddCompetitor(int competitionId, Party player)
        {
            _context.Competitions.SingleOrDefault(c => c.CompetitionId == competitionId).Competitors.Add(player);
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

        public Party Winner(int competitionId)
        {
            throw new NotImplementedException();
        }

        public List<Party> GetCompetitors(int competitionId)
        {
            return _context.Competitions.SingleOrDefault(c => c.CompetitionId == competitionId).Competitors.ToList();
        }
    }
}
