using System.Collections.Generic;
using System.Linq;
using AccountabilityLib;
using BengansBowlingDbLib;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingLib
{
    public class SqlMatchRepository : IMatchRepository
    {
        private readonly BengansBowlingContext _context;
        public SqlMatchRepository(BengansBowlingContext context)
        {
            _context = context;
        }
        public void Create(List<Party> parties, TimePeriod timePeriod, Lane lane)
        {
            _context.Matches.Add(new Match { Players = parties, TimePeriod = timePeriod, Lane = lane});
            _context.SaveChanges();
        }

        public void Create(List<Party> parties, TimePeriod timePeriod, Lane lane, Competition competition)
        {
            _context.Matches.Add(new Match { Players = parties, TimePeriod = timePeriod, Lane = lane, Competition = competition });
            _context.SaveChanges();
        }

        public List<Match> All()
        {
            return _context.Matches.ToList();
        }

        public List<Party> GetCompetitors(int matchId)
        {
            return _context.Matches.SingleOrDefault(m => m.MatchId == matchId).Players;
        }

        public Party Winner(int matchId)
        {
            return _context.Matches.SingleOrDefault(m => m.MatchId == matchId).MatchWinner;
        }
    }
}
