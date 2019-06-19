using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/52ece9de44751a64dc0001d9
    /// </summary>
    [Tag(Category.Algorithms | Category.Bit | Category.Binary | Category.Mathematics | Category.Numbers)]
    public class BitCalculator
    {
        public static int Calculate(string num1, string num2)
        {
            return num1.AsDecimal() + num2.AsDecimal();
        }
        
        [Fact]
        public void BasicTests()
        {
            Assert.Equal(2, Calculate("1", "1"));
            Assert.Equal(4, Calculate("10", "10"));
            Assert.Equal(2, Calculate("10", "0"));
            Assert.Equal(3, Calculate("10", "1"));
        }
    }

    public static class StringExtensions
    {
        public static int AsDecimal(this string bits)
        {
            var value = 0;
            var d = 1;
            for (var i = bits.Length - 1; i >= 0; i--)
            {
                if (bits[i] == '1')
                {
                    value += d;
                }

                d *= 2;
            }

            return value;
        }
    }
}