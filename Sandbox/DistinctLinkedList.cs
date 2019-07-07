using System;
using System.Collections.Generic;
using System.Text;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/linked-lists-remove-duplicates
    /// </summary>
    [Tag(Category.Fundamentals | Category.LinkedLists | Category.Lists | Category.DataStructures)]
    public class DistinctLinkedList
    {
        public Node RemoveDuplicates(Node head)
        {
            if (head == null)
            {
                return head;
            }

            var p1 = head;
            while (p1.Next != null)
            {
                var p2 = p1.Next;
                while (p1.Data == p2.Data)
                {
                    p2 = p2.Next;
                    if (p2 == null)
                    {
                        p1.Next = null;
                        return head;
                    }
                }

                p1.Next = p2;
                p1 = p2;
            }

            return head;
        }

        public class Node
        {
            public int Data;
            public Node Next;
        }

        [Fact]
        public void Test()
        {
            var list = new Node
            {
                Data = 1,
                Next = new Node
                {
                    Data = 1
                }
            };

            list = RemoveDuplicates(list);

            Assert.Equal(1, list.Data);
            Assert.Null(list.Next);
        }
    }
}
