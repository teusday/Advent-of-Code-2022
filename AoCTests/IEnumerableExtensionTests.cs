using System;
using Advent_of_Code_2022.CustomExtensions;

namespace AoCTests
{
    [TestClass]
    public class IEnumerableExtensionTests
    {
        [TestMethod]
        public void TestBasicRange()
        {
            var i = new List<int> { 1, 2, 3, 4, 5 };
            var res = i.ElementsIn(2..);
            CollectionAssert.AreEquivalent(new int[] { 3, 4, 5 }, res.ToArray());
        }

        [TestMethod]
        public void TestOffRight()
        {
            var i = new List<int> { 1, 2, 3, 4, 5 };
            var res = i.ElementsIn(5..);
            CollectionAssert.AreEqual(new int[] { }, res.ToArray());
        }
    }
}

