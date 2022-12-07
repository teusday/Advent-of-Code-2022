using System;
using System.CommandLine;

namespace Advent_of_Code_2022.Day6
{
    public class Day6
    {
        public static Command Command(Option<FileInfo> fileOption)
        {
            var c = new Command("6", "Signal Tuning");

            fileOption.SetDefaultValueFactory(() => new FileInfo("./res/Day6.txt"));

            c.SetHandler(async (file) =>
            {
                await new Day6Runner().Run(file);
            }, fileOption);

            return c;
        }
    }
}

