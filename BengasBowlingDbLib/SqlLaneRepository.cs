using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingDbLib
{
    public class SqlLaneRepository : ILaneRepository
    {
        private readonly BengansBowlingContext _context;
        public SqlLaneRepository(BengansBowlingContext context)
        {
            _context = context;
        }

        public void Create(string name)
        {
            _context.Lanes.Add(new Lane {Name = name});
            _context.SaveChanges();
        }

        public List<Lane> GetAll()
        {
            return _context.Lanes.ToList();
        }

        public void Update(Lane lane)
        {
            _context.Update(lane);
        }
    }
}
