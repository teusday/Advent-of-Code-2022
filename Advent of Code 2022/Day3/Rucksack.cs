using System;
namespace Advent_of_Code_2022.Day3
{
    public class Rucksack
    {
        public Rucksack(string contents)
        {
            if (int.IsOddInteger(contents.Length))
                throw new AoCException($"Rucksack contents {contents} cannot be halved");
            var half = contents.Length / 2;
            FirstCompartment = contents[..half];
            SecondCompartment = contents[half..];
        }
        public string FirstCompartment { get; set; }
        public string SecondCompartment { get; set; }

        public char WronglyPlacedItem
        {
            get
            {
                var firstItems = FirstCompartment.ToCharArray().Distinct();
                var secondItems = SecondCompartment.ToCharArray().Distinct();

                var intersection = firstItems.Intersect(secondItems);
                if (intersection.Count() == 1)
                    return intersection.First();
                else
                    throw new AoCException("Multiple Wrongly Placed Items");
            }
        }
    }
}

