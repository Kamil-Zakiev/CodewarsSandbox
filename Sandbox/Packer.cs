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
            
            return A(items, capacity);
        }

        public static int A((int price, int weight)[] items, int capacity)
        {
            if (capacity <= 0 || items.Length == 0)
            {
                return 0;
            }
            
            var (price, weight) = items.Last();
            var exceptLast = items.SkipLast(1).ToArray();
            var a = A(exceptLast, capacity);
            var leftCapacity = capacity - weight;
            if (leftCapacity < 0)
            {
                return a;
            }
            
            var b = price + A(exceptLast, leftCapacity);

            return a > b ? a : b;
        }
        
        [Fact]
        public static void ExampleTests()
        {
            Assert.Equal(29, PackBagpack(new[] {15, 10, 9, 5}, new[] {1, 5, 3, 4}, 8));
            Assert.Equal(60, PackBagpack(
                new[] {20, 5, 10, 40, 15, 25}, 
                new[] { 1, 2,  3,  8,  7,  4},
                10));
        }
    }
}