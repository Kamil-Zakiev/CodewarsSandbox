using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox.InsertToSorted
{
    /// <summary>
    /// https://www.codewars.com/kata/55cc33e97259667a08000044
    /// </summary>
    [Tag(Category.Fundamentals | Category.LinkedLists | Category.Lists | Category.DataStructures)]
    public class SortedInsertToLinkedList
    {
        public class Node
        {
            public Node(int data, Node next = null)
            {
                Data = data;
                Next = next;
            }

            public int Data { get; }
            public Node Next { get; set; }

            public static Node Insert(Node head, int data)
            {
                if (head == null || head.Data > data)
                {
                    return new Node(data, head);
                }

                var p = head;
                while (p.Next != null && p.Next.Data < data)
                {
                    p = p.Next;
                }

                p.Next = new Node(data, p.Next);
                return head;
            }
        }

        [Fact]
        public void Test1()
        {
            Assert.Equal(5, Node.Insert(null, 5).Data);
        }

        [Fact]
        public void Test2()
        {
            var head = new Node(1, new Node(3, new Node(5, null)));
            var valueToInsert = -2;
            Assert.Equal(valueToInsert, Node.Insert(head, valueToInsert).Data);
        }

        [Fact]
        public void Test3()
        {
            var head = new Node(1, new Node(3, new Node(5, null)));
            var valueToInsert = 2;
            Assert.Equal(valueToInsert, Node.Insert(head, valueToInsert).Next.Data);
        }

        [Fact]
        public void Test4()
        {
            var head = new Node(1, new Node(3, new Node(5, null)));
            var valueToInsert = 6;
            Assert.Equal(valueToInsert, Node.Insert(head, valueToInsert).Next.Next.Next.Data);
        }
    }
}