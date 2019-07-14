using System;

namespace CodingChallenge
{
    public static class Extensions
    {
        public static DateTime ToDateTime(this double unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
