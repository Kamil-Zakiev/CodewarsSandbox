using System.Collections.Generic;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/529bf0e9bdf7657179000008
    /// </summary>
    [Tag(Category.Algorithms | Category.DataStructures | Category.Validation)]
    public class Sudoku
    {
        public static bool ValidateSolution(int[][] board)
        {
            var subGridValidators = new HashSet<int>[9];
            var rowValidators = new HashSet<int>[9];
            var columnValidators = new HashSet<int>[9];
            for (var i = 0; i < 9; i++)
            {
                subGridValidators[i] = new HashSet<int>();
                rowValidators[i] = new HashSet<int>();
                columnValidators[i] = new HashSet<int>();
            }

            bool TryAddUnique(int digit, HashSet<int> set)
            {
                if (set.Contains(digit))
                {
                    return false;
                }

                set.Add(digit);
                return true;
            }

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var item = board[i][j];
                    var subGrid = i / 3 * 3 + j / 3;
                    if (item == 0 
                        || !TryAddUnique(item, rowValidators[i])
                        || !TryAddUnique(item, columnValidators[j])
                        || !TryAddUnique(item, subGridValidators[subGrid]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        [Fact]
        public void TestSudokuValidator()
        {
            var board1 = new[]
            {
                new[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                new[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                new[] {1, 9, 8, 3, 4, 2, 5, 6, 7},
                new[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
                new[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                new[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
                new[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                new[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                new[] {3, 4, 5, 2, 8, 6, 1, 7, 9},
            };

            Assert.True(ValidateSolution(board1));

            var board2 = new[]
            {
                new[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                new[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                new[] {1, 9, 8, 3, 0, 2, 5, 6, 7},
                new[] {8, 5, 0, 7, 6, 1, 4, 2, 3},
                new[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                new[] {7, 0, 3, 9, 2, 4, 8, 5, 6},
                new[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                new[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                new[] {3, 0, 0, 2, 8, 6, 1, 7, 9},
            };

            Assert.False(ValidateSolution(board2));
        }
    }
}
