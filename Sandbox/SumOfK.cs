using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/55e7280b40e1c4a06d0000aa
    /// </summary>
    [Tag(Category.Fundamentals)]
    public class SumOfK
    {
        public static int? chooseBestSum(int t, int k, List<int> ls)
        {
            if (ls.Count < k)
            {
                return null;
            }

            var sources = ls.Where(distance => distance <= t).Select((distance, position) => (distance: distance, position: position)).ToArray();
            var paths = sources;
            while (--k > 0)
            {
                paths = (from p in paths
                        from si in sources
                        where p.position < si.position && p.distance + si.distance <= t
                        select (distance: p.distance + si.distance, position: si.position))
                    .ToArray();

                if (!paths.Any())
                {
                    return null;
                }
            }
            
            return paths.Max(x => x.distance);
        }

        [Fact]
        public void Tests()
        {
            List<int> ts = new List<int> { 50, 55, 56, 57, 58 };
            int? n = SumOfK.chooseBestSum(163, 3, ts);
            Assert.Equal(163, n);

            ts = new List<int> { 50 };
            n = SumOfK.chooseBestSum(163, 3, ts);
            Assert.Null(n);

            ts = new List<int> { 91, 74, 73, 85, 73, 81, 87 };
            n = SumOfK.chooseBestSum(230, 3, ts);
            Assert.Equal(228, n);
        }
    }
}
