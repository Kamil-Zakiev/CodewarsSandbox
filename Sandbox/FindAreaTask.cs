using System;
using System.Collections.Generic;
using System.Text;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/59b166f0a35510270800018d
    /// </summary>
    [Tag(Category.Algorithms | Category.DataStructures | Category.Graphs)]
    public class FindAreaTask
    {
        public static double FindArea(List<Point> points)
        {
            double result = 0;

            for (int i = 0; i < points.Count - 1; i++)
            {
                var a = points[i].Y;
                var b = points[i + 1].Y;
                var h = points[i + 1].X - points[i].X;
                result += (a + b) * h;
            }

            return 0.5 * result;
        }

        [Fact]
        public void BaseTest()
        {
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(1, 4),
                new Point(3, 2)
            };

            Assert.Equal(8, FindArea(points));
        }

        public class Point
        {
            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double X { get; set; }
            public double Y { get; set; }
        }
    }
}
