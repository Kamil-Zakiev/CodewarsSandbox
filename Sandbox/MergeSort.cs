using System.Text;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// Merge two ordered linked lists into one ordered linked list (modify existing lists) 
    /// </summary>
    [Tag(Category.LinkedLists)]
    public class MergeSort
    {
        private Node Merge(Node list1, Node list2)
        {
            if (list1 == null || list2 == null || list1 == list2)
            {
                return list1 ?? list2;
            }

            Node newList = null, last = null;
            var (p1, p2) = (list1, list2);
            
            (p1, p2) = p1.Data < p2.Data ? (p1, p2) : (p2, p1);
            
            var nextForP1 = p1.Next;
            
            p1.Next = null;
            last = newList = p1;
            
            p1 = nextForP1;
            if (p1 == null)
            {
                last.Next = p2;
                return newList;
            }
            
            while (true)
            {
                (p1, p2) = p1.Data < p2.Data ? (p1, p2) : (p2, p1);
                
                nextForP1 = p1.Next;
                p1.Next = null;
                last.Next = p1;
                
                p1 = nextForP1;
                if (p1 == null)
                {
                    last.Next.Next = p2;
                    return newList;
                }

                last = last.Next;
            }
        }

        [Fact]
        public void Test1()
        {
            var list1 = new Node()
            {
                Data = 1,
                Next = new Node()
                {
                    Data = 3
                }
            };
            
            var list2 = new Node()
            {
                Data = 2,
                Next = new Node()
                {
                    Data = 4
                }
            };

            var list3 = Merge(list1, list2);
            
            Assert.Equal(1, list3.Data);
            Assert.Equal(2, list3.Next.Data);
            Assert.Equal(3, list3.Next.Next.Data);
            Assert.Equal(4, list3.Next.Next.Next.Data);
        }
        
        [Fact]
        public void Test2()
        {
            var list1 = new Node()
            {
                Data = 1,
                Next = new Node()
                {
                    Data = 2
                }
            };
            
            var list2 = new Node()
            {
                Data = 3,
                Next = new Node()
                {
                    Data = 4
                }
            };

            var list3 = Merge(list1, list2);
            
            Assert.Equal(1, list3.Data);
            Assert.Equal(2, list3.Next.Data);
            Assert.Equal(3, list3.Next.Next.Data);
            Assert.Equal(4, list3.Next.Next.Next.Data);
        }
        
        [Fact]
        public void Test3()
        {
            var list1 = new Node()
            {
                Data = 1,
                Next = new Node()
                {
                    Data = 4
                }
            };
            
            var list2 = new Node()
            {
                Data = 2,
                Next = new Node()
                {
                    Data = 3
                }
            };

            var list3 = Merge(list1, list2);
            
            Assert.Equal(1, list3.Data);
            Assert.Equal(2, list3.Next.Data);
            Assert.Equal(3, list3.Next.Next.Data);
            Assert.Equal(4, list3.Next.Next.Next.Data);
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