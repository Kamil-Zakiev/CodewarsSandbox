using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/5a51717fa7ca4d133f001fdf
    /// </summary>
    [Tag(Category.Algorithms | Category.DynamicProgramming)]
    public class Packer
    {
        public static int PackBagpack(int[] scores, int[] weights, int capacity)
        {
            var items = scores.Zip(weights, (score, weight) => (score, weight)).ToArray();
            
            
            
            return 0;
        }
        
        [Fact(Skip = "Not implemented yet.")]
        public static void ExampleTests()
        {
            Assert.Equal(29, PackBagpack(new[] {15, 10, 9, 5}, new[] {1, 5, 3, 4}, 8));
            Assert.Equal(60, PackBagpack(new[] {20, 5, 10, 40, 15, 25}, new[] {1, 2, 3, 8, 7, 4}, 10));
        }
    }
}