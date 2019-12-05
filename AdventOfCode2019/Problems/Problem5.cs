namespace AdventOfCode2019.Problems
{
    using Utils;

    internal class Problem5 : Problem
    {
        public Problem5() : base(5)
        {
        }

        public override string Answer()
        {
            var computer = new IntCodeComputer(Input[0].ConvertSeparatedListToInts(','));

            computer.RunProgram(1);
            var partOne = computer.Output;

            computer.ResetProgram();
            computer.RunProgram(5);
            var partTwo = computer.Output;

            return $"Part 1: {partOne}\nPart 2: {partTwo}";
        }
    }
}