using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Sandbox
{
    class Program
    {
        static Dictionary<(int, int), BigInteger> _cache = new Dictionary<(int, int), BigInteger>();

        static BigInteger Height(int n, int m)
        {
            if (n == 1 || m == 1)
            {
                return m;
            }

            if (_cache.TryGetValue((n, m), out var result))
            {
                return result;
            }

            return _cache[(n, m)] = m + Enumerable.Range(1, m - 1).Select(k => Height(n - 1, k)).Aggregate((a, b) => a + b);
        }

        static BigInteger Height2(int n, int m)
        {
            var array = new BigInteger[m];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }

            BigInteger[] array2 = new BigInteger[m];
            array2[1] = BigInteger.One;

            while (n-- != 2)
            {
                BigInteger s = 0;
                for (int i = 2; i < array2.Length; i++)
                {
                    s += array[i - 1];
                    array2[i] = i + s;
                }

                array = array2;
                array2 = new BigInteger[m];
                array2[1] = BigInteger.One;
            }

            return m + array.Aggregate((a, b) => a + b);
        }

        static BigInteger Height3(int n, int m)
        {
            var array1 = new BigInteger[m];
            BigInteger[] array2 = null;
            while (n-- > 1)
            {
                array2 = new BigInteger[m];
                for (int i = 0; i < m - 1; i++)
                {
                    array2[i + 1] = array1[i] + array2[i] + 1;
                }

                array1 = array2;
            }

            return m + array2.Aggregate((a, b) => a + b);
        }

        static BigInteger Height4(int n, int m)
        {
            var array1 = new BigInteger[m];
            BigInteger[] array2 = null;
            var k = 0;
            var q = BigInteger.Zero;
            while (k++ < n - 1)
            {
                array2 = new BigInteger[m];
                array2[k] = q = (q * 2 + 1);

                for (int i = k; i < m - n + k; i++)
                {
                    array2[i + 1] = array1[i] + array2[i] + 1;
                }

                array1 = array2;
            }

            for (int i = 0; i < n - 1; i++)
            {
                array1[i + 1] = array1[i] * 2 + 1;
            }

            return m + array1.Aggregate((a, b) => a + b);
        }

        static BigInteger Height5(int n, int m)
        {
            var array1 = new BigInteger[m];
            var array2 = new BigInteger[m];
            var array3 = new BigInteger[m];

            var k = 0;
            var q = BigInteger.Zero;
            while (k++ < n - 1)
            {
                array2[k] = q = (q * 2 + 1);
                for (int i = k; i < m - n + k; i++)
                {
                    array2[i + 1] = array1[i] + array2[i] + 1;
                }

                var t = array1;
                array1 = array2;
                array2 = array3;
                array3 = t;

            }

            for (int i = 0; i < n - 1; i++)
            {
                array1[i + 1] = array1[i] * 2 + 1;
            }

            return m + array1.Aggregate((a, b) => a + b);
        }

        public static string listSquared(long m, long n)
        {
            var list = new List<string>();
            for(var k = m; k <= n; k++)
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

        public static string Decomp(int n)
        {
            var primes = new List<int>() { 2, 3 };

            bool IsNotPrime(int number) => primes.Any(prime => number % prime == 0);

            int PushNewPrime()
            {
                var nextPrime = primes.Last() + 2;
                while (IsNotPrime(nextPrime))
                {
                    nextPrime += 2;
                }

                primes.Add(nextPrime);
                return nextPrime;
            }

            var dict = new Dictionary<int, int>();

            void AddPrimeDivider(int prime)
            {
                if (dict.ContainsKey(prime))
                {
                    dict[prime]++;
                }
                else
                {
                    dict[prime] = 1;
                }
            }

            while (n > 1)
            {
                var number = n;
                foreach (var prime in primes)
                {
                    while (number % prime == 0)
                    {
                        AddPrimeDivider(prime);
                        number /= prime;
                    }
                }

                while (number != 1)
                {
                    var nextPrime = PushNewPrime();

                    while (number % nextPrime == 0)
                    {
                        AddPrimeDivider(nextPrime);
                        number /= nextPrime;
                    }
                }

                n--;
            }

            var powers = dict.OrderBy(x => x.Key).Select(x => $"{x.Key}{(x.Value == 1 ? string.Empty : "^" + x.Value)}");
            return string.Join(" * ", powers);
        }

    }
}
