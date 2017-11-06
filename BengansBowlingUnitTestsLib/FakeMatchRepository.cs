using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccountabilityLib;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingUnitTestsLib
{
    public class FakeMatchRepository : IMatchRepository
    {
        private readonly List<Match> _matchList = new List<Match>();
        private readonly List<PlayerMatch> _playerMatchesList = new List<PlayerMatch>();

        public void Create(List<Player> players, TimePeriod timePeriod, Lane lane)
        {
            _matchList.Add(new Match { TimePeriod = timePeriod, Lane = lane });
            var matchId = _matchList.OrderByDescending(o => o.MatchId).FirstOrDefault().MatchId;
            players.ForEach(p => _playerMatchesList.Add(new PlayerMatch { PlayerId = p.PartyId, MatchId = matchId }));
        }

        public void Create(List<Player> players, TimePeriod timePeriod, Lane lane, Competition competition)
        {
            throw new NotImplementedException();
        }

        public List<Match> All()
        {
            return _matchList;
        }

        public List<Player> GetCompetitors(int matchId)
        {
            return _playerMatchesList.Where(p => p.MatchId == matchId).Select(p => p.Player).ToList();
        }

        public Player Winner(int matchId)
        {
            throw new NotImplementedException();
        }

        public Player YearChampion(int year)
        {
            throw new NotImplementedException();
        }
    }
}
