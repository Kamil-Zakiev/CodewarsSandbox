using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/582c5382f000e535100001a7
    /// </summary>
    [Tag(Category.Algorithms | Category.LinkedLists | Category.Lists | Category.DataStructures)] 
    public class ParseLinkedListFromString
    {
        private const string NullList = "null";
        private const string ItemSeparator = " -> ";
        
        public static Node Parse(string nodes)
        {
            if (nodes.Equals(NullList))
            {
                return null;
            }

            return nodes
                .Split(ItemSeparator)
                .SkipLast(1)
                .Reverse()
                .Select(int.Parse)
                .Aggregate<int, Node>(null, (node, y) => new Node(y, node));
        }
        
        public class Node
        {
            public int Data;
            public Node Next;

            public Node(int data, Node next = null)
            {
                this.Data = data;
                this.Next = next;
            }

            public override bool Equals(Object obj)
            {
                // Check for null values and compare run-time types.
                if (obj == null || GetType() != obj.GetType()) { return false; }

                return this.ToString() == obj.ToString();
            }

            public override string ToString()
            {
                List<int> result = new List<int>();
                Node curr = this;

                while (curr != null)
                {
                    result.Add(curr.Data);
                    curr = curr.Next;
                }

                return String.Join(" -> ", result) + " -> null";
            }
        }
        
        [Fact]
        public void SampleTest()
        {
            Assert.Equal(new Node(1, new Node(2, new Node(3))), Parse("1 -> 2 -> 3 -> null"));
            Assert.Equal(new Node(0, new Node(1, new Node(4, new Node(9, new Node(16))))), Parse("0 -> 1 -> 4 -> 9 -> 16 -> null"));
            Assert.Null(Parse("null"));
        }
    }
}