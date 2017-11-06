using System;
using System.Collections.Generic;
using System.Text;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingUnitTestsLib
{
    public class FakePlayerRepository : IPlayerRepository
    {
        private readonly List<Player> _playerList = new List<Player>();

        public void Create(string name, string legalId)
        {
            _playerList.Add(new Player{Name = name, LegalId = legalId});
        }

        public List<Player> All()
        {
            return _playerList;
        }
    }
}
