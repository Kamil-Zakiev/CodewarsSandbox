using System;
using System.Linq;
using System.Reflection;
using Sandbox;
using Sandbox.HelperUtils;

namespace Extension.TagSorter
{
    class Program
    {
        static string TaskNameFormatter(string task)
        {
            return $"- [{task}](Sandbox/{task}.cs)";
        }

        static string CategoryFormatter(Category category)
        {
            return $"## {category}";
        }

        static void Main(string[] args)
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
                });

            foreach (var data in reportData)
            {
                if (data.tasks.Length == 0)
                {
                    continue;
                }

                const string bars = "";
                Console.WriteLine($"{data.category}\n\n{data.tasks}\n{bars}");
            }
        }
    }
}
