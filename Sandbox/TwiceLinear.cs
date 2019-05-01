using System;
using System.Collections.Generic;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/5672682212c8ecf83e000050
    /// </summary>
    class TwiceLinear
    {
        public static int DblLinear4(int n)
        {
            var list = new List<int>() { 1 };

            void AddToSortedList(int edgePointer, int value)
            {
                var start = edgePointer + 1;
                var end = Math.Min(list.Count - 1, n);

                if (list[end] < value)
                {
                    if (end != n)
                    {
                        list.Add(value);
                    }

                    return;
                }

                while (end - start > 1)
                {
                    var middle = (start + end) / 2;
                    var middleValue = list[middle];

                    if (middleValue == value)
                    {
                        return;
                    }

                    if (middleValue < value)
                    {
                        start = middle + 1;
                    }
                    else
                    {
                        end = middle - 1;
                    }
                }

                if (list[start] == value || list[end] == value)
                {
                    return;
                }

                var pasteIndex = value < list[start]
                    ? start
                    : value < list[end]
                        ? end
                        : end + 1;
                list.Insert(pasteIndex, value);
            }

            var pointer = 0;
            while (pointer != n)
            {
                var x = list[pointer];

                var y = 2 * x + 1;
                var z = y + x;
                AddToSortedList(pointer, y);
                AddToSortedList(pointer, z);

                pointer++;
            }

            return list[n];
        }
    }
}
