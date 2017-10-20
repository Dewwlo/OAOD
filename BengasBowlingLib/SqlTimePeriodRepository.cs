using System;
using System.Collections.Generic;
using System.Linq;
using AccountabilityInterfacesLib;
using AccountabilityLib;
using BengansBowlingDbLib;

namespace BengansBowlingLib
{
    public class SqlTimePeriodRepository : ITimePeriodRepository
    {
        private readonly BengansBowlingContext _context;
        public SqlTimePeriodRepository(BengansBowlingContext context)
        {
            _context = context;
        }
        public void Create(DateTime fromDate, DateTime toDate)
        {
            _context.TimePeriods.Add(new TimePeriod { FromDate = fromDate, ToDate = toDate });
            _context.SaveChanges();
        }

        public TimePeriod Get(int timePeriodId)
        {
            return _context.TimePeriods.SingleOrDefault(tp => tp.TimePeriodId == timePeriodId);
        }

        public List<TimePeriod> All()
        {
            return _context.TimePeriods.ToList();
        }
    }
}
