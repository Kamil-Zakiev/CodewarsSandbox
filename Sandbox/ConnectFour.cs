using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/connect-four-1/train/csharp
    /// </summary>
    [Tag(Category.Fundamentals)]
    public class ConnectFour
    {
        private enum Disk
        {
            Yellow = 1,
            Red = 2
        }

        public static string WhoIsWinner(List<string> piecesPositionList)
        {
            var board = new Disk?[7, 6];

            Disk? ChooseWinner((int, int) place)
            {
                var (x, y) = place;
                var disk = board[x, y].Value;

                bool IsOnBoardAndTheSame(int x1, int y1)
                {
                    if (x1 < 0 || x1 > 6 || y1 < 0 || y1 > 5)
                    {
                        return false;
                    }

                    var cell = board[x1, y1];
                    return cell.HasValue && cell.Value == disk;
                }

                bool ContainsWinDirection(IEnumerable<(int x, int y)> offsets)
                {
                    var sequenceLength = 0;
                    foreach (var offset in offsets)
                    {
                        if (IsOnBoardAndTheSame(x + offset.x, y + offset.y))
                        {
                            if (++sequenceLength == 4)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            sequenceLength = 0;
                        }
                    }

                    return false;
                }

                var ascPattern = new[] {-3, -2, -1, 0, 1, 2, 3};
                var descPattern = ascPattern.Reverse();
                var noOffset = new int[ascPattern.Length];

                Func<int, int, (int, int)> composer = (a, b) => (a, b);
                var hasWon = new[]
                    {
                        ascPattern.Zip(ascPattern, composer),
                        ascPattern.Zip(noOffset, composer),
                        noOffset.Zip(ascPattern, composer),
                        ascPattern.Zip(descPattern, composer)
                    }
                    .Any(ContainsWinDirection);

                if (hasWon)
                {
                    return disk;
                }

                return null;
            }

            int GetColumn(string column)
            {
                switch (column)
                {
                    case "A": return 0;
                    case "B": return 1;
                    case "C": return 2;
                    case "D": return 3;
                    case "E": return 4;
                    case "F": return 5;
                    case "G": return 6;
                    default: throw new IndexOutOfRangeException();
                }
            }

            (int column, Disk disk) GetStepDescription(string step)
            {
                var description = step.Split('_');
                return (GetColumn(description[0]), description[1] == "Yellow" ? Disk.Yellow : Disk.Red);
            }

            (int x, int y) MakeStep(int column, Disk disk)
            {
                for (int i = 0; i <= 5; i++)
                {
                    if (!board[column, i].HasValue)
                    {
                        board[column, i] = disk;
                        return (column, i);
                    }
                }

                throw new InvalidOperationException();
            }

            foreach (var step in piecesPositionList)
            {
                var (column, player) = GetStepDescription(step);
                var place = MakeStep(column, player);
                var winner = ChooseWinner(place);

                if (winner.HasValue)
                {
                    return winner.ToString();
                }
            }

            return "Draw";
        }

        [Fact]
        public void FirstTest()
        {
            List<string> myList = new List<string>()
            {
                "A_Red",
                "B_Yellow",
                "A_Red",
                "B_Yellow",
                "A_Red",
                "B_Yellow",
                "G_Red",
                "B_Yellow"
            };
            Assert.Equal("Yellow", WhoIsWinner(myList));
        }

        [Fact]
        public void SecondTest()
        {
            List<string> myList = new List<string>()
            {
                "C_Yellow",
                "E_Red",
                "G_Yellow",
                "B_Red",
                "D_Yellow",
                "B_Red",
                "B_Yellow",
                "G_Red",
                "C_Yellow",
                "C_Red",
                "D_Yellow",
                "F_Red",
                "E_Yellow",
                "A_Red",
                "A_Yellow",
                "G_Red",
                "A_Yellow",
                "F_Red",
                "F_Yellow",
                "D_Red",
                "B_Yellow",
                "E_Red",
                "D_Yellow",
                "A_Red",
                "G_Yellow",
                "D_Red",
                "D_Yellow",
                "C_Red"
            };
            Assert.Equal("Yellow", WhoIsWinner(myList));
        }

        [Fact]
        public void ThirdTest()
        {
            List<string> myList = new List<string>()
            {
                "A_Yellow",
                "B_Red",
                "B_Yellow",
                "C_Red",
                "G_Yellow",
                "C_Red",
                "C_Yellow",
                "D_Red",
                "G_Yellow",
                "D_Red",
                "G_Yellow",
                "D_Red",
                "F_Yellow",
                "E_Red",
                "D_Yellow"
            };

            Assert.Equal("Red", WhoIsWinner(myList));
        }
    }
}
