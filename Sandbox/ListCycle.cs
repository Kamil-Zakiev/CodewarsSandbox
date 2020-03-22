using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.interviewbit.com/problems/list-cycle/
    /// </summary>
    [Tag(Category.LinkedLists)]
    public class ListCycle
    {
        private Node GetCycleStartNode(Node list)
        {
            Node slowPointer;
            var fastPointer = slowPointer = list;

            do
            {
                slowPointer = slowPointer?.Next;
                fastPointer = fastPointer?.Next?.Next;
            } while (fastPointer != null && slowPointer != fastPointer);

            if (fastPointer == null)
            {
                return null;
            }

            slowPointer = list;
            while (slowPointer != fastPointer)
            {
                slowPointer = slowPointer.Next;
                fastPointer = fastPointer.Next;
            }
            
            return slowPointer;
        }

        [Fact]
        public void Test1()
        {
            var node1 = new Node()
            {
                Data = 1
            };
            var node2 = new Node()
            {
                Data = 2
            };
            var node3 = new Node()
            {
                Data = 3
            };
            var node4 = new Node()
            {
                Data = 4
            };

            node1.Next = node2;
            node2.Next = node3;
            node3.Next = node4;
            node4.Next = node2;

            var loopStart = GetCycleStartNode(node1);
            
            Assert.Equal(node2, loopStart);
        }

        [Fact]
        public void Test2()
        {
            var node1 = new Node()
            {
                Data = 1
            };
            var node2 = new Node()
            {
                Data = 2
            };
            var node3 = new Node()
            {
                Data = 3
            };
            var node4 = new Node()
            {
                Data = 4
            };

            node1.Next = node2;
            node2.Next = node3;
            node3.Next = node4;
            node4.Next = null;

            var loopStart = GetCycleStartNode(node1);
            
            Assert.Null(loopStart);
        }

        class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }
        }
    }
}