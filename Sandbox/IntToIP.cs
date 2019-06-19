using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox.IntToIP
{
    /// <summary>
    /// https://www.codewars.com/kata/52e88b39ffb6ac53a400022e
    /// </summary>
    [Tag(Category.Algorithms | Category.Bit | Category.Binary)]
    public class IntToIP
    {
        public static string UInt32ToIP(uint ip)
        {
            var bits = new List<uint>();
            while (ip != 0)
            {
                bits.Add(ip % 2);
                ip /= 2;
            }
            
            const int octetSize = 8;
            var octet4 = bits.Take(octetSize);
            var octet3 = bits.Skip(octetSize * 1).Take(octetSize);
            var octet2 = bits.Skip(octetSize * 2).Take(octetSize);
            var octet1 = bits.Skip(octetSize * 3).Take(octetSize);

            return $"{AsDecimal(octet1)}.{AsDecimal(octet2)}.{AsDecimal(octet3)}.{AsDecimal(octet4)}";
        }

        private static int AsDecimal(IEnumerable<uint> reversedBits)
        {
            var value = 0;
            var d = 1;
            foreach (var bit in reversedBits)
            {
                if (bit == 1)
                {
                    value += d;
                }

                d *= 2;
            }

            return value;
        }

        [Fact]
        public void Test()
        {
            Assert.Equal("128.114.17.104", UInt32ToIP(2154959208));
            Assert.Equal("0.0.0.0", UInt32ToIP(0));
            Assert.Equal("128.32.10.1", UInt32ToIP(2149583361));
        }
    }
    
//    public static class PrimitiveExtensions
//    {
//        public static ToBinaryString
//    }
}