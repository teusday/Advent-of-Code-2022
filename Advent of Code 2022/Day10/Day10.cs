using System;
using System.CommandLine;

namespace Advent_of_Code_2022.Day10
{
    public class Day10
    {
        public static Command Command(Option<FileInfo?> fileOption)
        {
            var c = new Command("10", "CRT");

            c.SetHandler((f) =>
            {
                return new Day10Runner().Run(f ?? new FileInfo("res/Day10.txt"));
            }, fileOption);

            return c;
        }
    }
}

