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

        public Party YearChampion(int year)
        {
            var dict = new Dictionary<Party, Tuple<int, int>>();
            var temp = _context.Matches.Where(m => m.TimePeriod.FromDate.Year == year);

            foreach (var temps in temp)
            {
                foreach (var temp1 in temps.Players)
                {
                    if (dict.ContainsKey(temp1))
                        dict[temp1] = new Tuple<int, int>(dict[temp1].Item1 + 1, dict[temp1].Item2 + (temps.MatchWinner.PartyId == temp1.PartyId ? 1 : 0));
                    else
                        dict.Add(temp1, new Tuple<int, int>(1, temps.MatchWinner.PartyId == temp1.PartyId ? 1 : 0));
                }
                //? dict[p] = new Tuple<int, int>(1, temps.MatchWinner.PartyId == p.PartyId ? 1 : 0)
                //: dict.Add(p, new Tuple<int, int>(1, temps.MatchWinner.PartyId == p.PartyId ? 1 : 0)));
                //temps.Players.ForEach(p =>
                //dict.ContainsKey(p)
                //? dict[p] = new Tuple<int, int>(1, temps.MatchWinner.PartyId == p.PartyId ? 1 : 0)
                //: dict.Add(p, new Tuple<int, int>(1, temps.MatchWinner.PartyId == p.PartyId ? 1 : 0)));
            }

            return dict.Keys.FirstOrDefault();
        }
    }
}
