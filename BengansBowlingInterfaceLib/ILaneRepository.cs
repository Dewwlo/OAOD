using System;
using System.Collections.Generic;
using System.Text;
using BengansBowlingModelsLib;

namespace BengansBowlingInterfaceLib
{
    public interface ILaneRepository
    {
        void Create(string name);
        List<Lane> GetAll();
        void Update(Lane lane);
    }
}
