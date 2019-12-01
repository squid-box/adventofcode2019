namespace AdventOfCode2019.Problems
{
    using System;
    using Utils;

    public class Problem1 : Problem
	{
		public Problem1() : base(1)
		{
		}

		public override string Answer()
        {
            var input = Input.ConvertToInt();

            var naiveSum = 0;
            var actualSum = 0;

            foreach (var module in input)
            {
                naiveSum += CalculateModuleFuelRequirement(module);
                actualSum += CalculateActualFuelRequirement(module);
            }

            return $"Total fuel required is {naiveSum} units, actual fuel required is {actualSum} units.";
        }

        internal static int CalculateModuleFuelRequirement(int moduleMass)
        {
            return (int)Math.Floor(moduleMass / 3.0) - 2;
        }

        internal static int CalculateActualFuelRequirement(int moduleMass)
        {
            var total = 0;

            while (moduleMass > 0)
            {
                var requiredFuel = Math.Max(CalculateModuleFuelRequirement(moduleMass), 0);
                total += requiredFuel;
                moduleMass = requiredFuel;
            }

            return total;
        }
	}
}
