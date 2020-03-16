using System.Collections.Generic;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.interviewbit.com/problems/copy-list/
    /// </summary>
    [Tag(Category.LinkedLists)]
    public class CopyList
    {
        /// <summary>
        /// 1. Create shallow copy and put all connections to the dictionary
        /// 2. Restore origin references in the copy
        /// </summary>
        public static Node DeepCopy(Node list)
        {
            Node copy = null;
            var pointer = list;
            Dictionary<Node, Node> originalToCopyMap = new Dictionary<Node, Node>();
            while (pointer != null)
            {
                var nodeCopy = new Node(pointer.Data, pointer.Next)
                {
                    RandomPointer = pointer.Next
                };

                originalToCopyMap[pointer] = GetCopyOrSelf(nodeCopy);

                copy = copy ?? nodeCopy;
                pointer = pointer.Next;
            }
            
            pointer = copy;
            while (pointer != null)
            {
                pointer.Next = GetCopyOrSelf(pointer.Next);
                pointer.RandomPointer = GetCopyOrSelf(pointer.RandomPointer);
                
                pointer = pointer.Next;
            }
            
            return copy;

            Node GetCopyOrSelf(Node origin) => origin == null ? null : originalToCopyMap.GetValueOrDefault(origin, origin);
        }

        public class Node
        {
            public int Data { get; }
            
            public Node Next { get; set; }

            public Node RandomPointer { get; set; }

            public Node(int data)
            {
                Data = data;
            }

            public Node(int data, Node next):this(data)
            {
                Next = next;
            }
        }
        
        [Fact]
        public void Test1()
        {
            var node3 = new Node(3);
            var node2 = new Node(2, node3);
            var node1 = new Node(1, node2);

            node3.RandomPointer = node1;
            node1.RandomPointer = node3;
            node2.RandomPointer = node2;

            var copy = DeepCopy(node1);

            Assert.Equal(node1.Data, copy.Data);
            Assert.Equal(node2.Data, copy.Next.Data);
            Assert.Equal(node3.Data, copy.Next.Next.Data);
            
            Assert.NotEqual(node1, copy);
            Assert.NotEqual(node2, copy.Next);
            Assert.NotEqual(node3, copy.Next.Next);
            
            Assert.NotEqual(node1.RandomPointer, copy.RandomPointer);
            Assert.NotEqual(node2.RandomPointer, copy.Next.RandomPointer);
            Assert.NotEqual(node3.RandomPointer, copy.Next.Next.RandomPointer);
        }
    }
}