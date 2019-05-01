using System.Collections.Generic;
using System.Linq;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/sort-binary-tree-by-levels/train/csharp
    /// </summary>
    public class SortBinaryTreeByLevels
    {
        public static List<int> TreeByLevels(Node node)
        {
            var nodes = new List<Node>();
            void AddIfNotNull(Node n)
            {
                if (n != null)
                {
                    nodes.Add(n);
                }
            }

            var k = 0;
            AddIfNotNull(node);
            while (k != nodes.Count)
            {
                AddIfNotNull(nodes[k].Left);
                AddIfNotNull(nodes[k].Right);

                k++;
            }

            return nodes.Select(n => n.Value).ToList();
        }
    }

    public class Node
    {
        public Node Left;
        public Node Right;
        public int Value;

        public Node(Node l, Node r, int v)
        {
            Left = l;
            Right = r;
            Value = v;
        }
    }
}
