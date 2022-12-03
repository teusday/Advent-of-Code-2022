using System;
using System.CommandLine;

namespace Advent_of_Code_2022.Day2
{
    public static class Day2
    {
        public static Command Command()
        {
            var c = new Command("2", "Rock paper scisors");
            
            c.SetHandler(async () =>
            {
                await new Day2Runner().Run();
            });
            return c;
        }
    }
}

