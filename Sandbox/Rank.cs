using System.Linq;
using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/5616868c81a0f281e500005c
    /// </summary>
    [Tag(Category.Fundamentals | Category.Strings | Category.Sorting | Category.Algorithms)]
    public class Rank
    {
        public static string NthRank(string st, int[] we, int n)
        {
            // your code
            if (string.IsNullOrWhiteSpace(st))
            {
                return "No participants";
            }

            var participants = st.Split(',');
            if (participants.Length < n)
            {
                return "Not enough participants";
            }

            var winnersList = participants
                .Select((p, i) =>
                {
                    var lower = p.ToLower();
                    return new
                    {
                        Name = p,
                        WinningNumber = (lower.Length + lower.Sum(ch => ch - 'a' + 1)) * we[i]
                    };
                })
                .OrderByDescending(x => x.WinningNumber)
                .ThenBy(x => x.Name)
                .ToArray();

            return winnersList[n - 1].Name;
        }
    }
}
