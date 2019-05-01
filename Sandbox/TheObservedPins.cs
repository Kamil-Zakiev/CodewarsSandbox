using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/the-observed-pin/train/csharp
    /// </summary>
    public class TheObservedPins
    {
        public class GeneratingSource
        {
            private readonly IReadOnlyList<char> _chars;
            private int _currentPoint;

            public GeneratingSource(IReadOnlyList<char> chars)
            {
                _chars = chars;
                _currentPoint = 0;
            }

            /// <summary>
            /// Move pointer to the next char.
            /// </summary>
            /// <returns>The flag indicating overflow.</returns>
            public bool MoveNext()
            {
                var hasOverflow = _currentPoint + 1 > _chars.Count - 1;
                _currentPoint = hasOverflow 
                    ? 0
                    : _currentPoint + 1;

                return !hasOverflow;
            }

            public char CurrentChar => _chars[_currentPoint];
        }

        public static List<string> GetPINs(string observed)
        {
            var relatedNumbers = new Dictionary<char, IReadOnlyList<char>>
            {
                {'1', new List<char> {'1', '2', '4'}},
                {'2', new List<char> {'1', '2', '3', '5'}},
                {'3', new List<char> {'2', '3', '6'}},
                {'4', new List<char> {'1', '4', '5', '7'}},
                {'5', new List<char> {'2', '4', '5', '6', '8'}},
                {'6', new List<char> {'3', '5', '6', '9'}},
                {'7', new List<char> {'4', '7', '8'}},
                {'8', new List<char> {'5', '7', '8', '9', '0'}},
                {'9', new List<char> {'6', '8', '9'}},
                {'0', new List<char> {'0', '8'}}
            };

            var generatingSources = observed.Reverse().Select(ch => new GeneratingSource(relatedNumbers[ch])).ToArray();
            
            var pins = new List<string>();
            do
            {
                var pinChars = generatingSources.Select(s => s.CurrentChar).Reverse();
                pins.Add(string.Concat(pinChars));
            } while (generatingSources.Any(s => s.MoveNext()));

            return pins;
        }

        [Fact]
        public void Should_returnCorrectAmountOfPins()
        {
            var pins = GetPINs("1234");
            Assert.Equal(144, pins.Count);
        }

        [Fact]
        public void Should_generateExpectedPins_When_simpleCase()
        {
            var pins = GetPINs("11");
            Assert.Equal(new[] { "11", "22", "44", "12", "21", "14", "41", "24", "42" }.OrderBy(x => x), pins);
        }

        [Fact]
        public void Should_generateExpectedPins_When_tripleCase()
        {
            var pins = GetPINs("369");
            Assert.Equal(new[] { "339", "366", "399", "658", "636", "258", "268", "669", "668", "266", "369", "398", "256", "296", "259", "368", "638", "396", "238", "356", "659", "639", "666", "359", "336", "299", "338", "696", "269", "358", "656", "698", "699", "298", "236", "239" }.OrderBy(x => x), pins);
        }
    }
}
