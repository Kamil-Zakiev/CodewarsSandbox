using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/514b92a657cdc65150000006
    /// </summary>
    [Tag(Category.Algorithms | Category.Mathematics | Category.Numbers)]
    public class MultiplesOf3or5
    {
        public static int Solution(int value)
        {
            if (value <= 3) return 0;

            var sum = 0;
            var k = 3;

            while (k < value)
            {
                sum += k;
                k += 3;
            }

            k = 5;
            while (k < value)
            {
                sum += k;
                k += 5;
            }

            k = 15;
            while (k < value)
            {
                sum -= k;
                k += 15;
            }

            return sum;
        }
    }
}
