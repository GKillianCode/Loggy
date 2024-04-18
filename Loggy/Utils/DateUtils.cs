using System;
using System.Globalization;

namespace Loggy.Utils
{
    public class DateUtils
    {
        private DateUtils() { }

        public static DateTime convertStringDateToDateTime()
        {
            string dateString = "2015-07-29 17:41:44,747";
            string format = "yyyy-MM-dd HH:mm:ss,fff";

            DateTime result;

            if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("ERROR - Impossible de parser la date et l'heure.");
                throw new Exception();
            }

        }
    }
}
