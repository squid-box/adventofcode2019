namespace AdventOfCode2019
{
	using System;
	using System.Collections.Generic;

	using Problems;

	public class Program
	{
		public static void Main(string[] args)
		{
			Console.Out.WriteLine("+---------------------+");
			Console.Out.WriteLine("| Advent of Code 2019 |");
			Console.Out.WriteLine("+---------------------+" + Environment.NewLine);

			var problems = new List<Problem>
			{
				new Problem1()
			};

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
	}
}
