namespace AdventOfCode2019
{
	using System;
	using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Program
	{
		public static void Main(string[] args)
		{
			Console.Out.WriteLine("+---------------------+");
			Console.Out.WriteLine("| Advent of Code 2019 |");
			Console.Out.WriteLine("+---------------------+" + Environment.NewLine);

            var problems = FindProblems();

            foreach (var problem in problems)
			{
				try
				{
					var startTime = DateTime.Now;
					var answer = problem.ToString();
					var endTime = DateTime.Now;
					Console.WriteLine(answer);
					Console.WriteLine($"Calculated in {(endTime - startTime).TotalMilliseconds}ms.");
					Console.WriteLine();
				}
				catch (NotImplementedException)
				{
				}
			}

			Console.WriteLine("Execution Done.");
			Console.In.ReadLine();
		}

        /// <summary>
        /// Dynamically find any Problem implementations in the Problems namespace.
        /// </summary>
        /// <returns>A list of available instantiated Problems.</returns>
        private static IEnumerable<Problem> FindProblems()
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.Namespace == "AdventOfCode2019.Problems")
                .Where(type => type.Name.StartsWith("Problem"));

            return types.Select(type => (Problem) Activator.CreateInstance(type)).ToList();
        }
	}
}
