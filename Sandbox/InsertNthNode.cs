using System;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/55cacc3039607536c6000081
    /// </summary>
    [Tag(Category.Fundamentals | Category.LinkedLists | Category.Lists | Category.DataStructures)]
    public class InsertNthNode
    {
        public class Node
        {
            public int Data;
            public Node Next;
  
            public Node(int data)
            {
                this.Data = data;
                this.Next = null;
            }
  
            public Node(int data, Node next)
            {
                this.Data = data;
                this.Next = next;
            }
            
            public static Node InsertNth(Node head, int index, int data)
            {
                if (index == 0)
                {
                    return new Node(data, head);
                }

                var i = 0;
                var pointer = head;
                while (i++ != index - 1)
                {
                    pointer = pointer.Next;
                    if (pointer == null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }

                pointer.Next = new Node(data, pointer.Next);
                return head;
            }
        }
    }
}