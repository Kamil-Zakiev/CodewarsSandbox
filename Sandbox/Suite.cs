using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/55a29405bc7d2efaff00007c
    /// </summary>
    [Tag(Category.Algorithms | Category.Mathematics | Category.Numbers)]
    public class Suite
    {
        public static double going(int n)
        {
            double sum = 1;
            var b = (double)1 / n;

            while (n > 1 && b > 0.0000001)
            {
                sum += b;
                b /= --n;
            }

            return sum;
        }
    }
}
