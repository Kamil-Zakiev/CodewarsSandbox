using Sandbox.HelperUtils;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/54521e9ec8e60bc4de000d6c
    /// </summary>
    [Tag(Category.Fundamentals | Category.Algorithms | Category.Lists | Category.DataStructures | Category.DynamicProgramming)]
    public class MaxSubarraySum
    {
        public static int MaxSequence(int[] arr)
        {
            if (arr.Length == 0)
            {
                return 0;
            }

            var sMax = int.MinValue;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                var s = arr[i];
                if (sMax < s)
                {
                    sMax = s;
                }

                for (int j = i + 1; j < arr.Length; j++)
                {
                    s += arr[j];
                    if (sMax < s)
                    {
                        sMax = s;
                    }
                }
            }

            return sMax;
        }
    }
}
