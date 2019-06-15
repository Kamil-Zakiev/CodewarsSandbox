using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/55bf01e5a717a0d57e0000ec
    /// </summary>
    [Tag(Category.Fundamentals | Category.Numbers)]
    public class PersistentBugger
    {
        public static int Persistence(long n)
        {
            var k = 0;
            var n1 = n;

            while (n1 >= 10)
            {
                var mult = 1L;
                while (n1 != 0)
                {
                    var lastDigit = n1 % 10;
                    mult *= lastDigit;
                    n1 /= 10;
                }
                k++;
                n1 = mult;
            }
            return k;
        }
    }
}
