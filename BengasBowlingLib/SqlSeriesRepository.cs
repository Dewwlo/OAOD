using System;
using System.Collections.Generic;
using System.Linq;
using AccountabilityLib;
using BengansBowlingDbLib;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingLib
{
    public class SqlSeriesRepository : ISeriesRepository
    {
        private readonly BengansBowlingContext _context;
        public SqlSeriesRepository(BengansBowlingContext context)
        {
            _context = context;
        }
        public void Create(Party party, Match match)
        {
            _context.Series.Add(new Series { Party = party, Match = match });
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

        // Incomplete
        public int CalculateSeriesScore(int[,] array)
        {
            var seriesScore = new int[12];
            var lastFrame = "";
            var secondLastFrame = "";

            for (int frame = 0; frame <= 11; frame++)
            {
                //var frameScore = new [,]{{10,0}};
                //var frameScore = GenerateFrameScore();
                var frameScore = new [,]{{ array[frame,0], array[frame,1]}};
                if (frame < 9)
                {
                    seriesScore[frame] = frameScore[0, 0] + frameScore[0, 1];
                }
                else
                {
                    if (frameScore[0, 0] == 10)
                        seriesScore[frame] = 10;
                    else if (frameScore[0, 0] + frameScore[0, 1] == 10 && frame != 11)
                    {
                        seriesScore[frame] = frameScore[0, 0] + frameScore[0, 1];
                        //frame += 1;
                        //seriesScore[frame] = frameScore[0, 1];
                    }
                    else if (frame == 10)
                        seriesScore[frame] = frameScore[0, 0];
                    else
                        frame = 12;
                }

                if (frame < 9)
                {
                    switch (lastFrame)
                    {
                        case "strike":
                            seriesScore[frame - 1] += frameScore[0, 0] + frameScore[0, 1];
                            break;
                        case "spare":
                            seriesScore[frame - 1] += frameScore[0, 0];
                            break;
                    }
                }

                if (secondLastFrame == "strike" && lastFrame == "strike")
                    seriesScore[frame - 2] += frameScore[0, 0];

                secondLastFrame = lastFrame;

                if (frameScore[0, 0] == 10)
                    lastFrame = "strike";
                else if (frameScore[0, 0] + frameScore[0, 1] == 10)
                    lastFrame = "spare";
                else
                    lastFrame = "normal";
            }

            return seriesScore.Sum();
        }

        public int[,] GenerateFrameScore()
        {
            var frameScore = new int[1,2];
            frameScore[0, 0] = new Random().Next(0, 11);

            if (frameScore[0, 0] < 10)
                frameScore[0, 1] = new Random().Next(0, 11 - frameScore[0, 0]);
            else
                frameScore[0, 1] = 0;

            return frameScore;
        }
    }
}
