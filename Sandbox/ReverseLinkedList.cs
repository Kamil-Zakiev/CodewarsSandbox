using System;
using System.Collections.Generic;
using System.Text;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/55e72695870aae78c4000026
    /// </summary>
    [Tag(Category.Algorithms | Category.LinkedLists | Category.Lists | Category.DataStructures)]
    public class ReverseLinkedList
    {
        public void Reverse(ref Node list)
        {
            if (list == null)
            {
                return;
            }

            Node p1 = null;
            var p2 = list;
            while (p2 != null)
            {
                var p3 = p2.Next;
                p2.Next = p1;
                p1 = p2;
                p2 = p3;
            }

            list = p1;
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
                    Data = 2
                }
            };

            Reverse(ref list);

            Assert.Equal(2, list.Data);
            Assert.Equal(1, list.Next.Data);
            Assert.Null(list.Next.Next);
        }
    }
}
