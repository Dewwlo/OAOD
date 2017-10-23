using System.Collections.Generic;
using BengansBowlingModelsLib;

namespace BengansBowlingInterfaceLib
{
    public interface ISeriesRepository
    {
        void Create(Player player, Match match);
        Series Get(int seriesId);
        List<Series> All();
        void AddScore(int seriesId, int score);
        int CalculateSeriesScore(int[,] array);
    }
}
