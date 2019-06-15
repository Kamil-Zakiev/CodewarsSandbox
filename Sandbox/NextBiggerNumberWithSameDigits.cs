using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/55983863da40caa2c900004e
    /// </summary>
    [Tag(Category.Algorithms | Category.Numbers | Category.Strings | Category.Integers)]
    public class NextBiggerNumberWithSameDigits
    {
        public static long NextBiggerNumber(long n)
        {
            var endingDigits = new List<long>() { n % 10 };
            n /= 10;
            long nextDigit;
            do
            {
                nextDigit = n % 10;
                endingDigits.Insert(0, nextDigit);
                n /= 10;
            } while (nextDigit >= endingDigits[1] && n > 0);

            var firstDigit = endingDigits[0];
            if (n == 0 && endingDigits.Skip(1).All(d => d <= firstDigit || d == 0))
            {
                return -1;
            }

            var shouldBeFirst = endingDigits.Skip(1).Where(d => d > firstDigit).Min();
            endingDigits.Remove(shouldBeFirst);

            return new[] { n, shouldBeFirst }
                .Concat(endingDigits.OrderByDescending(x => x))
                .Aggregate((x, y) => x * 10 + y);
        }
    }
}
