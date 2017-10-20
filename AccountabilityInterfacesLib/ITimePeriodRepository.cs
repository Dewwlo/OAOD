using System;
using System.Collections.Generic;
using System.Text;
using AccountabilityLib;

namespace AccountabilityInterfacesLib
{
    interface ITimePeriodRepository
    {
        void Create(DateTime fromDate, DateTime toDate);
        List<TimePeriod> Get(int timePeriodId);
        List<TimePeriod> All();
    }
}
