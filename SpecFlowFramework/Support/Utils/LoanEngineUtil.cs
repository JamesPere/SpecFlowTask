using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowFramework.Support.Utils
{
    public static class LoanEngineUtil
    {
        public static DateTime GetExpectedRepaymentDate(DateTime dateRequested)
        {
            if(dateRequested.DayOfWeek == DayOfWeek.Saturday || dateRequested.DayOfWeek == DayOfWeek.Sunday)
            {
                var adjustedDate = dateRequested;

                while(adjustedDate.DayOfWeek == DayOfWeek.Saturday || adjustedDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    adjustedDate = adjustedDate.AddDays(-1);
                }
            }

            return dateRequested;
        }
    }
}
