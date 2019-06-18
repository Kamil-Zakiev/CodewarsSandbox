using System;

namespace Sandbox.HelperUtils
{
    [Flags]
    public enum Category
    {
        Algorithms = 1,
        Fundamentals = 1 << 1,
        Logic = 1 << 2,
        Numbers = 1 << 3,
        DataTypes = 1 << 4,
        Mathematics = 1 << 5,
        Strings = 1 << 6,
        ProgrammingParadigms = 1 << 7,
        DataStructures = 1 << 8,
        Bugs = 1 << 9,
        DatesTime = 1 << 10,
        DeclarativeProgramming = 1 << 11,
        DynamicProgramming = 1 << 12,
        Integers = 1 << 13,
        Performance = 1 << 14,
        AdvancedLanguageFeatures = 1 << 15,
        Arrays = 1 << 16,
        Binary = 1 << 17,
        BinarySearchTrees = 1 << 18,
        Formats = 1 << 19,
        Formatting = 1 << 20,
        FunctionalProgramming = 1 << 21,
        Lists = 1 << 22,
        Optimization = 1 << 23,
        Puzzles = 1 << 24,
        RegularExpressions = 1 << 25,
        Sorting = 1 << 26,
        Trees = 1 << 27,
        Validation = 1 << 28,
        Search = 1 << 29,
        Graphs = 1 << 30
    }
}
