using System.Collections.Generic;
using AccountabilityLib;
using BengansBowlingModelsLib;

namespace BengansBowlingInterfaceLib
{
    public interface ISeriesRepository
    {
        void Create(Party party, Match match);
        Series Get(int seriesId);
        List<Series> All();
        void AddScore(int seriesId, int score);
        int CalculateSeriesScore(int[,] array);
    }
}
