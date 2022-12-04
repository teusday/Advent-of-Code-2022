using System;
using System.CommandLine;

namespace Advent_of_Code_2022.Day4
{
    public static class Day4
    {
        public static Command Command()
        {
            var c = new Command("4", "Camp Cleanup");

            c.SetHandler(async () =>
            {
                await new Day4Runner().Run();
            });
            return c;
        }
    }
}

