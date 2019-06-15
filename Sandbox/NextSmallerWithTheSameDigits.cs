using Sandbox.HelperUtils;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/5659c6d896bc135c4c00021e
    /// </summary>
    [Tag(Category.Algorithms | Category.Numbers | Category.Strings | Category.Integers)]
    public class NextSmallerWithTheSameDigits
    {
        public static long NextSmaller2(long n)
        {
            var endingDigits = new List<long>() { (byte)(n % 10) };
            n /= 10;
            byte nextDigit;
            do
            {
                nextDigit = (byte)(n % 10);
                endingDigits.Insert(0, nextDigit);
                n /= 10;
            } while (nextDigit <= endingDigits[1] && n > 0);

            var firstDigit = endingDigits[0];
            if (n == 0 && endingDigits.Skip(1).All(d => d >= firstDigit || d == 0))
            {
                return -1;
            }

            var shouldBeFirst = endingDigits.Skip(1).Where(d => d < firstDigit).Max();
            endingDigits.Remove(shouldBeFirst);

            return new[] { n, shouldBeFirst }
                .Concat(endingDigits.OrderBy(x => x))
                .Aggregate((x, y) => x * 10 + y);
        }
    }
}
