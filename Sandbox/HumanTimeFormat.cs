using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/human-readable-duration-format/train/csharp
    /// </summary>
    [Tag(Category.Algorithms | Category.Formats | Category.Strings | Category.DatesTime | Category.Formatting)]
    public class HumanTimeFormat
    {
        public static string formatDuration(int seconds)
        {
            if (seconds == 0)
            {
                return "now";
            }

            string ExtractUnits(int unitSize, string unitName)
            {
                var units = seconds / unitSize;
                if (units == 0)
                {
                    return null;
                }

                seconds -= units * unitSize;

                var ending = units == 1 ? null : "s";
                return $"{units} {unitName}{ending}";
            }

            var parts = new[]
                {
                    ExtractUnits(365*24*60*60, "year"),
                    ExtractUnits(24*60*60, "day"),
                    ExtractUnits(60*60, "hour"),
                    ExtractUnits(60, "minute"),
                    ExtractUnits(1, "second")
                }
                .Where(x => x != null)
                .ToArray();

            return parts.Length == 1
                ? parts.Single()
                : string.Join(", ", parts.SkipLast(1)) + " and " + parts.Last();
        }

        [Fact]
        public void TestingTimeFormat()
        {
           // Assert.Equal("now", formatDuration(0));
            Assert.Equal("1 second", formatDuration(1));
            Assert.Equal("1 minute and 2 seconds", formatDuration(62));
            Assert.Equal("2 minutes", formatDuration(120));
            Assert.Equal("1 hour, 1 minute and 2 seconds", formatDuration(3662));
        }
    }
}
