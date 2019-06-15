using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/54da5a58ea159efa38000836
    /// </summary>
    [Tag(Category.Fundamentals)]
    public class FindTheOddInt
    {
        public static int find_it(int[] seq)
        {
            var dict = new Dictionary<int, int>();
            foreach (var item in seq)
            {
                if (dict.ContainsKey(item))
                {
                    dict[item]++;
                }
                else
                {
                    dict.Add(item, 1);
                }
            }

            return dict.Single(kv => kv.Value % 2 == 1).Key;
        }
    }
}
