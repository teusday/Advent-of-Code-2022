using System;
using System.CommandLine;

namespace Advent_of_Code_2022.Day9
{
    public class Day9
    {
        public static Command Command(Option<FileInfo?> fileOption)
        {
            var c = new Command("9", "Rope Bridge");

            var knotCount = new Option<int>(new string[] { "--knots", "-k" }, () => 2);
            c.AddOption(knotCount);

            c.SetHandler((f, c) =>
            {
                return new Day9Runner().Run(f ?? new FileInfo("res/Day9.txt"), c);
            }, fileOption, knotCount);

            return c;
        }
    }
}

