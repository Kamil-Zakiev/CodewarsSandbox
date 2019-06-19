using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/55c11989e13716e35f000013
    /// </summary>
    [Tag(Category.Fundamentals | Category.Binary | Category.Bit)]
    public class AddingBinaryNumbers
    {
        public static string Add(string a, string b)
        {
            var residue = '0';
            var result = new List<char>();

            using (var aIterator = a.Reverse().GetEnumerator())
            using (var bIterator = b.Reverse().GetEnumerator())
            {
                bool aCanMove, bCanMove;
                while ((aCanMove = aIterator.MoveNext()) | (bCanMove = bIterator.MoveNext()))
                {
                    var bitA = aCanMove ? aIterator.Current : '0';
                    var bitB = bCanMove ? bIterator.Current : '0';

                    char sum;
                    (residue, sum) = ColumnSum(bitA, bitB, residue);
                    result.Add(sum);
                }
                
                if (residue == '1')
                {
                    result.Add(residue);
                }
                
                result.Reverse();
            }

            return new string(result.SkipWhile((x, i) => i != result.Count - 1 && x == '0').ToArray());
        }

        private static (char residue, char sum) ColumnSum(char bitA, char bitB, char residue)
        {
            if (bitA == '1' && bitB == '1')
            {
                return ('1', residue);
            }

            if (bitA == '1' ^ bitB == '1')
            {
                return residue == '1'
                    ? ('1', '0')
                    : ('0', '1');
            }

            return ('0', residue);
        }

        [Fact]
        public void Test1()
        {
            Assert.Equal("1001", Add("111","10"));     
        }
        
        [Fact]
        public void Test2()
        {
            Assert.Equal("10010", Add("1101","101"));    
        }
        
        [Fact]
        public void Test3()
        {
            Assert.Equal("100100", Add("1101","10111"));
        }
        
        [Fact]
        public void Test4()
        {      
            Assert.Equal("11", Add("0011","00"));     
        }
        
        [Fact]
        public void Test5()
        {
            Assert.Equal("11", Add("00","0011"));      
        }
    }
}