using System;
using System.Text;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.interviewbit.com/problems/remove-nth-node-from-list-end/
    /// </summary>
    [Tag(Category.LinkedLists)]
    public class RemoveNthEndNode
    {
        private Node RemoveNthEnd(Node list, int n)
        {
            // O(n) complexity 
            // my approach:	k + (k-n)   = 2k - n
            // optimal:		n + 2*(k-n) = 2k - n
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            var count = 0;
            var p = list;
            while (p != null)
            {
                count++;
                p = p.Next;
            }

            if (n >= count)
            {
                return list.Next;
            }
            
            var skipCount = count - n;
            p = list;
            while (skipCount != 1)
            {
                p = p.Next;
                skipCount--;
            }

            p.Next = p.Next.Next;

            return list;
        }
        
        [Fact]
        public void Test1()
        {
            var list = CreateList(1, 2, 3, 4, 5);
            var n = 2;

            list = RemoveNthEnd(list, n);
            
            Assert.Equal(list.ToString(), CreateList(1,2,3,5).ToString());
        }

        [Fact]
        public void Test2()
        {
            var list = CreateList(1, 2, 3, 4, 5);
            var n = 5;

            list = RemoveNthEnd(list, n);

            Assert.Equal(list.ToString(), CreateList(2, 3, 4, 5).ToString());
        }
        
        [Fact]
        public void Test3()
        {
            var list = CreateList(1, 2, 3, 4, 5);
            var n = 6;

            list = RemoveNthEnd(list, n);

            Assert.Equal(list.ToString(), CreateList(2, 3, 4, 5).ToString());
        }

        
        [Fact]
        public void Test4()
        {
            var list = CreateList(1, 2, 3, 4, 5);
            var n = 1;

            list = RemoveNthEnd(list, n);

            Assert.Equal(list.ToString(), CreateList(1, 2, 3, 4).ToString());
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