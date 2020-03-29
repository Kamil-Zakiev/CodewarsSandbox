using System;
using System.Text;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.interviewbit.com/problems/partition-list/
    /// </summary>
    [Tag(Category.LinkedLists)]
    public class PartitionList
    {
        private Node MakePartition(Node list, int x)
        {
            // a -> b -> c -> ...
            // list1 to hold nodes with value <  x
            // list2 to hold nodes with value >= x
            // link (list1 -> list2) <=== result

            if (list == null)
            {
                throw new ArgumentNullException();
            }
            
            // p - pointer to the last node
            Node list1 = null, list2 = null, p1 = null, p2 = null;
            var p = list;
            while (p != null)
            {
                var nextP = p.Next;
                p.Next = null;
                
                if (p.Data < x)
                {
                    // put in list1
                    Add(ref list1, ref p1, p);
                }
                else
                {
                    // put in list2
                    Add(ref list2, ref p2, p);
                }
                
                p = nextP;
            }

            if (p1 == null)
            {
                return list2;
            }

            p1.Next = list2;
            return list1;

            void Add(ref Node head, ref Node pToLastNode, Node nodeToAdd)
            {
                if (head == null)
                {
                    head = pToLastNode = nodeToAdd;
                }
                else
                {
                    pToLastNode.Next = nodeToAdd;
                    pToLastNode = pToLastNode.Next;
                }
            }
        }
        
        [Fact]
        public void Test1()
        {
            var original = CreateList(1, 5, 2, 6, 1, 4, 3);
            var expected = CreateList(1, 2, 1, 5, 6, 4, 3);
            const int x = 3;

            var partitioned = MakePartition(original, x);
            
            Assert.Equal(expected.ToString(), partitioned.ToString());
        }
        
        [Fact]
        public void Test2()
        {
            var original = CreateList(1, 5, 2, 6, 1, 4, 3);
            var expected = CreateList(1, 5, 2, 6, 1, 4, 3);
            const int x = 7;

            var partitioned = MakePartition(original, x);
            
            Assert.Equal(expected.ToString(), partitioned.ToString());
        }

        [Fact]
        public void Test3()
        {
            var original = CreateList(1, 5, 2, 6, 1, 4, 3);
            var expected = CreateList(1, 5, 2, 6, 1, 4, 3);
            const int x = -3;

            var partitioned = MakePartition(original, x);
            
            Assert.Equal(expected.ToString(), partitioned.ToString());
        }

        private Node CreateList(params int[] values)
        {
            if (values.Length == 0)
            {
                throw new ArgumentException();
            }
            
            var head = new Node()
            {
                Data = values[0]
            };

            var p = head;
            for (var i = 1; i < values.Length; i++)
            {
                p.Next = new Node()
                {
                    Data = values[i]
                };

                p = p.Next;
            }

            return head;
        }
        
        class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }

            public override string ToString()
            {
                var sb = new StringBuilder();
                var p = this;
                while (p != null)
                {
                    sb.Append(p.Data);

                    p = p.Next;

                    if (p != null)
                    {
                        sb.Append(" -> ");
                    }
                }
                
                return sb.ToString();
            }
        }
    }
}