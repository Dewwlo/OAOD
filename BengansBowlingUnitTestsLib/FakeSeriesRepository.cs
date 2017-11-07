using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BengansBowlingDbLib.FrameMemento;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingUnitTestsLib
{
    public class FakeSeriesRepository : ISeriesRepository
    {
        private readonly List<Series> _seriesList = new List<Series>();
        private readonly FrameState _frameState;
        public FakeSeriesRepository(FrameState frameState)
        {
            _frameState = frameState;
        }
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
            return _seriesList;
        }

        public void AddScore(int seriesId, int score)
        {
            _seriesList.SingleOrDefault(s => s.SeriesId == seriesId).Score = score;
        }

        public int CalculateSeriesScore(int[,] scoreArray = null, int[,] frameTen = null)
        {
            var seriesScore = new int[10];

            for (int frame = 0; frame < 10; frame++)
            {
                var score = new int[1, 2];
                if (frame < 9)
                    score = new[,] {{scoreArray[frame, 0], scoreArray[frame, 1]}};
                else
                    score = new[,] {{frameTen[0, 0], frameTen[0, 1], frameTen[0, 2]}};

                SetFrameState(score);
                seriesScore[frame] += score.Cast<int>().Sum();

                if (_frameState.PreviousThrow == "Strike")
                {
                    seriesScore[frame - 1] += score[0, 0] + score[0, 1];

                    if (_frameState.SecondPreviousThrow == "Strike")
                        seriesScore[frame - 2] += score[0, 0];
                }

                if (_frameState.PreviousThrow == "Spare")
                    seriesScore[frame - 1] += score[0, 0];
            }

            return seriesScore.Sum();
        }

        public void SetFrameState(int[,] frame)
        {
            if (frame[0, 0] == 10)
                _frameState.CurrentThrow = "Strike";
            else
                _frameState.CurrentThrow = frame.Cast<int>().Sum() == 10 ? "Spare" : "None";
        }
    }
}
