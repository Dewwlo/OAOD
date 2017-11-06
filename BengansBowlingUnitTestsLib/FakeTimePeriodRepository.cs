using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccountabilityInterfacesLib;
using AccountabilityLib;

namespace BengansBowlingUnitTestsLib
{
    public class FakeTimePeriodRepository : ITimePeriodRepository
    {
        private readonly List<TimePeriod> _timePeriodsList = new List<TimePeriod>();
        public void Create(DateTime fromDate, DateTime toDate)
        {
            _timePeriodsList.Add(new TimePeriod{FromDate = fromDate, ToDate = toDate});
        }

        public TimePeriod Get(int timePeriodId)
        {
            return _timePeriodsList.FirstOrDefault(tp => tp.TimePeriodId == timePeriodId);
        }

        public List<TimePeriod> All()
        {
            return _timePeriodsList;
        }
    }
}
