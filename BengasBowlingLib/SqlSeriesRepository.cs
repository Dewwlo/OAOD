using System;
using System.Collections.Generic;
using System.Linq;
using BengansBowlingDbLib;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingLib
{
    public class SqlSeriesRepository : ISeriesRepository
    {
        private readonly BengansBowlingContext _context;
        private readonly FrameState _frameState;
        public SqlSeriesRepository(BengansBowlingContext context, FrameState frameState)
        {
            _context = context;
            _frameState = frameState;
        }
        public void Create(Player player, Match match)
        {
            _context.Series.Add(new Series { Player = player, Match = match });
            _context.SaveChanges();
        }

        public Series Get(int seriesId)
        {
            return _context.Series.SingleOrDefault(s => s.SeriesId == seriesId);
        }

        public List<Series> All()
        {
            return _context.Series.ToList();
        }

        public void AddScore(int seriesId, int score)
        {
            _context.Series.SingleOrDefault(s => s.SeriesId == seriesId).Score = score;
            _context.SaveChanges();
        }

        public int CalculateSeriesScore(int[,] scoreArray = null, int[,] frameTen = null)
        {
            var seriesScore = new int[10];
            var generate = scoreArray == null || frameTen == null;

            for (int frame = 0; frame < 10; frame++)
            {
                var score = new int[1,2];
                if (frame < 9)
                    score = generate ? GenerateFrameScore() : new [,] {{scoreArray[frame, 0], scoreArray[frame, 1]}};
                else
                    score = generate ? GenerateFrameScoreTen() : new[,] {{ frameTen[0, 0], frameTen[0, 1], frameTen[0, 2]}};

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

        public int[,] GenerateFrameScore()
        {
            var frameScore = new int[1, 2];
            frameScore[0, 0] = new Random().Next(0, 11);

            if (frameScore[0, 0] == 10)
                frameScore[0, 1] = 0;
            else
                frameScore[0, 1] += new Random().Next(0, 11 - frameScore[0, 0]);

            return frameScore;
        }

        public int[,] GenerateFrameScoreTen()
        {
            var frameScore = new int[1, 3];
            frameScore[0, 0] = new Random().Next(0, 11);

            if (frameScore[0, 0] == 10)
            {
                frameScore[0, 1] = new Random().Next(0, 11);
                frameScore[0, 2] = frameScore[0, 1] == 10 ? new Random().Next(0, 11) : new Random().Next(0, 11 - frameScore[0, 1]);
            }
            else
            {
                frameScore[0, 1] += new Random().Next(0, 11 - frameScore[0, 0]);
                frameScore[0, 2] += frameScore.Cast<int>().Sum() == 10 ? new Random().Next(0, 11) : 0;
            }

            return frameScore;
        }
    }
}
