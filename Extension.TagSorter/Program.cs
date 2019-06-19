using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sandbox;
using Sandbox.HelperUtils;

namespace Extension.TagSorter
{
    class Program
    {
        private const string Start = 
@"# Sandbox
The project aims to store the solutions of Codewars tasks (Currently solved {0} Kata). The tasks are tagged with categories so I provide the same structure to my solutions below.

";

        static string TaskNameFormatter(string task)
        {
            return $"- [{task}](Sandbox/{task}.cs)";
        }

        static string CategoryFormatter(Category category, IEnumerable<string> tasks)
        {
            return $"## {category} ({tasks.Count()})";
        }

        static (int, string) TasksByCategories()
        {
            var taskInfos = typeof(BouncingBall)
                .Assembly
                .GetTypes()
                .Where(t => t.IsDefined(typeof(TagAttribute), false))
                .Select(t => new
                {
                    t.Name,
                    Tags = t.GetCustomAttribute<TagAttribute>().Category
                })
                .ToArray();

            var reportData = Enum.GetValues(typeof(Category))
                .Cast<Category>()
                .Select(category =>
                {
                    var tasks = taskInfos
                        .Where(t => t.Tags.HasFlag(category))
                        .Select(t => t.Name)
                        .Select(TaskNameFormatter)
                        .ToArray();

                    return new
                    {
                        category = CategoryFormatter(category, tasks),
                        tasks
                    };
                })
                .Where(data => data.tasks.Any())
                .OrderByDescending(data => data.tasks.Length)
                .Select(data => $"{data.category}\n\n{string.Join("\n", data.tasks)}\n");

            return (taskInfos.Length, string.Join("\n", reportData));
        }

        static void Main(string[] args)
        {
            var pathParts = AppDomain.CurrentDomain.BaseDirectory.Split('\\');
            var rootPath = string.Join("\\", pathParts.Take(pathParts.Length - 5));
            var readmeFile = rootPath + @"\README.md";
            var (totalCount, tasksByCategories) = TasksByCategories();
            File.WriteAllText(readmeFile, string.Format(Start, totalCount) + tasksByCategories);
        }
    }
}
