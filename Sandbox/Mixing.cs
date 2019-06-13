using System;
using System.Linq;
using Xunit;

namespace Sandbox
{
    public class Mixing
    {
        public static string Mix(string s1, string s2)
        {
            var dict1 = s1
                .Where(x => x >= 'a' && x <= 'z')
                .GroupBy(x => x)
                .Select(g => new
                {
                    Letter = g.Key,
                    String = "1",
                    Count = g.Count()
                });

            var dict2 = s2
                .Where(x => x >= 'a' && x <= 'z')
                .GroupBy(x => x)
                .Select(g => new
                {
                    Letter = g.Key,
                    String = "2",
                    Count = g.Count()
                });

            var crossed = dict1
                .Join(dict2, d1 => d1.Letter, d2 => d2.Letter, (d1, d2) => new
                {
                    d1.Letter,
                    String = d1.Count > d2.Count
                        ? "1"
                        : d1.Count < d2.Count
                            ? "2"
                            : "=",
                    Count = Math.Max(d1.Count, d2.Count)
                });

            var outer1 = dict1.Where(d => !crossed.Any(kv => kv.Letter == d.Letter));
            var outer2 = dict2.Where(d => !crossed.Any(kv => kv.Letter == d.Letter));

            var data = crossed.Concat(outer1).Concat(outer2).Where(x => x.Count > 1).ToArray();

            var parts = data
                .OrderByDescending(x => x.Count)
                .ThenByDescending(x => x.String == "=" ? 0 : x.String == "1" ? 2 : 1)
                .ThenBy(x => x.Letter)
                .Select(x => $"{x.String}:{new string(x.Letter, x.Count)}")
                .ToArray();

            return string.Join("/", parts);
        }

        [Fact]
        public static void test1()
        {
            Assert.Equal("2:eeeee/2:yy/=:hh/=:rr", Mix("Are they here", "yes, they are here"));
            Assert.Equal("1:ooo/1:uuu/2:sss/=:nnn/1:ii/2:aa/2:dd/2:ee/=:gg",
                Mix("looping is fun but dangerous", "less dangerous than coding"));
            Assert.Equal("1:aaa/1:nnn/1:gg/2:ee/2:ff/2:ii/2:oo/2:rr/2:ss/2:tt",
                Mix(" In many languages", " there's a pair of functions"));
            Assert.Equal("1:ee/1:ll/1:oo", Mix("Lords of the Fallen", "gamekult"));
            Assert.Equal("", Mix("codewars", "codewars"));
            Assert.Equal("1:nnnnn/1:ooooo/1:tttt/1:eee/1:gg/1:ii/1:mm/=:rr",
                Mix("A generation must confront the looming ", "codewarrs"));
        }
    }
}
