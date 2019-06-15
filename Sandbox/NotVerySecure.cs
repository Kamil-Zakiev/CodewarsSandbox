using System.Text.RegularExpressions;
using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/526dbd6c8c0eb53254000110
    /// </summary>
    [Tag(Category.Bugs | Category.RegularExpressions | Category.DeclarativeProgramming | Category.AdvancedLanguageFeatures | Category.Fundamentals | Category.Strings)]
    public class NotVerySecure
    {
        public static bool Alphanumeric(string str)
        {
            var match = new Regex(@"[a-zA-Z0-9]+").Match(str);
            return match.Success && match.Length == str.Length;
        }
    }
}
