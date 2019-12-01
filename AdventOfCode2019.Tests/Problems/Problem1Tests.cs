namespace AdventOfCode2019.Tests.Problems
{
    using AdventOfCode2019.Problems;
    using NUnit.Framework;

	[TestFixture]
	public class Problem1Tests
	{
        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 654)]
        [TestCase(100756, 33583)]
        public void PartOne(int mass, int expectedResult)
        {
            Assert.AreEqual(expectedResult, Problem1.CalculateModuleFuelRequirement(mass));
        }

        [TestCase(14, 2)]
        [TestCase(1969, 966)]
        [TestCase(100756, 50346)]
        public void PartTwo(int mass, int expectedResult)
        {
            Assert.AreEqual(expectedResult, Problem1.CalculateActualFuelRequirement(mass));
        }
    }
}
