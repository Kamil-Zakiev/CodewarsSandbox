using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.interviewbit.com/problems/add-two-numbers-as-lists/
    /// </summary>
    [Tag(Category.LinkedLists)]
    public class AddTwoNumbers
    {
        private Node Sum(Node list1, Node list2)
        {
            if (list1 == null || list2 == null)
            {
                return list1 ?? list2;
            }

            Node sumList = null;
            Node pointer = null;
            var hasOverflow = false;
            while (list1 != null && list2 != null)
            {
                var sum = list1.Data + list2.Data + (hasOverflow ? 1 : 0);
                hasOverflow = sum > 9;
                var node =  new Node()
                {
                    Data = sum % 10
                };
                
                if (sumList == null)
                {
                    pointer = sumList = node;
                }
                else
                {
                    pointer.Next = node;
                    pointer = pointer.Next;
                }
                
                list1 = list1.Next;
                list2 = list2.Next;
            }

            var remainder = list1 ?? list2;
            while (remainder != null)
            {
                var sum = remainder.Data + (hasOverflow ? 1 : 0);
                hasOverflow = sum > 9;
                
                pointer.Next = new Node()
                {
                    Data = sum % 10
                };
                pointer = pointer.Next;
                
                remainder = remainder.Next;
            }
            
            return sumList;
        }

        [Fact]
        public void Test1()
        {
            var list1 = CreateList(2, 4, 3);
            var list2 = CreateList(5, 6, 4);

            var list3 = Sum(list1, list2);
            
            Assert.Equal(7, list3.Data);
            Assert.Equal(0, list3.Next.Data);
            Assert.Equal(8, list3.Next.Next.Data);
            Assert.Null(list3.Next.Next.Next);
        }
        
        [Fact]
        public void Test2()
        {
            var list2 = CreateList(5, 6, 4);

            var list3 = Sum(null, list2);
            
            Assert.Equal(list2, list3);
        }

        [Fact]
        public void Test3()
        {
            var list1 = CreateList(2, 4, 3);

            var list3 = Sum(list1, null);
            
            Assert.Equal(list1, list3);
        }

        [Fact]
        public void Test4()
        {
            var list1 = CreateList(2, 4, 3);
            var list2 = CreateList(5, 6, 4, 1);

            var list3 = Sum(list1, list2);
            
            Assert.Equal(7, list3.Data);
            Assert.Equal(0, list3.Next.Data);
            Assert.Equal(8, list3.Next.Next.Data);
            Assert.Equal(1, list3.Next.Next.Next.Data);
            Assert.Null(list3.Next.Next.Next.Next);
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

        class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }
        }
    }
}