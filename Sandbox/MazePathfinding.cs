using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/56d08f810f9408079200102f
    /// </summary>
    [Tag(Category.Algorithms | Category.Graphs | Category.DataStructures | Category.Trees)]
    public class MazePathfinding
    {
        public static int[] FindPath(bool[] mazeFlags, int size, int startIndex, int goalIndex)
        {
            var maze = new Maze(mazeFlags, size);
            var graph = new Graph(startIndex);

            var missedOpportunities = new Stack<Graph.Node>();
            var currentPointer = graph.Root;
            while (currentPointer.Cell != goalIndex)
            {
                var (anyWay, firstWay, otherDirections) = maze.GetPossibleDirections(currentPointer.Cell, graph.Nodes);
                if (!anyWay)
                {
                    currentPointer = missedOpportunities.Pop();
                    continue;
                }

                foreach (var otherDirection in otherDirections)
                {
                    missedOpportunities.Push(graph.CreateChildFor(currentPointer, otherDirection));
                }

                currentPointer = graph.Nodes.Contains(firstWay)
                    ? missedOpportunities.Pop()
                    : graph.CreateChildFor(currentPointer, firstWay);
            }

            return currentPointer.Path();
        }

        public class Graph
        {
            public Node Root { get; }

            private HashSet<Node> _nodes = new HashSet<Node>();
            public IReadOnlyCollection<int> Nodes => _nodes.Select(x => x.Cell).ToArray();

            public Graph(int rootData)
            {
                Root = new Node(rootData);
            }

            public Node CreateChildFor(Node parent, int childData)
            {
                var node = new Node(childData, parent);
                _nodes.Add(node);
                return node;
            }

            public class Node
            {
                public int Cell { get; }
                public Node Parent { get; }

                public Node(int cell, Node parent = null)
                {
                    Cell = cell;
                    Parent = parent;
                }

                public int[] Path()
                {
                    var path = new List<int>();
                    var node = this;
                    while (node != null)
                    {
                        path.Add(node.Cell);
                        node = node.Parent;
                    }

                    path.Reverse();
                    return path.ToArray();
                }
            }
        }

        public class Maze
        {
            private readonly bool[] _maze;
            private readonly int _size;

            public Maze(bool[] maze, int size)
            {
                _maze = maze;
                _size = size;
            }

            public (bool, int, int[]) GetPossibleDirections(int cell, IReadOnlyCollection<int> forbidden)
            {
                var possibleDirections = new[] {cell - 1, cell + 1, cell - _size, cell + _size }
                    .Where(x => 0 <= x && x <= _size * _size)
                    .Where(x => _maze[x])
                    .Where(x => !forbidden.Contains(x))
                    .ToArray();

                return (possibleDirections.Any(), possibleDirections.FirstOrDefault(), possibleDirections.Skip(1).ToArray());
            }
        }

        [Fact]
        public void StaticMaze1()
        {
            int[] path = { 36, 37, 38, 31, 24, 25, 26 };
            var maze = new[]
            {
                false, false, false, false, false, false, false,
                false, true, false, true, true, true, false,
                false, true, false, true, false, false, false,
                false, true, true, true, true, true, false,
                false, false, false, true, false, true, false,
                false, true, true, true, false, true, false,
                false, false, false, false, false, false, false,
            };
            Assert.Equal(path, FindPath(maze, 7, 36, 26));
        }

        [Fact]
        public void Test2()
        {
            var maze = new[]
            {
                false, false, false, false, false, false, false,
                false, true, true, true, false, true, false,
                false, true, false, false, false, true, false,
                false, true, true, true, true, true, false,
                false, true, false, true, false, true, false,
                false, true, false, true, false, true, false,
                false, false, false, false, false, false, false
            };
            Assert.NotNull(FindPath(maze, 7, 22, 12));
        }

        [Fact]
        public void Test3()
        {
            var maze = new[]
            {
                false, false, false, false, false, false, false, false, false, false, false,
                false, true, false, true, false, true, false, true, false, true, false,
                false, true, false, true, false, true, false, true, false, true, false,
                false, true, true, true, false, true, false, true, true, true, false,
                false, false, false, true, false, true, false, true, false, true,
                false, false, true, true, true, false, true, true, true, false,
                true, false, false, false, false, true, false, true, false,
                false, false, false, false, false, true, false, true, true,
                true, false, true, false, true, false, false, true, false,
                false, false, true, false, true, false, true, false, false,
                true, true, true, true, true, true, true, true, true, false,
                false, false, false, false, false, false, false, false, false, false, false
            };
            Assert.NotNull(FindPath(maze, 11, 78, 108));
        }

        [Fact]
        public void Test4()
        {
            var maze = new[]
            {
                false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, true, true,
                true, true, true, true, true, false, true, false, true, true,
                true, false, false, true, false, false, false, true, false,
                false, false, true, false, true, false, true, false, false,
                true, true, true, false, true, false, true, false, true, false,
                true, false, true, false, false, true, false, false, false, false,
                false, true, false, true, false, false, false, true, false, false,
                true, false, true, true, true, true, true, true, true, false, true,
                true, true, false, false, true, false, false, false, true, false,
                false, false, false, false, false, false, true, false, false, true,
                false, true, false, true, true, true, false, true, false, true, false,
                true, false, false, true, false, true, false, true, false, false, false,
                true, false, true, false, true, false, false, true, false, true, true, true, false,
                true, false, true, true, true, true, true, false, false, true, false, false, false,
                true, false, true, false, false, false, true, false, true, false, false, true, true,
                true, false, true, true, true, true, true, false, true, false, true, false, false, true,
                false, true, false, false, false, true, false, false, false, true, false, false, false,
                false, true, false, true, true, true, true, true, true, true, true, true, true, true, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false
            };
            Assert.NotNull(FindPath(maze, 15, 46, 28));
        }
    }
}
