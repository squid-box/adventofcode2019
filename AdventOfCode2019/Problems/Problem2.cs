namespace AdventOfCode2019.Problems
{
    using Utils;

    internal class Problem2 : Problem
    {
        private const int OpCodeAdd = 1;
        private const int OpCodeMultiply = 2;
        private const int OpCodeHalt = 99;

        public Problem2() : base(2)
        {
        }

        internal static int[] ParseInput(string input)
        {
            return input.Split(',').ConvertToInt();
        }

        internal static bool ExecuteStep(ref int[] input, int position)
        {
            var opCode = input[position];

            switch (opCode)
            {
                case OpCodeAdd:
                    input[input[position + 3]] = input[input[position+1]] + input[input[position + 2]];
                    break;
                case OpCodeMultiply:
                    input[input[position + 3]] = input[input[position + 1]] * input[input[position + 2]];
                    break;
                case OpCodeHalt:
                    return false;
            }

            return true;
        }

        internal static int[] RunProgram(int[] input, int firstReplace, int secondReplace)
        {
            var position = 0;

            input[1] = firstReplace;
            input[2] = secondReplace;

            while (true)
            {
                if (!ExecuteStep(ref input, position))
                {
                    break;
                }

                position += 4;
            }

            return input;
        }

        internal static int FindNounAndVerb(int[] input, int target)
        {
            for (var verb = 0; verb < 100; verb++)
            {
                for (var noun = 0; noun < 100; noun++)
                {
                    var copy = (int[])input.Clone();
                    copy = RunProgram(copy, noun, verb);

                    if (copy[0] == target)
                    {
                        return (100 * noun) + verb;
                    }
                }
            }

            return -1;
        }

        public override string Answer()
        {
            var program = ParseInput(Input[0]);

            RunProgram(program, 12, 2);
            var nounVerbProduct = FindNounAndVerb(ParseInput(Input[0]), 19690720);

            return $"Program execution leaves {program[0]} at position 0, and \"100 * noun + verb\" is {nounVerbProduct}.";
        }
    }
}