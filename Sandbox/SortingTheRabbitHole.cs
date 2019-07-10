using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    ///     https://www.codewars.com/kata/563301d00656afb8a600009d
    /// </summary>
    [Tag(Category.Algorithms | Category.Arrays | Category.Sorting)]
    public class SortingTheRabbitHole
    {
        private static readonly Dictionary<object, int> Cache = new Dictionary<object, int>();

        public static object[] DeepSort(object[] arr, bool asc = false)
        {
            Cache.Clear();
            return DeepSortInner(arr, asc);
        }

        private static object[] DeepSortInner(object[] arr, bool asc = false)
        {
            var result = (asc
                    ? arr.OrderBy(ValueOf)
                    : arr.OrderByDescending(ValueOf))
                .ToArray();

            for (var i = 0; i < result.Length; i++)
                if (result[i] is object[] nestedArray)
                    result[i] = DeepSortInner(nestedArray, asc);

            return result;
        }

        private static int ValueOf(object o)
        {
            return o is int i ? i : SumOf((object[]) o);
        }

        private static int SumOf(object[] array)
        {
            if (Cache.TryGetValue(array, out var result)) return result;

            var sum = array
                .Sum(item => item is int value
                    ? value
                    : SumOf(item as object[]));

            Cache.Add(array, sum);
            return sum;
        }

        private static void TestWithVisualization(object[] actual, object[] expected)
        {
            Assert.Equal(ArrayToString(expected), ArrayToString(actual));
        }

        private static string ArrayToString(object[] array)
        {
            var list = new List<string>();
            for (var i = 0; i < array.Length; i++)
                if (array[i].GetType().IsArray)
                    list.Add(ArrayToString((object[]) array[i]));
                else
                    list.Add(array[i].ToString());

            return "[" + string.Join(", ", list) + "]";
        }

        [Fact]
        public void BasicTests()
        {
            TestWithVisualization(DeepSort(new object[] {1, 2, 3, 4}, true), new object[] {1, 2, 3, 4});
            TestWithVisualization(DeepSort(new object[] {1, 2, 3, 4}), new object[] {4, 3, 2, 1});
            TestWithVisualization(DeepSort(new object[] {2, new object[] {1, 1}, new object[] {1, 1}, 2}, true),
                new object[] {2, new object[] {1, 1}, new object[] {1, 1}, 2});

            TestWithVisualization(DeepSort(new object[] {1, 2, 3, 4, new object[] {-5, -4}}, true),
                new object[] {new object[] {-5, -4}, 1, 2, 3, 4});

            TestWithVisualization(DeepSort(new object[] {1, 2, 3, 4, new object[] {-5, -4}}),
                new object[] {4, 3, 2, 1, new object[] {-4, -5}});
            TestWithVisualization(
                DeepSort(
                    new object[]
                    {
                        1, new object[] {2, 3, new object[] {4, 5, new object[] {9, 11}, 1, 8}, 6},
                        new object[] {20, 7, 8}
                    },
                    true),
                new object[]
                {
                    1, new object[] {7, 8, 20}, new object[] {2, 3, 6, new object[] {1, 4, 5, 8, new object[] {9, 11}}}
                });
            TestWithVisualization(
                DeepSort(new object[]
                {
                    1, new object[] {2, 3, new object[] {4, 5, new object[] {9, 11}, 1, 8}, 6}, new object[] {20, 7, 8}
                }),
                new object[]
                {
                    new object[] {new object[] {new object[] {11, 9}, 8, 5, 4, 1}, 6, 3, 2}, new object[] {20, 8, 7}, 1
                });
            TestWithVisualization(
                DeepSort(
                    new object[]
                    {
                        1, new object[] {2, 4}, 3, 8,
                        new object[]
                        {
                            6, 6,
                            new object[]
                            {
                                3, 3,
                                new object[]
                                {
                                    5,
                                    new object[] {8, 9, 0, new object[] {12, new object[] {11, 11, new object[] {1}}}}
                                }
                            }
                        },
                        -1, new object[] {80, 12}
                    }, true),
                new object[]
                {
                    -1, 1, 3, new object[] {2, 4}, 8,
                    new object[]
                    {
                        6, 6,
                        new object[]
                        {
                            3, 3,
                            new object[]
                                {5, new object[] {0, 8, 9, new object[] {12, new object[] {new object[] {1}, 11, 11}}}}
                        }
                    },
                    new object[] {12, 80}
                });
            TestWithVisualization(
                DeepSort(new object[]
                {
                    1, new object[] {2, 4}, 3, 8,
                    new object[]
                    {
                        6, 6,
                        new object[]
                        {
                            3, 3,
                            new object[]
                                {5, new object[] {8, 9, 0, new object[] {12, new object[] {11, 11, new object[] {1}}}}}
                        }
                    },
                    -1, new object[] {80, 12}
                }),
                new object[]
                {
                    new object[] {80, 12},
                    new object[]
                    {
                        new object[]
                        {
                            new object[]
                                {new object[] {new object[] {new object[] {11, 11, new object[] {1}}, 12}, 9, 8, 0}, 5},
                            3, 3
                        },
                        6, 6
                    },
                    8, new object[] {4, 2}, 3, 1, -1
                });
            TestWithVisualization(
                DeepSort(new object[] {new object[] {4, 2, 7}, new object[] {7, 2, 4}, new object[] {2, 4, 7}}, true),
                new object[] {new object[] {2, 4, 7}, new object[] {2, 4, 7}, new object[] {2, 4, 7}});
            TestWithVisualization(
                DeepSort(new object[] {new object[] {4, 2, 7}, new object[] {7, 2, 4}, new object[] {2, 4, 7}}),
                new object[] {new object[] {7, 4, 2}, new object[] {7, 4, 2}, new object[] {7, 4, 2}});
            TestWithVisualization(
                DeepSort(
                    new object[]
                    {
                        86,
                        new object[]
                        {
                            33, 8, new object[] {9, 4, 4, 3, new object[] {1, 2, 3}}, 5, 5,
                            new object[] {77, 1, -1, new object[] {-5, -6, -7}, new object[] {56, 65, 43}}
                        }
                    }, true),
                new object[]
                {
                    86,
                    new object[]
                    {
                        5, 5, 8, new object[] {3, 4, 4, new object[] {1, 2, 3}, 9}, 33,
                        new object[] {new object[] {-7, -6, -5}, -1, 1, 77, new object[] {43, 56, 65}}
                    }
                });
            TestWithVisualization(
                DeepSort(
                    new object[]
                    {
                        86,
                        new object[]
                        {
                            33, 8, new object[] {9, 4, 4, 3, new object[] {1, 2, 3}}, 5, 5,
                            new object[] {77, 1, -1, new object[] {-5, -6, -7}, new object[] {56, 65, 43}}
                        }
                    }),
                new object[]
                {
                    new object[]
                    {
                        new object[] {new object[] {65, 56, 43}, 77, 1, -1, new object[] {-5, -6, -7}}, 33,
                        new object[] {9, new object[] {3, 2, 1}, 4, 4, 3}, 8, 5, 5
                    },
                    86
                });
        }
    }
}