using System;
using System.Collections.Generic;
using System.Text;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingUnitTestsLib
{
    public class FakeSeriesRepository : ISeriesRepository
    {
        public void Create(Player player, Match match)
        {
            throw new NotImplementedException();
        }

        public Series Get(int seriesId)
        {
            throw new NotImplementedException();
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
