using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/52597aa56021e91c93000cb0
    /// </summary>
    [Tag(Category.Algorithms | Category.Arrays | Category.Sorting)]
    public class MovingZerosTask
    {
        public static int[] MoveZeroes(int[] arr)
        {
            var result = new int[arr.Length];
            var i = 0;
            foreach (var item in arr.Where(x => x != 0))
            {
                result[i++] = item;
            }

            return result;
        }

        [Fact]
        public void Test()
        {
            Assert.Equal(new int[] { 1, 2, 1, 1, 3, 1, 0, 0, 0, 0 }, MoveZeroes(new int[] { 1, 2, 0, 1, 0, 1, 0, 3, 0, 1 }));
        }
    }
}
