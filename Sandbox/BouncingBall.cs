using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/5544c7a5cb454edb3c000047
    /// </summary>
    [Tag(Category.Fundamentals)]
    public class BouncingBall
    {
        public static int bouncingBall(double h, double bounce, double window)
        {
            // checking conditions
            if (h <= 0) return -1;
            if (bounce <= 0 || bounce >= 1) return -1;
            if (window >= h) return -1;

            var k = 1;
            h *= bounce;
            while (h > window)
            {
                k += 2;
                h *= bounce;
            }

            return k;
        }
    }
}
