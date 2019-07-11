using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/5259510fc76e59579e0009d4
    /// </summary>
    [Tag(Category.Algorithms | Category.Search | Category.DataStructures)]
    public class MostSimilarString
    {
        private IEnumerable<string> words;

        public MostSimilarString(IEnumerable<string> words)
        {
            this.words = words;
        }

        public string FindMostSimilar(string term)
        {
            var (minChangesCount, didYouMean) = (int.MaxValue, string.Empty);
            foreach (var word in words)
            {
                var changesCount = LevenshteinDistance(term, word);
                if (minChangesCount > changesCount)
                {
                    (minChangesCount, didYouMean) = (changesCount, word);
                }
            }

            return didYouMean;
        }

        private int NaiveDistance(string source, string target)
        {
            var d1 = source.GroupBy(ch => ch).ToDictionary(g => g.Key, g => g.Count());
            var d2 = target.GroupBy(ch => ch).ToDictionary(g => g.Key, g => g.Count());

            var intersectedChars = d1.Where(kv => d2.ContainsKey(kv.Key)).ToArray();
            foreach (var (key1, value1) in intersectedChars)
            {
                var k = Math.Min(value1, d2[key1]);
                d1[key1] -= k;
                d2[key1] -= k;
            }

            return Math.Max(d1.Values.Sum(), d2.Values.Sum());
        }

        // see https://en.wikipedia.org/wiki/Levenshtein_distance
        private int LevenshteinDistance(string source, string target)
        {
            var m = new int[source.Length + 1, target.Length + 1];
            for (var i = 0; i < source.Length + 1; i++)
            {
                m[i, 0] = i;
            }
            for (var j = 0; j < target.Length + 1; j++)
            {
                m[0, j] = j;
            }

            for (var i = 1; i < source.Length + 1; i++)
            {
                for (var j = 1; j < target.Length + 1; j++)
                {
                    var k = source[i - 1] == target[j - 1] ? 0 : 1;
                    m[i, j] = Math.Min(
                        m[i - 1, j] + 1,
                        Math.Min(
                            m[i, j - 1] + 1,
                            m[i - 1, j - 1] + k));
                }
            }

            return m[source.Length, target.Length];
        }
    }

    public class MostSimilarStringTests
    {
        [Fact]
        public void TestDictionary1()
        {
            var kata = new MostSimilarString(new List<string>
                {"cherry", "pineapple", "melon", "strawberry", "raspberry"});
            Assert.Equal("strawberry", kata.FindMostSimilar("strawbery"));
            Assert.Equal("cherry", kata.FindMostSimilar("berry"));
        }

        [Fact]
        public void TestDictionary2()
        {
            var kata = new MostSimilarString(new List<string>
                {"javascript", "java", "ruby", "php", "python", "coffeescript"});
            Assert.Equal("java", kata.FindMostSimilar("heaven"));
            Assert.Equal("javascript", kata.FindMostSimilar("javascript"));
        }

        [Fact]
        public void TestDictionary3()
        {
            var kata = new MostSimilarString(new List<string> {"karpscdigdvucfr", "zqdrhpviqslik"});
            Assert.Equal("zqdrhpviqslik", kata.FindMostSimilar("rkacypviuburk"));
        }

        [Fact]
        public void TestDictionary4()
        {
            var kata = new MostSimilarString(new List<string> {"java", "php", "python", "brainfuck", "coffeescript", "c", "ruby", "javascript", "cpp"});
            Assert.Equal("java", kata.FindMostSimilar("heaven"));
        }
    }
}