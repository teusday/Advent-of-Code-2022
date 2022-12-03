using System;
using System.CommandLine;

namespace Advent_of_Code_2022.Day3
{
    public static class Day3
    {
        public static Command Command()
        {
            var c = new Command("3", "Rucksack repacking");
            c.SetHandler(async () =>
            {
                await new Day3Runner().Run();
            });
            return c;
        }
    }
}

