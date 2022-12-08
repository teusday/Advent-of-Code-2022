using System;
using System.CommandLine;
using System.IO;

namespace Advent_of_Code_2022.Day7
{
    public class Day7
    {
        public static Command Command(Option<FileInfo?> fileOption)
        {
            var c = new Command("7", "Disk Expoloration");

            c.SetHandler(async (file) =>
            {
                await new Day7Runner().Run(file ?? new FileInfo("./res/Day7.txt"));
            }, fileOption);
            return c;
        }
    }
}

