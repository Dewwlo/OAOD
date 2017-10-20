using System;
using System.Collections.Generic;
using System.Linq;
using AccountabilityInterfacesLib;
using AccountabilityLib;
using BengansBowlingDbLib;

namespace BengansBowlingLib
{
    public class SqlPartyRepository : IPartyRepository
    {
        private BengansBowlingContext _context;

        public SqlPartyRepository(BengansBowlingContext context)
        {
            _context = context;
        }

        public void Create(string name, string legalId)
        {
            var party = new Party{ PartyId = Guid.NewGuid(), Name = name, LegalId = legalId };
            _context.Parties.Add(party);
            _context.SaveChanges();
        }

        public List<Party> All()
        {
            return _context.Parties.ToList();
        }

        public List<Party> Get(string term)
        {
            throw new NotImplementedException();
        }
        
    }
}
