using System;
using System.Collections.Generic;
using System.Linq;
using BengansBowlingDbLib;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingLib
{
    public class SqlPlayerRepository : IPlayerRepository
    {
        private readonly BengansBowlingContext _context;

        public SqlPlayerRepository(BengansBowlingContext context)
        {
            _context = context;
        }

        public void Create(string name, string legalId)
        {
            var player = new Player{ PartyId = Guid.NewGuid(), Name = name, LegalId = legalId };
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public List<Player> All()
        {
            return _context.Players.ToList();
        }

        public List<Player> Get(string term)
        {
            throw new NotImplementedException();
        }
        
    }
}
