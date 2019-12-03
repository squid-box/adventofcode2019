namespace AdventOfCode2019.Tests.Problems
{
    using AdventOfCode2019.Problems;
    using NUnit.Framework;

    [TestFixture]
    public class Problem3Tests
    {
        [TestCase("R8,U5,L5,D3", "U7,R6,D4,L4", 6)]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void PartOne(string firstWire, string secondWire, int expectedDistance)
        {
            var grid = new Grid();

            grid.ExecuteInstructions(firstWire.Split(','), secondWire.Split(','));

            Assert.AreEqual(expectedDistance, grid.FindClosestIntersection());
            Assert.AreEqual(expectedDistance, grid.FindClosestIntersection());
        }

        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 610)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)]
        public void PartTwo(string firstWire, string secondWire, int expectedSteps)
        {
            var grid = new Grid();

            grid.ExecuteInstructions(firstWire.Split(','), secondWire.Split(','));

            Assert.AreEqual(expectedSteps, grid.FindIntersectionWithFewestSteps());
            Assert.AreEqual(expectedSteps, grid.FindIntersectionWithFewestSteps());
        }
    }
}