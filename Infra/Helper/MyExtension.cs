using System;
using System.Collections.Generic;

namespace Data.Helper
{
    public static class MyExtension
    {
        public static DateTime getLocalTime(this DateTime utc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time"));
        }

        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }

        public static List<string> SplitString(this string text, char separator)
        {
            List<string> trimmedSplits = new List<string>();

            var splits = text.Split(separator);

            foreach (var split in splits)
                trimmedSplits.Add(split.Trim());

            return trimmedSplits;

        }
    }
}