using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/52f787eb172a8b4ae1000a34
    /// </summary>
    [Tag(Category.Algorithms | Category.Mathematics | Category.Numbers)]
    public class NumberOfTrailingZeros
    {
        public static int TrailingZeros(int n)
        {
            var countOfTwo = 0;
            var countOfFive = 0;

            var d = 2;
            while (d <= n)
            {
                countOfTwo += n / d;
                d *= 2;
            }

            d = 5;
            while (d <= n)
            {
                countOfFive += n / d;
                d *= 5;
            }

            return countOfTwo > countOfFive ? countOfFive : countOfTwo;
        }
    }
}
