using System;
using UnityEngine;

namespace Immerse.Core
{
    public static class TimeExtensions
    {
        public static string ToHumanTime(this float floatTime)
        {
            return ToHumanTime((double) floatTime);
        }

        public static string ToHumanTime(this double floatTime)
        {
            int time = (int) floatTime;
            int minutes = time / 60;
            time -= minutes * 60;
            int seconds = time;

            return $"{minutes.ToString().PadLeft(2, '0')}:{seconds.ToString().PadLeft(2, '0')}";
        }
        
        public static int ToSeconds(this string humanTime)
        {
            try
            {
                var timeDigits = humanTime.Split(':');
                int minutes = Int32.Parse(timeDigits[0]);
                int seconds = Int32.Parse(timeDigits[1]);
                return minutes * 60 + seconds;
            }
            catch
            {
                return 0;
            }
        }

        public static bool IsSeconds(this string humanTime)
        {
            try
            {
                var timeDigits = humanTime.Split(':');
                Int32.Parse(timeDigits[0]);
                Int32.Parse(timeDigits[1]);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}