namespace Advent_of_Code_2022.Day3
{
    public class Day3Runner
    {
        public async Task Run()
        {
            List<Rucksack> rucksacks;
            using (var file = File.OpenText("./res/Day3.txt"))
            {
                rucksacks = await ReadInRucksacks(file);
            }

            var totalPriority = rucksacks.Sum(r => ItemUtil.ItemPriority(r.WronglyPlacedItem));
            Console.WriteLine("Total priority is " + totalPriority);
        }

        private async Task<List<Rucksack>> ReadInRucksacks(StreamReader file)
        {
            List<Rucksack> rucksacks = new();
            while (await file.ReadLineAsync() is string line)
            {
                rucksacks.Add(new Rucksack(line));
            }

            return rucksacks;
        }
    }
}

