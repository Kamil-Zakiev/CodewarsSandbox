using System;
using System.Collections.Generic;
using System.Text;
using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/52bc74d4ac05d0945d00054e
    /// </summary>
    [Tag(Category.Algorithms | Category.Strings | Category.Search)]
    public class FirstNonRepeatingChar
    {
        public static string FirstNonRepeatingLetter(string s)
        {
            var lower = s.ToLower();
            for (var i = 0; i < s.Length; i++)
            {
                var isRepeated = false;
                for (int j = 0; j < s.Length && !isRepeated; j++)
                {
                    isRepeated = i != j && lower[i] == lower[j];
                }

                if (!isRepeated)
                {
                    return s.Substring(i, 1);
                }
            }

            return string.Empty;
        }
    }
}
