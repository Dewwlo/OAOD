using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingUnitTestsLib
{
    public class FakeSeriesRepository : ISeriesRepository
    {
        private readonly List<Series> _seriesList = new List<Series>();
        public void Create(Player player, Match match)
        {
            _seriesList.Add(new Series
            {
                Player = player,
                Match = match,
                SeriesId = _seriesList.FirstOrDefault() == null ? 1 : _seriesList.OrderByDescending(s => s.SeriesId).FirstOrDefault().SeriesId+1
            });
        }

        public Series Get(int seriesId)
        {
            return _seriesList.FirstOrDefault(x => x.SeriesId == seriesId);
        }

        public List<Series> All()
        {
            throw new NotImplementedException();
        }

        public void AddScore(int seriesId, int score)
        {
            throw new NotImplementedException();
        }

        public int CalculateSeriesScore(int[,] scoreArray = null, int[,] frameTen = null)
        {
            throw new NotImplementedException();
        }
    }
}
