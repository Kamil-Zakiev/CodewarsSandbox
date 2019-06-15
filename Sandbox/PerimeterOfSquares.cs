using System.Numerics;
using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/559a28007caad2ac4e000083
    /// </summary>
    [Tag(Category.Algorithms | Category.Mathematics | Category.Numbers)]
    public class PerimeterOfSquares
    {
        public static BigInteger perimeter(BigInteger n)
        {
            var (p, preLast, last) = (BigInteger.One, BigInteger.One, BigInteger.One);
            while (n-- > 0)
            {
                p += last;
                (preLast, last) = (last, preLast + last);
            }

            return 4 * p;
        }
    }
}
