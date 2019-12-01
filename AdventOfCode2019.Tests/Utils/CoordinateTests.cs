namespace AdventOfCode2019.Tests.Utils
{
	using AdventOfCode2019.Utils;
	using NUnit.Framework;

    [TestFixture]
    public class CoordinateTests
    {
        private Coordinate _coord1;
        private Coordinate _coord2;
        private Coordinate _coord3;
        private Coordinate _coord4;

        [SetUp]
        public void SetUp()
        {
            _coord1 = new Coordinate(0, 0);
            _coord2 = new Coordinate(0, 0);
            _coord3 = new Coordinate(2, 0);
            _coord4 = new Coordinate(10, 10);
        }

        [Test]
        public void ManhattanDistanceTest()
        {
            Assert.AreEqual(0, Coordinate.ManhattanDistance(_coord1, _coord2));
            Assert.AreEqual(Coordinate.ManhattanDistance(_coord1, _coord2), Coordinate.ManhattanDistance(_coord2, _coord1));
            Assert.AreEqual(2, Coordinate.ManhattanDistance(_coord1, _coord3));
            Assert.AreEqual(20, Coordinate.ManhattanDistance(_coord1, _coord4));
        }

        [Test]
        public void EqualityTest()
        {
            Assert.IsTrue(_coord1.Equals(_coord2));
            Assert.IsFalse(_coord1.Equals(_coord3));
        }
    }
}
