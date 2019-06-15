using System;
using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/59ccf051dcc4050f7800008f
    /// </summary>
    [Tag(Category.Fundamentals | Category.Mathematics | Category.Algorithms | Category.Numbers)]
    public class Bud
    {
        public static string Buddy(long start, long limit)
        {
            long GetProperDividersSum(long k, long? stopValue = null)
            {
                var s = 1L;
                for (long d = 2; d <= Math.Sqrt(k); d++)
                {
                    if (k % d == 0)
                    {
                        s += d;

                        var dPair = k / d;
                        if (dPair != d)
                        {
                            s += dPair;
                        }

                        if (stopValue.HasValue && s > stopValue.Value + 1)
                        {
                            return -1;
                        }
                    }
                }

                return s;
            }

            for (var n = start; n <= limit; n++)
            {
                var sN = GetProperDividersSum(n);
                var m = sN - 1;

                if (n >= m)
                {
                    continue;
                }

                var sM = GetProperDividersSum(m, n);

                if (sM - 1 == n)
                {
                    return $"({n} {m})";
                }
            }

            return "Nothing";
        }
    }
}
