using System;
using System.Text;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.interviewbit.com/problems/sort-list/
    /// </summary>
    /// <remarks>
    /// Not completed
    /// </remarks>
    [Tag(Category.Sorting | Category.LinkedLists)]
    public class LinkedListMergeSort
    {
        private void Sort(ref Node list)
        {
            var head = list;

            var count = 0;
            var p = head;
            while (p != null)
            {
                p = p.Next;
                count++;
            }
            
            var h = 2;
            while (h < count)
            {
                Node previous = null;
                p = head;
                while (p != null)
                {
                    var sortedPartSize = h / 2;
                    var firstHead = p;
                    for (var i = 0; i < sortedPartSize - 1; i++)
                    {
                        p = p.Next;
                    }

                    var tempNext = p.Next;
                    p.Next = null;
                    p = tempNext;

                    var secondHead = p;
                    for (var i = 0; i < sortedPartSize - 1 && p != null; i++)
                    {
                        p = p.Next;
                    }

                    if (p != null)
                    {
                        tempNext = p.Next;
                        p.Next = null;
                        p = tempNext;
                    }

                    if (p == null)
                    {
                        continue;
                    }

                    var (newFirst, newLast) = MergeSorted(firstHead, secondHead);

                    newLast.Next = p;
                    if (previous == null)
                    {
                        list = newFirst;
                    }
                    else
                    {
                        previous.Next = newFirst;
                    }

                    previous = newLast;
                }

                h *= 2;
            }

            (Node, Node) MergeSorted(Node firstHead, Node secondHead)
            {
                if (secondHead == null)
                {
                    var temp = firstHead;
                    while (temp.Next != null)
                    {
                        temp = temp.Next;
                    }

                    return (firstHead, temp);
                }
                
                var (p1, p2) = firstHead.Data < secondHead.Data
                    ? (firstHead, secondHead)
                    : (secondHead, firstHead);

                var newFirst = p1;
                p1 = p1.Next;
                if (p1 == null)
                {
                    newFirst.Next = p2;
                    var temp = p2;
                    while (temp.Next != null)
                    {
                        temp = temp.Next;
                    }

                    return (newFirst, temp);
                }

                var last = newFirst;
                while (true)
                {
                    (p1, p2) = p1.Data < p2.Data ? (p1, p2) : (p2, p1);
                    last.Next = p1;
                    p1 = p1.Next;
                    last.Next.Next = null;

                    
                    if (p1 == null)
                    {
                        last.Next = p2;
                        var temp = p2;
                        while (temp.Next != null)
                        {
                            temp = temp.Next;
                        }

                        return (newFirst, temp);
                    }

                    last = last.Next;
                }
            }
        }

        [Fact(Skip = "Solution is not ready, need to debug")]
        public void Test()
        {
            var list = CreateList(4, 1, 5, 2, 3);
            
            Sort(ref list);
            
            var expected = CreateList(1, 2, 3, 4, 5).ToString();
            Assert.Equal(expected, list.ToString());
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