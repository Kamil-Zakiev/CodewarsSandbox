using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/526571aae218b8ee490006f4
    /// </summary>
    [Tag(Category.Algorithms | Category.Binary | Category.Bit)]
    public class BitCounting
    {
        public static int CountBits(int n)
        {
            var count = 0;
            while (n != 0)
            {
                if (n % 2 == 1)
                {
                    count++;
                }
                n /= 2;
            }
            
            return count;
        }
    }
}