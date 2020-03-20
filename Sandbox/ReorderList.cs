using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
	// https://www.interviewbit.com/problems/reorder-list/
    [Tag(Category.LinkedLists)]
    public class ReorderList
    {
        private void Reorder(Node list)
        {
            if (list == null)
            {
                return;
            }
            
            var count = 0;
            var p = list;
            while (p != null)
            {
                p = p.Next;
                count++;
            }

            var half = count / 2;
            p = list;
            while (half > 0)
            {
                var temp = p;
                p = p.Next;
                half--;

                if (half == 0)
                {
                    temp.Next = null;
                }
            }
            
            // revert the second list
            // 4-5-6  ->  6-5-4
            var q = p;
            var next = p.Next;
            p.Next = null;

            while (next != null)
            {
                var temp = next.Next;
                next.Next = q;
                q = next;
                next = temp;
            }
            
            // merge halves
            var head1 = list;
            var head2 = q;

            Node result = null;
            Node pointerToLast = null;
            while (head1 != null && head2 != null)
            {
                var node1 = head1;
                var node2 = head2;
                head1 = head1.Next;
                head2 = head2.Next;

                node1.Next = node2;
                node2.Next = null;
                
                if (result == null)
                {
                    result = node1;
                }
                else
                {
                    pointerToLast.Next = node1;
                }
                
                pointerToLast = node2;
            }

            var remainder = head1 ?? head2;
            if (pointerToLast != null)
            {
                pointerToLast.Next = remainder;
            }
        }
        
        [Fact]
        public void Test1()
        {
            var list = new Node()
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
                            Data = 4,
                            Next = new Node()
                            {
                                Data = 5,
                                Next = new Node()
                                {
                                    Data = 6
                                }
                            }
                        }
                    }
                }
            };

            Reorder(list);
            
            // 1-6-2-5-3-4
            Assert.Equal(1, list.Data);
            Assert.Equal(6, list.Next.Data);
            Assert.Equal(2, list.Next.Next.Data);
            Assert.Equal(5, list.Next.Next.Next.Data);
            Assert.Equal(3, list.Next.Next.Next.Next.Data);
            Assert.Equal(4, list.Next.Next.Next.Next.Next.Data);
            Assert.Null(list.Next.Next.Next.Next.Next.Next);
        }

        
        [Fact]
        public void Test2()
        {
            var list = new Node()
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
                            Data = 4,
                            Next = new Node()
                            {
                                Data = 5
                            }
                        }
                    }
                }
            };

            Reorder(list);
            
            // 1-5-2-4-3
            Assert.Equal(1, list.Data);
            Assert.Equal(5, list.Next.Data);
            Assert.Equal(2, list.Next.Next.Data);
            Assert.Equal(4, list.Next.Next.Next.Data);
            Assert.Equal(3, list.Next.Next.Next.Next.Data);
            Assert.Null(list.Next.Next.Next.Next.Next);
        }
        
        [Fact]
        public void Test3()
        {
            var list = new Node()
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

            Reorder(list);
            
            // 1-3-2
            Assert.Equal(1, list.Data);
            Assert.Equal(3, list.Next.Data);
            Assert.Equal(2, list.Next.Next.Data);
            Assert.Null(list.Next.Next.Next);
        }

        [Fact]
        public void Test4()
        {
            var list = new Node()
            {
                Data = 1,
                Next = new Node()
                {
                    Data = 2
                }
            };

            Reorder(list);
            
            // 1-3-2
            Assert.Equal(1, list.Data);
            Assert.Equal(2, list.Next.Data);
            Assert.Null(list.Next.Next);
        }

        [Fact]
        public void Test5()
        {
            var list = new Node()
            {
                Data = 1
            };

            Reorder(list);
            
            // 1-3-2
            Assert.Equal(1, list.Data);
            Assert.Null(list.Next);
        }
        
        [Fact]
        public void Test6()
        {
            Reorder(null);
            
            Assert.True(true);
        }

        class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }
        }
    }
}