namespace AdventOfCode2019.Tests.Utils
{
    using System.Linq;
    using AdventOfCode2019.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class CollectionUtilitiesTests
    {
        [Test]
        public void PermutationsTest()
        {
            var input = new[] {1, 2, 3};
            var permutations = CollectionUtilities.GetPermutations(input, input.Length).ToList();

            Assert.AreEqual(6, permutations.Count);
            Assert.Contains(new [] {1, 3, 2}, permutations);
        }
    }
}
