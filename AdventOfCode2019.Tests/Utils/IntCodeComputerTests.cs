namespace AdventOfCode2019.Tests.Utils
{
    using AdventOfCode2019.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class IntCodeComputerTests
    {
        [Test]
        public void ExecuteProgramTest()
        {
            var computer = new IntCodeComputer(new []{ 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 });

            computer.SetNounAndVerb(9, 10);
            computer.RunProgram();

            Assert.AreEqual(3500, computer.ZeroRegister);
        }
    }
}
