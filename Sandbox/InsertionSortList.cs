using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.interviewbit.com/problems/insertion-sort-list/
    /// </summary>
    [Tag(Category.LinkedLists | Category.Sorting)]
    public class InsertionSortList
    {
        public static Node Sort(Node list)
        {
            if (list == null)
            {
                return null;
            }

            var ordered = list;
            var next = list.Next;
            ordered.Next = null;

            if (next == null)
            {
                return ordered;
            }

            do
            {
                if (ordered.Data > next.Data)
                {
                    var temp = next.Next;
                    var newOrdered = next;
                    next.Next = ordered;
                    next = temp;
                    ordered = newOrdered;
                    
                    continue;
                }

                var pointer = ordered;
                while (pointer.Next != null && pointer.Next.Data < next.Data)
                {
                    pointer = pointer.Next;
                }

                Node furtherNext;
                if (pointer.Next == null)
                {
                    pointer.Next = next;
                    furtherNext = next.Next;
                    next.Next = null;
                }
                else
                {
                    var temp = pointer.Next;
                    pointer.Next = next;
                    furtherNext = next.Next;
                    next.Next = temp;
                }

                next = furtherNext;
            } while (next != null);
            
            return ordered;
        }

        public class Node
        {
            public int Data { get; set; }

            public Node Next { get; set; }
        }

        [Theory]
        [InlineData(3,1,2)]
        [InlineData(3,2,1)]
        [InlineData(2,1,3)]
        [InlineData(2,3,1)]
        [InlineData(1,2,3)]
        [InlineData(1,3,2)]
        public void Test1(int a, int b, int c)
        {
            var list = CreateList(a, b, c);
            
            var ordered = Sort(list);
            
            Assert.Equal(1, ordered.Data);
            Assert.Equal(2, ordered.Next.Data);
            Assert.Equal(3, ordered.Next.Next.Data);
        }
        
        [Fact]
        public void Test2()
        {
            var list = CreateList();
            
            var ordered = Sort(list);
            
            Assert.Null(ordered);
        }
        
        [Fact]
        public void Test3()
        {
            var list = CreateList(1);
            
            var ordered = Sort(list);
            
            Assert.Equal(1, ordered.Data);
        }

        private Node CreateList(params int[] data)
        {
            Node list = null;
            for (var i = data.Length - 1; i >= 0; i--)
            {
                list = new Node()
                {
                    Data = data[i],
                    Next = list
                };
            }

            return list;
        }
    }
}