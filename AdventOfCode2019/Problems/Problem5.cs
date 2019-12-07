namespace AdventOfCode2019.Problems
{
    using System.Linq;
    using Utils;

    internal class Problem5 : Problem
    {
        public Problem5() : base(5)
        {
        }

        public override string Answer()
        {
            var computer = new IntCodeComputer(Input[0].ConvertSeparatedListToInts(','));

            computer.RunProgram(new []{1});
            var partOne = computer.GetOutput().LastOrDefault();

            computer.ResetProgram();
            computer.RunProgram(new[] { 5 });
            var partTwo = computer.GetOutput().LastOrDefault();

            return $"Part 1: {partOne}\nPart 2: {partTwo}";
        }
    }
}