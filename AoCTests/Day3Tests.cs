using Advent_of_Code_2022.Day3;

namespace AoCTests;

[TestClass]
public class Day3Tests
{
    [TestMethod]
    public void TestRucksackCreation()
    {
        var r = new Rucksack("aaBB");
        Assert.AreEqual("aa", r.FirstCompartment);
        Assert.AreEqual("BB", r.SecondCompartment);
    }

    [TestMethod]
    public void TestRucksackWronglyPlaced()
    {
        var r = new Rucksack("abcABc");
        Assert.AreEqual('c', r.WronglyPlacedItem);
    }

    [DataRow("vJrwpWtwJgWrhcsFMMfFFhFp", 'p')]
    [DataRow("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", 'L')]
    [DataRow("PmmdzqPrVvPwwTWBwg", 'P')]
    [DataRow("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", 'v')]
    [DataRow("ttgJtRGJQctTZtZT", 't')]
    [DataRow("CrZsJsPPZsGzwwsLwLmpwMDw", 's')]
    [DataTestMethod]
    public void TestRucksackWronglyPlacedFromSample(string rucksackContents, char wronglyPlaced)
    {
        var r = new Rucksack(rucksackContents);
        Assert.AreEqual(wronglyPlaced, r.WronglyPlacedItem);
    }

    [TestMethod]
    public void TestItemPriority()
    {
        var prioritya = ItemUtil.ItemPriority('a');
        Assert.AreEqual(1, prioritya);

        var priorityA = ItemUtil.ItemPriority('A');
        Assert.AreEqual(27, priorityA);
    }
}
