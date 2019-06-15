using System;
using System.Collections.Generic;
using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/55aa075506463dac6600010d
    /// </summary>
    [Tag(Category.Fundamentals | Category.Algorithms | Category.Optimization)]
    public class SumSquaredDivisors
    {
        public static string listSquared(long m, long n)
        {
            var list = new List<string>();
            for (var k = m; k <= n; k++)
            {
                var s = 0L;
                for (long d = 1; d <= k; d++)
                {
                    if (k % d == 0)
                    {
                        s += d * d;
                    }
                }

                if ((int)Math.Sqrt(s) * (int)Math.Sqrt(s) == s)
                {
                    list.Add($"[{k}, {s}]");
                }
            }

            return $"[{string.Join(", ", list)}]";
        }
    }
}
