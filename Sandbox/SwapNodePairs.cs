using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.interviewbit.com/problems/swap-list-nodes-in-pairs/
    /// </summary>
    [Tag(Category.LinkedLists)]
    public class SwapNodePairs
    {
        private Node Swap(Node list)
        {
            if (list?.Next == null)
            {
                return list;
            }

            var newList = list.Next;
            
            // 1 -> 2 -> ...
            var pointer1 = list;
            Node previous = null;
            while (pointer1?.Next != null)
            {
                var pointer2 = pointer1.Next;
                var tail = pointer2.Next;

                pointer2.Next = pointer1; // 1 <-> 2  ...
                pointer1.Next = tail; // ... <- 1 <- 2

                if (previous != null)
                {
                    previous.Next = pointer2;
                }
                previous = pointer1;
                pointer1 = tail;
            }

            return newList;
        }

        [Fact]
        public void Test1()
        {
            Node list = null;
            var swapped = Swap(list);
            Assert.Null(list);
        }

        [Fact]
        public void Test2()
        {
            var list = new Node();
            var swapped = Swap(list);
            Assert.Equal(0, swapped.Data);
            Assert.Null(list.Next);
        }
        
        
        [Fact]
        public void Test3()
        {
            Node list = new Node()
            {
                Data = 1,
                Next = new Node()
                {
                    Data = 2
                }
            };
            var swapped = Swap(list);
            Assert.Equal(2, swapped.Data);
            Assert.Equal(1, swapped.Next.Data);
        }

        [Fact]
        public void Test4()
        {
            Node list = new Node()
            {
                Data = 1,
                Next = new Node()
                {
                    Data = 2,
                    Next = new Node()
                    {
                        Data = 3
                    }
                }
            };
            var swapped = Swap(list);
            Assert.Equal(2, swapped.Data);
            Assert.Equal(1, swapped.Next.Data);
            Assert.Equal(3, swapped.Next.Next.Data);
        }

        [Fact]
        public void Test5()
        {
            Node list = new Node()
            {
                Data = 1,
                Next = new Node()
                {
                    Data = 2,
                    Next = new Node()
                    {
                        Data = 3,
                        Next = new Node()
                        {
                            Data = 4
                        }
                    }
                }
            };
            var swapped = Swap(list);
            Assert.Equal(2, swapped.Data);
            Assert.Equal(1, swapped.Next.Data);
            Assert.Equal(4, swapped.Next.Next.Data);
            Assert.Equal(3, swapped.Next.Next.Next.Data);
        }

        class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }
        }
    }
}