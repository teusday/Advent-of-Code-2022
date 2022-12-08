using System;
using System.CommandLine;

namespace Advent_of_Code_2022.Day6
{
    public class Day6
    {
        public static Command Command(Option<FileInfo?> fileOption)
        {
            var c = new Command("6", "Signal Tuning");

            c.SetHandler(async (file) =>
            {
                await new Day6Runner().Run(file ?? new FileInfo("./res/Day6.txt"));
            }, fileOption);

            return c;
        }
    }
}

