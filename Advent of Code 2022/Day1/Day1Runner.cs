namespace Advent_of_Code_2022.Day1
{
	public class Day1Runner: IAoCRunner
	{
		public Day1Runner()
		{
		}

        public async Task Run()
        {
            List<Elf> elves;
            using (var file = File.OpenText("./res/Day1.txt")) {
                elves = await ReadInElves(file);
            }

            var bestFedElf = elves.MaxBy(e => e.CalorieTotal);
            Console.WriteLine($"The elf with the most calories has {bestFedElf!.CalorieTotal} calories");
        }

        private async Task<List<Elf>> ReadInElves(StreamReader reader)
        {
            List<Elf> elves = new();
            Elf elf = new();
            while(await reader.ReadLineAsync() is string line)
            {
                if (string.IsNullOrEmpty(line))
                {
                    elves.Add(elf);
                    elf = new();
                }
                else
                {
                    if (int.TryParse(line, out var calorie))
                        elf.CalorieTotal += calorie;
                    else
                        throw new AoCException("Invalid input: " + line);
                }
            }
            elves.Add(elf);
            return elves;
        }
    }

    class Elf
    {
        public Elf() { }
        public int CalorieTotal { get; set; }
    }
}

