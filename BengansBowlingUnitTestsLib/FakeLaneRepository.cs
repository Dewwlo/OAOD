using System;
using System.Collections.Generic;
using System.Text;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingUnitTestsLib
{
    public class FakeLaneRepository : ILaneRepository
    {
        private readonly List<Lane> _laneList = new List<Lane>();
        public void Create(string name)
        {
            _laneList.Add(new Lane{Name = name});
        }

        public List<Lane> GetAll()
        {
            return _laneList;
        }

        public void Update(Lane lane)
        {
            throw new NotImplementedException();
        }
    }
}
