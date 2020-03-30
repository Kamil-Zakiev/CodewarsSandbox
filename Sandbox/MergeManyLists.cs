using System;
using System.Linq;
using System.Text;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// Given sorted linked lists, merge them and print the sorted output.
    /// Example:
    /// list1 = 1->3->5->7
    /// list2 = 2->4->6->8
    /// list3 = 0->9->10->11
    /// Output:
    /// list4 = 0->1->2->3->4->5->6->7->8->9->10->11
    /// </summary>
    [Tag(Category.LinkedLists)]
    public class MergeManyLists
    {
        private Node Merge(params Node[] lists)
        {
            if (lists == null || lists.Length == 0 || lists.Any(p => p == null))
            {
                throw new ArgumentNullException();
            }

            if (lists.Length == 1)
            {
                return lists[0];
            }

            var pointers = new Node[lists.Length]; // pointers to nodes of each list
            for (var i = 0; i < pointers.Length; i++)
            {
                pointers[i] = lists[i];
            }

            Node merged = null, lastP = null;
            
            // Time O(k*n), where n - total amount of nodes; k - the number of lists to be merged
            //             - can be enhanced using heap to get the max in O(n*log(k))
            // Memory: O(k),
            while (pointers.Any(NotNull))
            {
                var i = GetNextToBeAdded();
                var nextPointerForI = pointers[i].Next;

                var p = pointers[i];
                p.Next = null;
                if (lastP == null)
                {
                    lastP = merged = p;
                }
                else
                {
                    lastP.Next = p;
                    lastP = lastP.Next;
                }

                pointers[i] = nextPointerForI;
            }
            
            return merged;

            int GetNextToBeAdded()
            {
                int? minimumIndex = null;
                for (var i = 0; i < pointers.Length; i++)
                {
                    if (pointers[i] == null)
                    {
                        continue;
                    }

                    if (!minimumIndex.HasValue)
                    {
                        minimumIndex = i;
                        continue;
                    }

                    if (pointers[minimumIndex.Value].Data > pointers[i].Data)
                    {
                        minimumIndex = i;
                    }
                }

                return minimumIndex ?? throw new InvalidOperationException();
            }
            
            bool NotNull(Node node) => node != null;
        }

        [Fact]
        public void Test()
        {
            var list1 = CreateList(1, 3, 5, 7);
            var list2 = CreateList(2, 4, 6, 8);
            var list3 = CreateList(2, 4, 5, 6);

            var expected = CreateList(1, 2, 2, 3, 4, 4, 5, 5, 6, 6, 7, 8);
            
            Assert.Equal(expected.ToString(), Merge(list1, list2, list3).ToString());
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