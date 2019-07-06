using System;
using System.Collections.Generic;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/5845e3f680a8cf0bad00017d
    /// </summary>
    [Tag(Category.Algorithms | Category.DynamicProgramming)]
    public class MinPathSquare
    {
        private static Dictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

        public static int MinPath(int[,] grid, int x, int y)
        {
            _cache.Clear();
            return A(grid, x, y);
        }

        private static int A(int[,] grid, int x, int y)
        {
            if (_cache.ContainsKey((x, y)))
            {
                return _cache[(x, y)];
            }

            var current = grid[y, x];
            if (x == 0 && y == 0)
            {
                return current;
            }
            else if (x == 0)
            {
                return current + A(grid, 0, y - 1);
            }
            else if (y == 0)
            {
                return current + A(grid, x - 1, 0);
            }

            var path1 = A(grid, x - 1, y);
            var path2 = A(grid, x, y - 1);
            var path = current + Math.Min(path1, path2);
            _cache.Add((x, y), path);
            return path;
        }

        private static int B(int[,] grid, int x, int y)
        {
            var totalCost = new int[y + 1, x + 1];
            totalCost[0, 0] = grid[0, 0];
            for (var i = 1; i <= y; i++)
            {
                totalCost[i, 0] = totalCost[i - 1, 0] + grid[i, 0];
            }

            for (int j = 1; j <= x; j++)
            {
                totalCost[0, j] = totalCost[0, j - 1] + grid[0, j];
            }

            for (var i = 1; i <= y; i++)
            {
                for (var j = 1; j <= x; j++)
                {
                    totalCost[i, j] = grid[i, j] + Math.Min(totalCost[i - 1, j], totalCost[i, j - 1]);
                }
            }

            return totalCost[y, x];
        }

        private static void FillCosts(int[,] totalCost, int[,] grid, int i, int j)
        {
            if (i == 0)
            {
                totalCost[i, j] = grid[i, j] + totalCost[i, j - 1];
            }


        }

        private static int[,] smallSquare = new int[,]
        {
            { 1, 2, 3, 6, 2, 8, 1 },
            { 4, 8, 2, 4, 3, 1, 9 },
            { 1, 5, 3, 7, 9, 3, 1 },
            { 4, 9, 2, 1, 6, 9, 5 },
            { 7, 6, 8, 4, 7, 2, 6 },
            { 2, 1, 6, 2, 4, 8, 7 },
            { 8, 4, 3, 9, 2, 5, 8 }
        };

        [Fact]
        public static void SmallTests()
        {
            //Assert.Equal(2, smallSquare[2, 1]);
            Assert.Equal(1, MinPathSquare.MinPath(smallSquare, 0, 0));
            Assert.Equal(5, MinPathSquare.MinPath(smallSquare, 0, 1));
            Assert.Equal(11, MinPathSquare.MinPath(smallSquare, 2, 2));
            Assert.Equal(24, MinPathSquare.MinPath(smallSquare, 4, 2));
            Assert.Equal(39, MinPathSquare.MinPath(smallSquare, 6, 6));
            Assert.Equal(24, MinPathSquare.MinPath(smallSquare, 4, 5));
        }

        private static int[,] mySquare1 = new int[,]
        {
            { 1, 1, 3, 6, 2, 8, 1 },
            { 4, 1, 1, 4, 3, 1, 9 },
            { 1, 5, 1, 7, 9, 3, 1 },
            { 4, 9, 1, 1, 6, 9, 5 },
            { 7, 6, 8, 1, 1, 2, 6 },
            { 2, 1, 6, 2, 1, 1, 1 },
            { 8, 4, 3, 9, 2, 5, 1 }
        };

        private static int[,] mySquare2 = new int[,]
        {
            { 1, 1, 1, 1, 1, 1, 1 },
            { 4, 8, 2, 4, 3, 1, 1 },
            { 1, 5, 3, 7, 9, 3, 1 },
            { 4, 9, 2, 1, 6, 9, 1 },
            { 7, 6, 8, 4, 7, 2, 1 },
            { 2, 1, 6, 2, 4, 8, 1 },
            { 8, 4, 3, 9, 2, 5, 1 }
        };

        [Fact]
        public static void MyTests()
        {
            Assert.Equal(13, MinPath(mySquare1, 6, 6));

            //Assert.Equal(13, MinPath(mySquare2, 6, 6));
            Assert.Equal(7, MinPath(mySquare2, 5, 1));
        }
    }
}
