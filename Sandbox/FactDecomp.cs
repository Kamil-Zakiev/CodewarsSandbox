using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/5a045fee46d843effa000070
    /// </summary>
    [Tag(Category.Fundamentals)]
    public class FactDecomp
    {
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
