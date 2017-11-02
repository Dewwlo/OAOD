using System;
using System.Collections.Generic;
using System.Text;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingUnitTestsLib
{
    public class FakePlayerRepository : IPlayerRepository
    {
        public void Create(string name, string legalId)
        {
            throw new NotImplementedException();
        }

        public List<Player> All()
        {
            throw new NotImplementedException();
        }
    }
}
