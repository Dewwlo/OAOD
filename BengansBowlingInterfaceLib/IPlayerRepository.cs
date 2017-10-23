using System.Collections.Generic;
using BengansBowlingModelsLib;

namespace BengansBowlingInterfaceLib
{
    public interface IPlayerRepository
    {
        void Create(string name, string legalId);
        List<Player> All();
        List<Player> Get(string term);
    }
}
