using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/5779624bae28070489000146
    /// </summary>
    [Tag(Category.Algorithms | Category.DataStructures | Category.Graphs | Category.Arrays | Category.Lists)]
    public class LogisticMapTask
    {
        public static int[,] LogisticMap(int width, int height, int[] xs, int[] ys)
        {
            return B(width, height, xs, ys);
        }

        public static  int[,] A(int width, int height, int[] xs, int[] ys)
        {
            const int NOT_SUPPLIED = -1;
            var a = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    a[i, j] = NOT_SUPPLIED;
                }
            }

            var nextLayer = 0;
            var peers = xs.Zip(ys, (x, y) => (x, y)).ToHashSet();
            while (peers.Any())
            {
                var nextPeers = new HashSet<(int x, int y)>();

                void AddNeighbours(int x, int y)
                {
                    var neighbours = new[]
                        {
                            (x: x - 1, y: y),
                            (x: x + 1, y: y),
                            (x: x, y: y - 1),
                            (x: x, y: y + 1),
                        }
                        .Where(p => p.x >= 0 && p.x < width && p.y >= 0 && p.y < height)
                        .ToArray();

                    var existed = neighbours.Where(p => a[p.y, p.x] == NOT_SUPPLIED);
                    foreach (var neighbour in existed)
                    {
                        nextPeers.Add(neighbour);
                    }
                }

                foreach (var (x, y) in peers)
                {
                    a[y, x] = nextLayer;
                    AddNeighbours(x, y);
                }

                nextLayer++;
                peers = nextPeers;
            }

            return a;
        }

        public static int[,] B(int width, int height, int[] xs, int[] ys)
        {
            const int NOT_SUPPLIED = -1;
            var map = new int[height, width];
            var supplyPoints = xs.Zip(ys, (x, y) => (x, y)).ToArray();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = supplyPoints.Any()
                        ? supplyPoints.Min(p => Math.Abs(p.x - j) + Math.Abs(p.y - i))
                        : NOT_SUPPLIED;
                }
            }

            return map;
        }

        [Fact]
        public void BasicTests()
        {
            var xs = new int[] { 0 };
            var ys = new int[] { 0 };
            var val = LogisticMap(3, 3, xs, ys);
            var ans = new int[3, 3]
            {
                { 0, 1, 2 }, 
                { 1, 2, 3 }, 
                { 2, 3, 4 }
            };
            Assert.Equal(ans, val);

            xs = new int[] { 2 };
            ys = new int[] { 2 };
            val = LogisticMap(3, 3, xs, ys);
            ans = new int[3, 3]
            {
                { 4, 3, 2 }, 
                { 3, 2, 1 }, 
                { 2, 1, 0 }
            };
            Assert.Equal(ans, val);

            xs = new int[] { 0 };
            ys = new int[] { 0 };
            val = LogisticMap(1, 1, xs, ys);
            ans = new int[1, 1] { { 0 } };
            Assert.Equal(ans, val);

            xs = new int[] { 0, 4 };
            ys = new int[] { 0, 0 };
            val = LogisticMap(5, 2, xs, ys);
            ans = new int[2, 5]
            {
                { 0, 1, 2, 1, 0 }, 
                { 1, 2, 3, 2, 1 }
            };
            Assert.Equal(ans, val);

            xs = new int[] { };
            ys = new int[] { };
            val = LogisticMap(2, 2, xs, ys);
            ans = new int[2, 2] { { -1, -1 }, { -1, -1 } };
            Assert.Equal(ans, val);
        }
    }
}
