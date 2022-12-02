namespace Advent_of_Code_2022.Day1
{
	public class Day1Runner
	{
		public Day1Runner()
		{
		}

        public async Task Run(int topN)
        {
            List<Elf> elves;
            using (var file = File.OpenText("./res/Day1.txt")) {
                elves = await ReadInElves(file);
            }

            var topNElves = elves.OrderByDescending(e => e.CalorieTotal)
                .Take(topN);
            var bestFedElf = elves.MaxBy(e => e.CalorieTotal);

            var elfPluralize = topNElves.Count() switch
            {
                0 => "elves",
                1 => "elf",
                _ => "elves"
            };

            //TODO Roslyn code generator to do rust-like pluralize (see https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator and https://stackoverflow.com/a/21274149)
            Console.WriteLine($"The {elfPluralize} with the most calories: "+string.Join(", ",topNElves));
            if(topNElves.Count() > 1)
            {
                Console.WriteLine($"Together, they have {topNElves.Sum(e=>e.CalorieTotal)} calories");
            }
        }

        private static async Task<List<Elf>> ReadInElves(StreamReader reader)
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

        public override string ToString()
        {
            return CalorieTotal.ToString();
        }
    }
}

