namespace AdventOfCode2019.Problems
{
    using System;
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

            foreach (var permutation in CollectionUtilities.GetPermutations(phaseSettings))
            {
                var computer = new IntCodeComputer(program);
                var currentSignal = 0;

                foreach (var phaseSetting in permutation)
                {
                    computer.RunProgram(new [] { phaseSetting, currentSignal });
                    currentSignal = computer.GetOutput().LastOrDefault();
                    computer.ResetProgram();
                }

                possibleThrusts.Add(currentSignal);
            }

            return possibleThrusts.Max();
        }

        internal static int FindMaxLoopedThrusterSignal(int[] program, int[] phaseSettings)
        {
            var possibleThrusts = new List<int>();

            foreach (var permutation in CollectionUtilities.GetPermutations(phaseSettings))
            {
                var computerArray = new IntCodeComputer[phaseSettings.Length];

                for (var i = 0; i < computerArray.Length; i++)
                {
                    var computer = new IntCodeComputer(program);
                    computer.AddInput(permutation.ToArray()[i]);
                    computerArray[i] = computer;
                }

                computerArray[0].AddInput(0);

                var run = true;

                while (run)
                {
                    for (var n = 0; n < computerArray.Length; n++)
                    {
                        var computer = computerArray[n];

                        if (!computer.WaitingForInput && !computer.HasHalted)
                        {
                            computer.ContinueProgram();
                        }

                        if (computer.HasOutput)
                        {
                            computerArray[n + 1 % computerArray.Length - 1].AddInput(computer.GetOutput());
                        }

                        if (computerArray[4].HasHalted)
                        {
                            possibleThrusts.Add(computerArray[4].GetOutput().LastOrDefault());
                            run = false;
                            break;
                        }
                    }
                }
            }

            return possibleThrusts.Max();
        }

        public override string Answer()
        {
            var partOne = FindMaxThrusterSignal(Input[0].ConvertSeparatedListToInts(','), new[] {0, 1, 2, 3, 4});
            var partTwo = FindMaxLoopedThrusterSignal(Input[0].ConvertSeparatedListToInts(','), new[] { 5, 6, 7, 8, 9 });

            return $"Part 1: {partOne}\nPart 2: {partTwo}";
        }

        
    }
}