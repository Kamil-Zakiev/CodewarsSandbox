using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/5526fc09a1bbd946250002dc
    /// </summary>
    [Tag(Category.Algorithms)]
    public class ParityOutlier
    {
        public static int Find(int[] integers)
        {
            var oddCount = 0;
            var evenCount = 0;
            var lastEven = 0;
            var lastOdd = 0;

            foreach (var number in integers)
            {
                if (number % 2 == 0)
                {
                    evenCount++;
                    lastEven = number;
                }
                else
                {
                    oddCount++;
                    lastOdd = number;
                }

                if (evenCount * oddCount == 0) continue;

                if (evenCount == 1) return lastEven;
                return lastOdd;
            }

            return -1;
        }
    }
}
