using System;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/546d15cebed2e10334000ed9
    /// </summary>
    [Tag(Category.Puzzles | Category.Mathematics | Category.Algorithms | Category.Numbers)]
    public class FindTheUnknownDigit
    {
        public static int solveExpression(string expression)
        {
            var (n1, op, n2, n3) = ParseExpression(expression);
            
            var calculator = GetCalculator(op);
            var presentedDigits = expression.Where(IsNumber).Select(ch => ch - '0').Distinct().ToArray();
            var isZeroAllowed = IsZeroDigitAllowed(n1) && IsZeroDigitAllowed(n2) && IsZeroDigitAllowed(n3);
            for (var digit = isZeroAllowed ? 0 : 1; digit < 10; digit++)
            {
                if (presentedDigits.Contains(digit))
                {
                    continue;
                }

                var calculated = calculator(BuiltNumber(n1, digit), BuiltNumber(n2, digit));
                var parsed = int.Parse(BuiltNumber(n3, digit));
                if (calculated == parsed)
                {
                    return digit;
                }
            }
            
            return -1;
        }

        private static (string n1, string op, string n2, string n3) ParseExpression(string expression)
        {
            var n1 = new string(expression.TakeWhile((ch, i) => i == 0 || IsNumber(ch) || ch == '?').ToArray());
            var op = expression.Skip(n1.Length).First().ToString();
            var n2 = new string(expression.Skip(n1.Length + 1).TakeWhile((ch, i) => i == 0 || IsNumber(ch) || ch == '?').ToArray());
            var n3 = new string(expression.Skip(n1.Length + 1 + n2.Length + 1).ToArray());

            return (n1, op, n2, n3);
        }

        private static Func<string, string, int> GetCalculator(string op)
        {
            switch (op)
            {
                case "+": return (a, b) => int.Parse(a) + int.Parse(b);
                case "-": return (a, b) => int.Parse(a) - int.Parse(b);
                case "*": return (a, b) => int.Parse(a) * int.Parse(b);
                default:
                    throw new ArgumentException(nameof(op));
            }
        }

        private static bool IsNumber(char ch) => '0' <= ch && ch <= '9';

        private static string BuiltNumber(string number, int digit) => number.Replace("?", digit.ToString());

        private static bool IsZeroDigitAllowed(string maskedNumber) => !(maskedNumber[0] == '?' && maskedNumber.Length > 1 || maskedNumber[0] == '-' && maskedNumber[1] == '?');

        [Theory]
        [InlineData("1+1=?", "1", "+", "1", "?")]
        [InlineData("123*45?=5?088", "123", "*", "45?", "5?088")]
        [InlineData("-5?*-1=5?", "-5?", "*", "-1", "5?")]
        [InlineData("19--45=5?", "19", "-", "-45", "5?")]
        [InlineData("??*??=302?", "??", "*", "??", "302?")]
        [InlineData("?*11=??", "?", "*", "11", "??")]
        [InlineData("??*1=??", "??", "*", "1", "??")]
        [InlineData("??+??=??", "??", "+", "??", "??")]
        public void Should_parse_numbers(string expression, string n1, string op, string n2, string n3)
        {
            var t = ParseExpression(expression);

            Assert.Equal(n1, t.n1);
            Assert.Equal(op, t.op);
            Assert.Equal(n2, t.n2);
            Assert.Equal(n3, t.n3);
        }

        [Theory]
        [InlineData("1+1=?", 2)]
        [InlineData("123*45?=5?088", 6)]
        [InlineData("-5?*-1=5?", 0)]
        [InlineData("19--45=5?", -1)]
        [InlineData("??*??=302?", 5)]
        [InlineData("?*11=??", 2)]
        [InlineData("??*1=??", 2)]
        [InlineData("??+??=??", -1)]
        public void Should_decipherDigit(string expression, int digit)
        {
            Assert.Equal(solveExpression(expression), digit);
        }
    }
}
