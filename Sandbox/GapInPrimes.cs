using System;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/561e9c843a2ef5a40c0000a4
    /// </summary>
    [Tag(Category.Fundamentals | Category.Numbers)]
    public class GapInPrimes
    {
        public static long[] Gap(int g, long m, long n)
        {
            long firstPrime, secondPrime = NextPrime(m - 1);
            if (secondPrime > n)
            {
                return null;
            }

            do
            {
                firstPrime = secondPrime;
                secondPrime = NextPrime(firstPrime);
                if (secondPrime > n)
                {
                    return null;
                }
            } while (secondPrime - firstPrime != g);

            return new[] {firstPrime, secondPrime};
        }

        private static long NextPrime(long number)
        {
            var nextPrime = number % 2 == 0
                ? number + 1
                : number + 2;
            while (!IsPrime(nextPrime))
            {
                nextPrime += 2;
            }

            return nextPrime;
        }

        private static bool IsPrime(long number)
        {
            for (long i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        [Fact]
        public void TestIsPrime()
        {
            Assert.False(IsPrime(361));
        }

        [Fact]
        public void Tests()
        {
            Assert.Equal(new long[] { 359, 367 }, Gap(8, 300, 400));
            Assert.Equal(new long[] { 101, 103 }, Gap(2, 100, 110));
            Assert.Equal(new long[] { 103, 107 }, Gap(4, 100, 110));
            Assert.Null(Gap(6, 100, 110));
            Assert.Equal(new long[] { 337, 347 }, Gap(10, 300, 400));
        }
    }
}
