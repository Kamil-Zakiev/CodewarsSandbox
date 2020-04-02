using System;
using System.Text;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.interviewbit.com/problems/reverse-link-list-ii/
    /// </summary>
    [Tag(Category.LinkedLists)]
    public class ReverseLinkedList2
    {
        private Node Revert(Node list, int m, int n)
        {
            if (m <= 0 || n <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (list == null)
            {
                throw new ArgumentNullException();
            }

            var pointer = list;
            var count = 0;
            while (pointer != null)
            {
                pointer = pointer.Next;
                count++;
            }

            if (m >= n || m > count || n > count)
            {
                throw new ArgumentException();
            }
            
            
            // TODO: handle corner cases
            
            // q    p               
            // 1 -> 2 <- 3 <- 4    5
            //               prev  cur

            var usePhantom = m == 1;
            if (usePhantom)
            {
                list = new Node()
                {
                    Next = list
                };
                m++;
                n++;
            }
            
            var q = list;
            var counter = 1;
            while (counter != m - 1)
            {
                q = q.Next;
                counter++;
            }

            var p = q.Next;
            Node prev = p, current = p.Next;
            counter += 2;
            while (counter != n + 1)
            {
                var next = current.Next;
                current.Next = prev;
                
                prev = current;
                current = next;
                
                counter++;
            }

            q.Next = prev;
            p.Next = current;

            return usePhantom ? list.Next : list;
        }

        [Fact]
        public void Test1()
        {
            var original = CreateList(1, 2, 3, 4, 5);
            var expected = CreateList(1, 4, 3, 2, 5);
            var m = 2;
            var n = 4;

            var reverted = Revert(original, m, n);
            
            Assert.Equal(expected.ToString(), reverted.ToString());
        }
        
        [Fact]
        public void Test2()
        {
            var original = CreateList(1, 2, 3, 4, 5);
            var expected = CreateList(4, 3, 2, 1, 5);
            var m = 1;
            var n = 4;

            var reverted = Revert(original, m, n);
            
            Assert.Equal(expected.ToString(), reverted.ToString());
        }

        [Fact]
        public void Test3()
        {
            var original = CreateList(1, 2, 3, 4, 5);
            var expected = CreateList(1, 5, 4, 3, 2);
            var m = 2;
            var n = 5;

            var reverted = Revert(original, m, n);
            
            Assert.Equal(expected.ToString(), reverted.ToString());
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