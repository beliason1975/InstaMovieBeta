using System;
using System.Linq;
using System.Text;
using InstaMovieBeta.Models.Rovi.CommonJsonTypes;

namespace InstaMovieBeta.Utils
{
    public class Utility
    {
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetCurrentUnixTimestampMillis()
        {
            return (long)(DateTime.UtcNow - unixEpoch).TotalMilliseconds;
        }

        public static DateTime DateTimeFromUnixTimestampMillis(long millis)
        {
            return unixEpoch.AddMilliseconds(millis);
        }

        public static long GetCurrentUnixTimestampSeconds()
        {
            return (long)(DateTime.UtcNow - unixEpoch).TotalSeconds;
        }

        public static DateTime DateTimeFromUnixTimestampSeconds(long seconds)
        {
            return unixEpoch.AddSeconds(seconds);
        }

        public static string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }

        public static Image GetClosestMatchImage(Image[] images, int desiredWidth)
        {
            Image currentMatch = images[0];
            foreach (var img in images)
            {
                if (img.Width == desiredWidth)
                {
                    return img;
                }
                else if (img.Width < desiredWidth && img.Width > currentMatch.Width)
                {
                    currentMatch = img;
                }
            }
            return currentMatch;
        }

        public static Image GetBiggestImage(Image[] images)
        {
            Image currentMatch = images[0];
            foreach (var img in images)
            {
                if (img.Width > currentMatch.Width)
                {
                    currentMatch = img;
                }
            }
            return currentMatch;
        }
    }
}