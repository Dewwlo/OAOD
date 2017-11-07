using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccountabilityLib;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;
using Remotion.Linq.Clauses;

namespace BengansBowlingUnitTestsLib
{
    public class FakeMatchRepository : IMatchRepository
    {
        private readonly List<Match> _matchList = new List<Match>();

        public void Create(List<Player> players, TimePeriod timePeriod, Lane lane)
        {
            var matchId = _matchList.FirstOrDefault() == null ? 1 : _matchList.OrderByDescending(o => o.MatchId).FirstOrDefault().MatchId + 1;
            var match = new Match {MatchId = matchId, TimePeriod = timePeriod, Lane = lane};
            match.Players = players.Select(p => new PlayerMatch { Player = p, Match = match }).ToList();
            _matchList.Add(match);
        }

        public void Create(List<Player> players, TimePeriod timePeriod, Lane lane, Competition competition)
        {
            var matchId = _matchList.FirstOrDefault() == null ? 1 : _matchList.OrderByDescending(o => o.MatchId).FirstOrDefault().MatchId + 1;
            var match = new Match { MatchId = matchId, TimePeriod = timePeriod, Lane = lane, Competition = competition };
            match.Players = players.Select(p => new PlayerMatch {Player = p, Match = match}).ToList();
            _matchList.Add(match);
        }

        public List<Match> All()
        {
            return _matchList;
        }

        public List<Player> GetCompetitors(int matchId)
        {
            return _matchList.FirstOrDefault(m => m.MatchId == matchId).Players.Select(p => p.Player).ToList();
        }

        public Player Winner(int matchId)
        {
            return _matchList.SingleOrDefault(m => m.MatchId == matchId).MatchWinner;
        }

        public Player YearChampion(int year)
        {
            var dict = new Dictionary<Player, Tuple<int, int>>();
            var matches = _matchList.Where(m => m.TimePeriod.FromDate.Year == year);

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

            return dict.Select(d => new { d.Key, percent = d.Value.Item2 / d.Value.Item1 })
                .OrderByDescending(p => p.percent).Select(p => p.Key).FirstOrDefault();
        }
    }
}
