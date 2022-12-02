using System.CommandLine;

namespace Advent_of_Code_2022.Day1
{
    public static class Day1
    {
        public static Command Command()
        {
            var c = new Command("1","Elf calorie counting");
            var topNOption = new Option<int>(new[] { "--top", "-t" }, ()=>1)
            {
                ArgumentHelpName = "Top N",
                Description = "Get the elves with the top N calorie counts",
                Name = "top",
            };
            c.AddOption(topNOption);
            c.SetHandler(async (n) =>
            {
                await new Day1Runner().Run(n);
            }, topNOption);
            return c;
        }
    }
}

