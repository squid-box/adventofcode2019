namespace AdventOfCode2019.Tests.Problems
{
    using AdventOfCode2019.Problems;
    using NUnit.Framework;

    [TestFixture]
    public class Problem6Tests
    {
        private readonly string[] _input = { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L" };

        [Test]
        public void ParseInputTests()
        {
            var parsedInput = Problem6.ParseInput(_input);

            Assert.IsFalse(parsedInput.ContainsKey("COM"));
            Assert.IsTrue(parsedInput.ContainsKey("D"));

            Assert.AreEqual("COM", parsedInput["B"].Direct);
            Assert.AreEqual("E", parsedInput["F"].Direct);
            Assert.AreEqual("E", parsedInput["J"].Direct);
        }

        [Test]
        public void CollectIndirectOrbitsTests()
        {
            var input = Problem6.ParseInput(_input);

            Assert.AreEqual(1, Problem6.CollectIndirectOrbits(input, "B").List.Count);
            Assert.AreEqual(7, Problem6.CollectIndirectOrbits(input, "L").List.Count);
        }
    }
}