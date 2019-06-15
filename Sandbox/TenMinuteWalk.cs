using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/54da539698b8a2ad76000228
    /// </summary>
    [Tag(Category.Fundamentals | Category.Arrays)]
    public class TenMinuteWalk
    {
        public static bool IsValidWalk(string[] walk)
        {
            if (walk.Length != 10) return false;
            var verticalOffset = 0;
            var horizontalOffset = 0;
            foreach (var step in walk)
            {
                switch (step)
                {
                    case "n": verticalOffset++; break;
                    case "s": verticalOffset--; break;
                    case "w": horizontalOffset++; break;
                    case "e": horizontalOffset--; break;
                }
            }

            if (verticalOffset == 0 && horizontalOffset == 0)
            {
                return true;
            }

            return false;
        }
    }
}
