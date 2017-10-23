using System;
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
        public void Create(List<Player> players, TimePeriod timePeriod, Lane lane)
        {
            _context.Matches.Add(new Match { TimePeriod = timePeriod, Lane = lane });
            _context.SaveChanges();
            var matchId = _context.Matches.OrderByDescending(o => o.MatchId).FirstOrDefault().MatchId;
            players.ForEach(p => _context.PlayerMatches.Add(new PlayerMatch {PlayerId = p.PartyId, MatchId = matchId}));
            _context.SaveChanges();
        }

        public void Create(List<Player> players, TimePeriod timePeriod, Lane lane, Competition competition)
        {
            _context.Matches.Add(new Match { TimePeriod = timePeriod, Lane = lane, Competition = competition });
            _context.SaveChanges();
            var matchId = _context.Matches.OrderByDescending(o => o.MatchId).FirstOrDefault().MatchId;
            players.ForEach(p => _context.PlayerMatches.Add(new PlayerMatch { PlayerId = p.PartyId, MatchId = matchId }));
            _context.SaveChanges();
        }

        public List<Match> All()
        {
            return _context.Matches.ToList();
        }

        public List<Player> GetCompetitors(int matchId)
        {
            return _context.PlayerMatches.Where(p => p.MatchId == matchId).Select(p => p.Player).ToList();
        }

        public Player Winner(int matchId)
        {
            return _context.Matches.SingleOrDefault(m => m.MatchId == matchId).MatchWinner;
        }

        public Player YearChampion(int year)
        {
            var dict = new Dictionary<Player, Tuple<int, int>>();
            var matches = _context.Matches.Where(m => m.TimePeriod.FromDate.Year == year);

            foreach (var match in matches)
            {
                foreach (var player in match.Players.Select(p => p.Player))
                {
                    if (dict.ContainsKey(player))
                        dict[player] = new Tuple<int, int>(dict[player].Item1 + 1, dict[player].Item2 + (match.MatchWinner.PartyId == player.PartyId ? 1 : 0));
                    else
                        dict.Add(player, new Tuple<int, int>(1, match.MatchWinner.PartyId == player.PartyId ? 1 : 0));
                }
            }

            return dict.Select(d => new {d.Key, percent = d.Value.Item2 / d.Value.Item1})
                .OrderByDescending(p => p.percent).Select(p => p.Key).FirstOrDefault();
        }
    }
}
