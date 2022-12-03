using System;
using System.CommandLine;

namespace Advent_of_Code_2022.Day3
{
    public static class Day3
    {
        public static Command Command()
        {
            var c = new Command("3", "Rucksack repacking");

            var wrong = new Command("wrong", "Find wrongly placed items");
            wrong.SetHandler(async () =>
            {
                await new Day3Runner().GetTotalForWrongItems();
            });
            c.AddCommand(wrong);

            var group = new Command("group", "Find group badges");
            group.SetHandler(async () =>
            {
                await new Day3Runner().GetTotalForGroupBadges();
            });
            c.AddCommand(group);

            return c;
        }
    }
}

