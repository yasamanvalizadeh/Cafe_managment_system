using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagementSystem.Common.CommonExtensionMethods
{
    public static class ConvertToPersionDate
    {
        public static string ToPersion(DateTime dt)
        {
            var pc = new PersianCalendar();
            var day = pc.GetDayOfMonth(dt);
            var month = pc.GetMonth(dt);
            var year = pc.GetYear(dt);
            return $"{year}/{month}/{day}";
        }
    }
}
