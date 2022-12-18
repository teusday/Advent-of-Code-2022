using System;
using System.CommandLine;

namespace Advent_of_Code_2022.Day8
{
    public class Day8
    {
        public static Command Command(Option<FileInfo?> fileOption)
        {
            var c = new Command("8", "Treetops");

            c.SetHandler((f) =>
            {
                return new Day8Runner().Run(f ?? new FileInfo("res/Day8.txt"));
            }, fileOption);

            return c;
        }
    }
}

