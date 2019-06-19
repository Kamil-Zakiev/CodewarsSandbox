using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/56293ae77e20756fc500002e
    /// </summary>
    [Tag(Category.Algorithms | Category.Arrays | Category.DataStructures | Category.Graphs)]
    public class ExpandingDependencyChains
    {
        public static Dictionary<string, string[]> ExpandDependencies_WithoutRecursion(Dictionary<string, string[]> dependencies) 
        {
            var unwrappedDeps = new Dictionary<string, string[]>();
            foreach (var (file, deps) in dependencies)
            {
                if (deps.Length == 0)
                {
                    unwrappedDeps.Add(file, new string[0]);
                    continue;
                }

                if (deps.Contains(file))
                {
                    throw new InvalidOperationException();
                }

                var allDeps = deps.ToList();
                var handlingDepIndex = 0;
                while (handlingDepIndex < allDeps.Count)
                {
                    var handlingDep = allDeps[handlingDepIndex++];

                    if (unwrappedDeps.TryGetValue(handlingDep, out var innerDeps))
                    {
                        allDeps.AddRange(innerDeps.Where(innerDep => !allDeps.Contains(innerDep)));
                        continue;
                    }
                    
                    foreach (var innerDep in dependencies[handlingDep])
                    {
                        if (allDeps.Contains(innerDep))
                        {
                            continue;
                        }
                    
                        if (innerDep.Equals(file))
                        {
                            throw new InvalidOperationException();
                        }
                        
                        allDeps.Add(innerDep);
                    }
                    
                }

                unwrappedDeps.Add(file, allDeps.ToArray());
            }

            return unwrappedDeps;
        }
        
        public static Dictionary<string, string[]> ExpandDependencies_Recursion(Dictionary<string, string[]> dependencies) 
        {
            var unwrappedDeps = new Dictionary<string, string[]>();
            foreach (var (file, deps) in dependencies)
            {
                var allDeps = new List<string>();

                void AddDep(string dep)
                {
                    if (!allDeps.Contains(dep))
                    {
                        if (dep.Equals(file))
                        {
                            throw new InvalidOperationException();
                        }
                        
                        allDeps.Add(dep);
                        foreach (var innerDep in dependencies[dep])
                        {
                            AddDep(innerDep);
                        }
                    }
                }

                foreach (var dep in deps)
                {
                    AddDep(dep);
                }
                
                unwrappedDeps.Add(file, allDeps.ToArray());
            }

            return unwrappedDeps;
        }
        
        [Fact]
        public void ExampleFromDescription()
        {
            // Arrange
            var startFiles = new Dictionary<string, string[]>();
            startFiles["A"] = new[] {"B", "D"};
            startFiles["B"] = new[] {"C"};
            startFiles["C"] = new[] {"D"};
            startFiles["D"] = new string[] {};
    
            var correctFiles = new Dictionary<string, string[]>();
            correctFiles["A"] = new[] {"B", "C", "D"};
            correctFiles["B"] = new[] {"C", "D"};
            correctFiles["C"] = new[] {"D"};
            correctFiles["D"] = new string[] {};
    
            // Act
            var actualFiles = ExpandDependencies_WithoutRecursion(startFiles);
    
            // Assert
            
            Assert.Equal(actualFiles.Count, correctFiles.Count);
            Assert.Equal(actualFiles["A"].OrderBy(x => x), correctFiles["A"].OrderBy(x => x));
            Assert.Equal(actualFiles["B"].OrderBy(x => x), correctFiles["B"].OrderBy(x => x));
            Assert.Equal(actualFiles["C"].OrderBy(x => x), correctFiles["C"].OrderBy(x => x));
            Assert.Equal(actualFiles["D"].OrderBy(x => x), correctFiles["D"].OrderBy(x => x));
        }
  
        [Fact]
        public void TestEmptyFileList() {
            // Arrange
            var startFiles = new Dictionary<string, string[]>();
            var correctFiles = new Dictionary<string, string[]>();
    
            // Act
            var actualFiles = ExpandDependencies_WithoutRecursion(startFiles);
    
            // Assert
            Assert.Equal(actualFiles, correctFiles);
        }
  
        [Fact]
        public void TestCircularDependencies() {
            // Arrange
            var startFiles = new Dictionary<string, string[]>();
            startFiles["A"] = new[] {"B"};
            startFiles["B"] = new[] {"C"};
            startFiles["C"] = new[] {"D"};
            startFiles["D"] = new[] {"A"};
    
            // Act/Assert
            Assert.Throws<InvalidOperationException>(delegate { ExpandDependencies_WithoutRecursion(startFiles); });
        }
    }
}