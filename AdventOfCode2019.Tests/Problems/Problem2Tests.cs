namespace AdventOfCode2019.Tests.Problems
{
    using AdventOfCode2019.Problems;
    using NUnit.Framework;

    [TestFixture]
    public class Problem2Tests
    {
        [TestCase("1,0,0,0,99", 2)]
        [TestCase("2,3,0,3,99", 6)]
        [TestCase("2,4,4,5,99,0", 9801)]
        [TestCase("1,1,1,4,99,5,6,0,99", 30)]
        public void PartOne(string input, int expectedResult)
        {
            //Assert.AreEqual(expectedResult, Problem2.SomeFunction(input));
        }

        [Test]
        public void ParseInputTest()
        {
            Assert.AreEqual(new []{1,20,-33333}, Problem2.ParseInput("1,20,-33333"));
        }

        [Test]
        public void ExecuteStepTest()
        {
            var program = Problem2.ParseInput("1,9,10,3,2,3,11,0,99,30,40,50");

            Assert.IsTrue(Problem2.ExecuteStep(ref program, 0));
            Assert.AreEqual(70, program[3]);

            Assert.IsTrue(Problem2.ExecuteStep(ref program, 4));
            Assert.AreEqual(3500, program[0]);

            Assert.IsFalse(Problem2.ExecuteStep(ref program, 8));
        }

        [Test]
        public void ExecuteProgramTest()
        {
            var program = Problem2.ParseInput("1,9,10,3,2,3,11,0,99,30,40,50");

            Problem2.RunProgram(program, 9, 10);

            Assert.AreEqual(new []{3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50}, program);
        }
    }
}