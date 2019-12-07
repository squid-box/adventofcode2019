namespace AdventOfCode2019.Problems
{
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    public class Problem7 : Problem
    {
        public Problem7() : base(7)
        {
        }

        internal static int FindMaxThrusterSignal(int[] program, int[] phaseSettings)
        {
            var possibleThrusts = new List<int>();

            foreach (var permutation in CollectionUtilities.GetPermutations(phaseSettings, phaseSettings.Length))
            {
                var computer = new IntCodeComputer(program);
                var currentSignal = 0;

                foreach (var phaseSetting in permutation)
                {
                    computer.RunProgram(new [] { phaseSetting, currentSignal });
                    currentSignal = computer.Output;
                    computer.ResetProgram();
                }

                possibleThrusts.Add(currentSignal);
            }

            return possibleThrusts.Max();
        }

        public override string Answer()
        {
            var phaseSettings = new[] {3, 1, 2, 4, 0};
            return $"Part 1: {FindMaxThrusterSignal(Input[0].ConvertSeparatedListToInts(','), phaseSettings)}";
        }

        
    }
}