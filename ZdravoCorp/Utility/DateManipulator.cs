using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Utility
{
    public static class DateManipulator
    {
        private const string culture = "en-GB";

        public static bool checkIfLaterDate(DateTime currentDate, DateTime checkingDate)
        {
            return currentDate >= checkingDate;
        }

        public static DateTime addTimeToDate(DateTime date, TimeSpan time)
        {
            return date.Add(time);
        }

        public static TimeSpan Duration(DateTime start, DateTime end)
        {
            return end.Subtract(start);
        }

    }
}
