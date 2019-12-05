namespace AdventOfCode2019.Problems
{
    using Utils;

    internal class Problem2 : Problem
    {
        public Problem2() : base(2)
        {
        }

        internal static int FindNounAndVerb(int[] input, int target)
        {
            var computer = new IntCodeComputer(input);

            for (var verb = 0; verb < 100; verb++)
            {
                for (var noun = 0; noun < 100; noun++)
                {
                    computer.SetNounAndVerb(noun, verb);
                    computer.RunProgram();

                    if (computer.ZeroRegister == target)
                    {
                        return (100 * noun) + verb;
                    }
                    
                    computer.ResetProgram();
                }
            }

            return -1;
        }

        public override string Answer()
        {
            var program = Input[0].ConvertSeparatedListToInts(',');

            var computer = new IntCodeComputer(program);
            computer.SetNounAndVerb(12, 2);
            computer.RunProgram();
            var partOne = computer.ZeroRegister;

            var partTwo = FindNounAndVerb(program, 19690720);

            return $"Part 1: {partOne}\nPart 2: {partTwo}";
        }
    }
}