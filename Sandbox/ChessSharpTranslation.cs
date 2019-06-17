using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/52fcc820f7214727bc0004b7
    /// </summary>
    [Tag(Category.Algorithms | Category.Arrays | Category.DataStructures | Category.Validation | Category.Search)]
    public  class ChessSharpTranslation
    {
        public static List<Figure> isCheck(IList<Figure> figures, int player)
        {
            var actingKing = figures.SingleOrDefault(x => x.Owner == player && x.Type == FigureType.King);
            return figures
                .Where(f => f.IsEnemyFor(actingKing))
                .Where(f => f.CanHit(actingKing.Cell, figures))
                .ToList();
        }

        public static bool isMate(IList<Figure> figures, int player)
        {
            var threatenEnemies = isCheck(figures, player);
            if (!threatenEnemies.Any())
            {
                return false;
            }

            var king = figures.SingleOrDefault(x => x.Owner == player && x.Type == FigureType.King);
            var allies = figures.Where(f => f.IsAllyOf(king));
            if (threatenEnemies.Any(e => king.CanCaptureForFree(e, figures) 
                                         || allies.Any(a => a.CanHit(e.Cell, figures) && a.NotExposeTheKing(e.Cell, figures))
                                         || e.CanBeBlocked(king, figures)))
            {
                return false;
            }
            
            var kingCell = king.Cell;
            var goPositions = new[]
                {
                    new Pos(kingCell.Y + 1, kingCell.X + 1),
                    new Pos(kingCell.Y + 1, kingCell.X),
                    new Pos(kingCell.Y + 1, kingCell.X - 1),
                    new Pos(kingCell.Y - 1, kingCell.X + 1),
                    new Pos(kingCell.Y - 1, kingCell.X),
                    new Pos(kingCell.Y - 1, kingCell.X - 1),
                    new Pos(kingCell.Y, kingCell.X + 1),
                    new Pos(kingCell.Y, kingCell.X - 1)
                }
                .Where(PosExtensions.IsOnBoard)
                .Where(pos => !figures.Any(f => f.Cell.Equals(pos)))
                .ToArray();

            var escapeCells = goPositions
                .Where(pos => !figures.Any(f => f.IsEnemyFor(king) && f.CanHit(pos, figures)))
                .ToArray();

            return escapeCells.Length == 0;
        }

        [Fact]
        public void MyTest()
        {
            var pawnThreatensKing = isCheck(SampleTestCases.pawnThreatensKing, 0).Single();
            Assert.Equal(pawnThreatensKing, SampleTestCases.pawnThreatensKing[2]);

            Assert.Equal(isCheck(SampleTestCases.rookThreatensKing, 0),
                new[] {SampleTestCases.rookThreatensKing[2]});

            Assert.Equal(isCheck(SampleTestCases.knightThreatensKing, 0),
                new[] {SampleTestCases.knightThreatensKing[2]});

            Assert.Equal(isCheck(SampleTestCases.bishopThreatensKing, 0),
                new[] {SampleTestCases.bishopThreatensKing[2]});

            Assert.Equal(isCheck(SampleTestCases.queenThreatensKing1, 0),
                new[] {SampleTestCases.queenThreatensKing1[2]});

            Assert.Equal(isCheck(SampleTestCases.queenThreatensKing2, 0),
                new[] {SampleTestCases.queenThreatensKing2[2]});

            Assert.Equal(isCheck(SampleTestCases.doubleThreat, 0),
                new[] {SampleTestCases.doubleThreat[5], SampleTestCases.doubleThreat[6]});
        }

        [Fact]
        public void Test08()
        {
            var test08Board = new[]
            {
                new Figure(FigureType.King, 1, new Pos(0, 4)),
                new Figure(FigureType.Queen, 1, new Pos(4, 7)),

                new Figure(FigureType.King, 0, new Pos(7, 4)),
                new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
                new Figure(FigureType.Knight, 0, new Pos(7, 6)),
                new Figure(FigureType.Rook, 0, new Pos(7, 7)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 3)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
                new Figure(FigureType.Pawn, 0, new Pos(5, 5)),
                new Figure(FigureType.Pawn, 0, new Pos(4, 6)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 7)),
            };

            Assert.Equal(isCheck(test08Board, 0).Single(), test08Board[1]);
            Assert.False(isMate(test08Board, 0));
        }

        [Fact]
        public void Test09()
        {
            var test09Board = new[]
            {
                new Figure(FigureType.King, 1, new Pos(0, 4)),
                new Figure(FigureType.Queen, 1, new Pos(4, 7)),

                new Figure(FigureType.King, 0, new Pos(7, 4)),
                new Figure(FigureType.Queen, 0, new Pos(7, 3)),
                new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
                new Figure(FigureType.Knight, 0, new Pos(7, 6)),
                new Figure(FigureType.Rook, 0, new Pos(7, 7)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 3)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
                new Figure(FigureType.Pawn, 0, new Pos(5, 5)),
                new Figure(FigureType.Pawn, 0, new Pos(4, 6)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 7)),
            };

            Assert.True(isMate(test09Board, 0));
        }
        
        [Fact]
        public void Test19()
        {
            var test19Board = new[]
            {
                new Figure(FigureType.King, 1, new Pos(0, 4)),
                new Figure(FigureType.Rook, 1, new Pos(7, 3)),

                new Figure(FigureType.King, 0, new Pos(7, 4)),
                new Figure(FigureType.Rook, 0, new Pos(7, 5)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            };

            Assert.False(isMate(test19Board, 0));
        }
        
        [Fact]
        public void Test20()
        {
            var test20Board = new[]
            {
                new Figure(FigureType.King, 1, new Pos(0, 4)),
                new Figure(FigureType.Queen, 1, new Pos(7, 3)),
                new Figure(FigureType.Rook, 1, new Pos(6, 3)),

                new Figure(FigureType.King, 0, new Pos(7, 4)),
                new Figure(FigureType.Rook, 0, new Pos(7, 5)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            };

            Assert.True(isMate(test20Board, 0));
        }

        [Fact]
        public void Test22()
        {
            var test22Board = new[]
            {
                new Figure(FigureType.King, 1, new Pos(3, 5)),
                new Figure(FigureType.Rook, 1, new Pos(2, 5)),
                new Figure(FigureType.Bishop, 1, new Pos(2, 4)),
                new Figure(FigureType.Pawn, 1, new Pos(3, 4)),
                new Figure(FigureType.Knight, 1, new Pos(3, 3)),
                new Figure(FigureType.Pawn, 1, new Pos(4, 3)),

                new Figure(FigureType.King, 0, new Pos(7, 4)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
                new Figure(FigureType.Queen, 0, new Pos(5, 6)),
                new Figure(FigureType.Knight, 0, new Pos(5, 2)),
                new Figure(FigureType.Pawn, 0, new Pos(4, 4), new Pos(6, 4)),
            };

            Assert.False(isMate(test22Board, 1));
        }

        [Fact]
        public void Test08_checkD1()
        {
            var test08Board = new[]
            {
                new Figure(FigureType.King, 1, new Pos(0, 4)),
                new Figure(FigureType.Queen, 1, new Pos(3, 7)),

                new Figure(FigureType.King, 0, new Pos(7, 4)),
                new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
                new Figure(FigureType.Knight, 0, new Pos(7, 6)),
                new Figure(FigureType.Rook, 0, new Pos(7, 7)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 3)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
                new Figure(FigureType.Pawn, 0, new Pos(5, 5)),
                new Figure(FigureType.Pawn, 0, new Pos(4, 6)),
                new Figure(FigureType.Pawn, 0, new Pos(6, 7)),
            };

            Assert.Empty(isCheck(test08Board, 0));
        }
    }

    public class SampleTestCases
    {
        public static readonly Figure[] pawnThreatensKing = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Pawn, 1, new Pos(6, 5)),
        };

        public static readonly Figure[] rookThreatensKing = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Rook, 1, new Pos(1, 4)),
        };

        public static readonly Figure[] knightThreatensKing = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Knight, 1, new Pos(6, 2)),
        };

        public static readonly Figure[] bishopThreatensKing = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Bishop, 1, new Pos(3, 0)),
        };

        public static readonly Figure[] queenThreatensKing1 = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Queen, 1, new Pos(1, 4)),
        };

        public static readonly Figure[] queenThreatensKing2 = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Queen, 1, new Pos(4, 7)),
        };

        public static readonly Figure[] doubleThreat = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
            new Figure(FigureType.Bishop, 1, new Pos(4, 1)),
            new Figure(FigureType.Rook, 1, new Pos(7, 2), new Pos(5, 2)),
        };
    }

    public enum FigureType
    {
        Pawn,
        King,
        Queen,
        Rook,
        Knight,
        Bishop
    }

    //struct to make it convenient to work with cells
    [DebuggerDisplay("({Y}, {X})")]
    public struct Pos
    {
        public readonly sbyte X;
        public readonly sbyte Y;

        public Pos(sbyte y, sbyte x)
        {
            Y = y;
            X = x;
        }

        public Pos(int y, int x)
        {
            Y = (sbyte)y;
            X = (sbyte)x;
        }

        public override bool Equals(object obj)
        {
            return obj is Pos pos && pos.Y == Y && pos.X == X;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }

    public class Figure
    {
        public FigureType Type { get; }
        public byte Owner { get; }
        public Pos Cell { get; set; }
        public Pos? PrevCell { get; }

        public Figure(FigureType type, byte owner, Pos cell, Pos? prevCell = null)
        {
            Type = type;
            Owner = owner;
            Cell = cell;
            PrevCell = prevCell;
        }
    }

    public static class FigureExtensions
    {
        public static bool CanHit(this Figure figure, Pos cell, IList<Figure> board)
        {
            if (figure.Type == FigureType.Pawn)
            {
                var enemyInCell = board.SingleOrDefault(f => f.Cell.Equals(cell));
                if (enemyInCell != null
                    && enemyInCell.Type == FigureType.Pawn
                    && enemyInCell.PrevCell != null
                    && enemyInCell.PrevCell.Value.Y - cell.Y == 2
                    && Math.Abs(cell.X - figure.Cell.X) == 1)
                {
                    return true;
                }

                if (figure.Owner == 1) // if the figure is black
                {
                    return figure.Cell.Equals(new Pos(cell.Y - 1, cell.X - 1))
                           || figure.Cell.Equals(new Pos(cell.Y - 1, cell.X + 1));
                }
                else // if the figure is white
                {
                    return figure.Cell.Equals(new Pos(cell.Y + 1, cell.X - 1))
                           || figure.Cell.Equals(new Pos(cell.Y + 1, cell.X + 1));
                }
            }

            if (figure.Type == FigureType.King)
            {
                var hitPositions = new[]
                {
                    new Pos(cell.Y + 1, cell.X + 1),
                    new Pos(cell.Y + 1, cell.X),
                    new Pos(cell.Y + 1, cell.X - 1),
                    new Pos(cell.Y - 1, cell.X + 1),
                    new Pos(cell.Y - 1, cell.X),
                    new Pos(cell.Y - 1, cell.X - 1),
                    new Pos(cell.Y, cell.X + 1),
                    new Pos(cell.Y, cell.X - 1)
                };

                return hitPositions.Any(x => figure.Cell.Equals(x));
            }

            if (figure.Type == FigureType.Knight)
            {
                var hitPositions = new[]
                {
                    new Pos(cell.Y - 2, cell.X + 1),
                    new Pos(cell.Y - 2, cell.X - 1),
                    new Pos(cell.Y + 2, cell.X + 1),
                    new Pos(cell.Y + 2, cell.X - 1),
                    new Pos(cell.Y + 1, cell.X - 2),
                    new Pos(cell.Y - 1, cell.X - 2),
                    new Pos(cell.Y + 1, cell.X + 2),
                    new Pos(cell.Y - 1, cell.X + 2),
                };

                return hitPositions.Any(x => figure.Cell.Equals(x));
            }

            var occupied = board.Select(x => x.Cell).ToArray();
            if (figure.Type == FigureType.Bishop)
            {
                if (figure.Cell.Y == figure.Cell.X - (cell.X - cell.Y)
                    || figure.Cell.Y + figure.Cell.X == cell.X + cell.Y)
                {
                    var moveX = Math.Sign(cell.X - figure.Cell.X);
                    var moveY = Math.Sign(cell.Y - figure.Cell.Y);

                    var nextPos = new Pos(figure.Cell.Y + moveY, figure.Cell.X + moveX);
                    while (!nextPos.Equals(cell))
                    {
                        if (occupied.Contains(nextPos))
                        {
                            return false;
                        }
                        nextPos = new Pos(nextPos.Y + moveY, nextPos.X + moveX);
                    }

                    return true;
                }

                return false;
            }

            if (figure.Type == FigureType.Rook)
            {
                if (figure.Cell.X == cell.X || figure.Cell.Y == cell.Y)
                {
                    var moveX = Math.Sign(cell.X - figure.Cell.X);
                    var moveY = Math.Sign(cell.Y - figure.Cell.Y);

                    var nextPos = new Pos(figure.Cell.Y + moveY, figure.Cell.X + moveX);
                    while (!nextPos.Equals(cell))
                    {
                        if (occupied.Contains(nextPos))
                        {
                            return false;
                        }
                        nextPos = new Pos(nextPos.Y + moveY, nextPos.X + moveX);
                    }

                    return true;
                }

                return false;
            }

            if (figure.Type == FigureType.Queen)
            {

                if (figure.Cell.X == cell.X
                    || figure.Cell.Y == cell.Y
                    || figure.Cell.Y == figure.Cell.X - (cell.X - cell.Y)
                    || figure.Cell.Y + figure.Cell.X == cell.X + cell.Y)
                {
                    var moveX = Math.Sign(cell.X - figure.Cell.X);
                    var moveY = Math.Sign(cell.Y - figure.Cell.Y);

                    var nextPos = new Pos(figure.Cell.Y + moveY, figure.Cell.X + moveX);
                    while (!nextPos.Equals(cell))
                    {
                        if (occupied.Contains(nextPos))
                        {
                            return false;
                        }
                        nextPos = new Pos(nextPos.Y + moveY, nextPos.X + moveX);
                    }

                    return true;
                }

                return false;
            }

            return true;
        }

        public static bool CanReach(this Figure figure, Pos cell, IList<Figure> board)
        {
            if (figure.Type == FigureType.Pawn)
            {
                if (cell.X != figure.Cell.X)
                {
                    return false;
                }

                if (figure.Owner == 0)
                {
                    if (figure.Cell.Y == 6)
                    {
                        return cell.Y == 5 || cell.Y == 4;
                    }
                    else
                    {
                        return cell.Y == figure.Cell.Y - 1;
                    }
                }
                else
                {
                    if (figure.Cell.Y == 1)
                    {
                        return cell.Y == 2 || cell.Y == 3;
                    }
                    else
                    {
                        return cell.Y == figure.Cell.Y + 1;
                    }
                }
            }

            return CanHit(figure, cell, board);
        }

        public static bool CanNotReach(this Figure figure, Pos cell, IList<Figure> board)
        {
            return !CanReach(figure, cell, board);
        }

        public static bool IsEnemyFor(this Figure figure, Figure other)
        {
            return figure.Owner != other.Owner;
        }

        public static bool IsAllyOf(this Figure figure, Figure other)
        {
            return figure.Owner == other.Owner && figure != other;
        }

        public static bool CanCaptureForFree(this Figure figure, Figure other, IList<Figure> figures)
        {
            if (figure.Owner == other.Owner)
            {
                throw new InvalidOperationException();
            }

            if (!figure.CanHit(other.Cell, figures))
            {
                return false;
            }

            var enemyAllies = figures.Where(f => f.IsAllyOf(other));
            return enemyAllies.All(e => !e.CanHit(other.Cell, figures));
        }

        public static bool NotExposeTheKing(this Figure figure, Pos newLocation, IList<Figure> figures)
        {
            var allyKing = figures.SingleOrDefault(x => x.Owner == figure.Owner && x.Type == FigureType.King);

            var nextBoard = figures
                .Where(f => f != figure && !f.Cell.Equals(newLocation))
                .Select(f => new Figure(f.Type, f.Owner, f.Cell, f.PrevCell))
                .ToList();
            nextBoard.Add(new Figure(figure.Type, figure.Owner, newLocation, figure.Cell));

            return nextBoard
                .Where(f => f.IsEnemyFor(allyKing))
                .All(f => f.CanNotReach(allyKing.Cell, nextBoard));
        }

        public static bool CanBeBlocked(this Figure enemy, Figure theKing, IList<Figure> figures)
        {
            var directHitters = new[] { FigureType.Knight, FigureType.Pawn };
            if (directHitters.Contains(enemy.Type))
            {
                return false;
            }

            var moveX = Math.Sign(theKing.Cell.X - enemy.Cell.X);
            var moveY = Math.Sign(theKing.Cell.Y - enemy.Cell.Y);

            var nextPos = new Pos(enemy.Cell.Y + moveY, enemy.Cell.X + moveX);
            var kingAllies = figures.Where(f => f.IsAllyOf(theKing)).ToArray();
            while (!nextPos.Equals(theKing.Cell))
            {
                if (kingAllies.Any(a => a.CanReach(nextPos, figures)))
                {
                    return true;
                }

                nextPos = new Pos(nextPos.Y + moveY, nextPos.X + moveX);
            }

            return false;

        }
    }

    public static class PosExtensions
    {
        public static bool IsOnBoard(this Pos cell)
        {
            return 0 <= cell.X && cell.X <= 7 && 0 <= cell.Y && cell.Y <= 7;
        }
    }
}
