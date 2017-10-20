using System;
using System.Collections.Generic;
using System.Text;
using AccountabilityLib;

namespace AccountabilityInterfacesLib
{
    public interface ITimePeriodRepository
    {
        void Create(DateTime fromDate, DateTime toDate);
        TimePeriod Get(int timePeriodId);
        List<TimePeriod> All();
    }
}
