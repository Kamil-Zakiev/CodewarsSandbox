using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/52685f7382004e774f0001f7
    /// </summary>
    [Tag(Category.Algorithms | Category.DatesTime | Category.Mathematics | Category.Numbers)]
    public class TimeFormat
    {
        private static string Pad(int s)
        {
            return s.ToString().PadLeft(2, '0');
        }
        public static string GetReadableTime(int seconds)
        {
            var minutes = seconds / 60;
            var hours = minutes / 60;

            minutes = minutes % 60;
            seconds = seconds % 60;

            return Pad(hours) + ":" + Pad(minutes) + ":" + Pad(seconds);
        }
    }
}
