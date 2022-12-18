namespace Advent_of_Code_2022.Day3
{
    public class Day3Runner
    {
        public async Task GetTotalForWrongItems()
        {
            List<Rucksack> rucksacks;
            using (var file = File.OpenText("./res/Day3.txt"))
            {
                rucksacks = await ReadInRucksacks(file);
            }

            var totalPriority = rucksacks.Sum(r => ItemUtil.ItemPriority(r.WronglyPlacedItem));
            Console.WriteLine("Total priority for wrong items is " + totalPriority);
        }

        public async Task GetTotalForGroupBadges()
        {
            List<Rucksack> rucksacks;
            using (var file = File.OpenText("./res/Day3.txt"))
            {
                rucksacks = await ReadInRucksacks(file);
            }

            var groups = rucksacks
                .Chunk(3)
                .Select(c => new ElfGroup(c));

            var totalPriority = groups.Sum(g => ItemUtil.ItemPriority(g.Badge));
            Console.WriteLine("Total priority for all badges is " + totalPriority);

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

