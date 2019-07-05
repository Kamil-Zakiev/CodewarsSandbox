using System;
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
            return MaxScorePriorToPointer(scores, weights, scores.Length - 1, capacity);
        }

        public static int MaxScorePriorToPointer(int[] prices, int[] weights, int pointer, int capacity)
        {
            if (capacity == 0 || pointer == -1)
            {
                return 0;
            }

            var pointedIsNotIncluded = MaxScorePriorToPointer(prices, weights, pointer - 1, capacity);
            var leftCapacity = capacity - weights[pointer];
            var pointedIsInculed = leftCapacity >= 0
                ? prices[pointer] + MaxScorePriorToPointer(prices, weights, pointer - 1, leftCapacity)
                : 0;

            return Math.Max(pointedIsNotIncluded, pointedIsInculed);
        }

        [Fact]
        public static void ExampleTests()
        {
            Assert.Equal(29, PackBagpack(new[] {15, 10, 9, 5}, new[] {1, 5, 3, 4}, 8));
            Assert.Equal(60, PackBagpack(new[] {20, 5, 10, 40, 15, 25}, new[] { 1, 2,  3,  8,  7,  4}, 10));
        }
    }
}