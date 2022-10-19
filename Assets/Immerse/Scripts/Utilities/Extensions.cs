using System;

namespace Immerse.Brodsky
{
    public static class Extensions
    {
        public static string ToHumanTime(this float floatTime)
        {
            int time = (int) floatTime;
            int minutes = time / 60;
            time -= minutes * 60;
            int seconds = time;

            return $"{minutes.ToString().PadLeft(2, '0')}:{seconds.ToString().PadLeft(2, '0')}";
        }

        public static int ToSeconds(this string humanTime)
        {
            var timeDigits = humanTime.Split(':');

            int minutes = Int32.Parse(timeDigits[0]);
            int seconds = Int32.Parse(timeDigits[1]);
            
            return minutes * 60 + seconds;
        }
    }
}