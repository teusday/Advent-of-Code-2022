using System;
using System.CommandLine;

namespace Advent_of_Code_2022.Day4
{
    public static class Day4
    {
        public static Command Command(Option<FileInfo?> fileOption)
        {
            var c = new Command("4", "Camp Cleanup");

            c.SetHandler(async (file) =>
            {
                await new Day4Runner().Run(file ?? new FileInfo("./res/Day4.txt"));
            }, fileOption);
            return c;
        }
    }
}

