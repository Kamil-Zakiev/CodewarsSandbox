using System;
using System.Collections.Generic;
using System.Numerics;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/53d40c1e2f13e331fc000c26
    /// </summary>
    [Tag(Category.Algorithms | Category.Numbers | Category.Mathematics)]
    public class Fibonacci
    {
        private const int NaiveApproachMaxValue = 20;
        private static Dictionary<int, BigInteger> Cache = new Dictionary<int, BigInteger>();

        public static BigInteger fib(int n)
        {
            if (Cache.TryGetValue(n, out var value))
            {
                return value;
            }

            if (n < 0)
            {
                return -n % 2 == 0 ? -fib(-n) : fib(-n);
            }

            if (n <= NaiveApproachMaxValue)
            {
                return NaiveFib(n);
            }

            BigInteger result;
            if (n % 2 == 1)
            {
                var a = fib(n / 2);
                var b = fib(n / 2 + 1);
                result = a * a + b * b;
            }
            else
            {
                result = fib(n + 1) - fib(n - 1);
            }

            Cache.Add(n, result);
            return result;
        }

        private static BigInteger NaiveFib(int n)
        {
            if (n > NaiveApproachMaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "Naive fib algorithm supports non-negative numbers that less or equal to 20!");
            }

            if (n == 0 || n == 1)
            {
                return n;
            }

            return NaiveFib(n - 1) + NaiveFib(n - 2);
        }

        [Fact]
        public void TestFib0()
        {
            TestFib(5, -5);
            TestFib(-3, -4);
            TestFib(1, -1);
            TestFib(0, 0);
            TestFib(1, 1);
            TestFib(1, 2);
            TestFib(2, 3);
            TestFib(3, 4);
            TestFib(5, 5);
            TestFib(10946, 21);
            TestFib(17711, 22);
            TestFib(7540113804746346429, 92);
        }

        [Fact]
        public void TestFib1()
        {
            fib(4000);
        }

        [Fact]
        public void TestFib2()
        {
            fib(700000);
        }

        [Fact]
        public void TestFib3()
        {
            fib(1000000);
        }

        [Fact]
        public void TestFib4()
        {
            fib(1500000);
        }

        [Fact]
        public void TestFib5()
        {
            fib(1700000);
        }

        [Fact]
        public void TestFib6()
        {
            fib(2000000);
        }

        private static void TestFib(long expected, int input)
        {
            Assert.Equal(new BigInteger(expected), fib(input));
        }
    }
}
