using System;
using SpecFlowFramework.Support.Enum;

namespace SpecFlowFramework.Support.Utils
{
    public static class CalendarUtil
    {
        public static DateTime GetNextDay(CalendarEnum day, DateTime startDate)
        {
            var desiredDate = startDate;

            while ((int)desiredDate.DayOfWeek != (int) day)
            {
                desiredDate = desiredDate.AddDays(1);
            }

            return desiredDate;
        }
    }
}
