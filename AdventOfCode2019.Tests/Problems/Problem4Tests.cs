namespace AdventOfCode2019.Tests.Problems
{
    using System;
    using AdventOfCode2019.Problems;
    using NUnit.Framework;

    [TestFixture]
    public class Problem4Tests
    {
        [TestCase(111111, true)]
        [TestCase(223450, false)]
        [TestCase(123789, false)]
        public void IsValidPasswordTest(int password, bool isValid)
        {
            Assert.AreEqual(isValid, Problem4.IsValidPassword(password));
        }

        [TestCase(112233, true)]
        [TestCase(123444, false)]
        [TestCase(111122, true)]
        [TestCase(111111, true)]
        [TestCase(111333, false)]
        [TestCase(654321, false)]
        public void IsValidPasswordPart2(int password, bool isValid)
        {
            Assert.AreEqual(isValid, Problem4.IsValidPasswordPart2(password));
        }

        [TestCase("123456-987654", 123456, 987654)]
        public void ParseInputTest(string input, int expectedLowerLimit, int expectedUpperLimit)
        {
            var (lowerLimit, upperLimit) = Problem4.ParseInput(input);
            Assert.AreEqual(expectedLowerLimit, lowerLimit);
            Assert.AreEqual(expectedUpperLimit, upperLimit);
        }
    }
}