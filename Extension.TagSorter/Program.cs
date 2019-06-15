using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Sandbox;
using Sandbox.HelperUtils;

namespace Extension.TagSorter
{
    class Program
    {
        private const string Start = @"# Sandbox
The project aims to store the solutions of Codewars tasks. The tasks are tagged with categories so I provide the same structure to my solutions below.

";

        static string TaskNameFormatter(string task)
        {
            return $"- [{task}](Sandbox/{task}.cs)";
        }

        static string CategoryFormatter(Category category)
        {
            return $"## {category}";
        }

        static string TasksByCategories()
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
                        .Select(TaskNameFormatter);

                    return new
                    {
                        category = CategoryFormatter(category),
                        tasks = string.Join("\n", tasks)
                    };
                })
                .Where(data => data.tasks.Length != 0)
                .Select(data => $"{data.category}\n\n{data.tasks}\n");

            return string.Join("\n", reportData);
        }

        static void Main(string[] args)
        {
            const string readmeFile = @"G:\Sandbox\README.md";
            var tasksByCategories = TasksByCategories();
            File.WriteAllText(readmeFile, Start + tasksByCategories);
        }
    }
}
