using System;
namespace Advent_of_Code_2022.Day8
{
    public class Day8Runner
    {
        public Day8Runner()
        {
        }

        public async Task Run(FileInfo file)
        {
            Forest forest;
            using (var f = file.OpenText())
            {
                forest = await ReadInForest(f);
            }

            var visiblity = forest.DetermineVisibility();

            Console.WriteLine($"Visibile trees: {visiblity.VisibleCount()}");

            var scenicMatrix = forest.CalculateScenicScores();

            Console.WriteLine($"Best scenic score possible is {scenicMatrix.BestScore()}");
        }

        private async Task<Forest> ReadInForest(StreamReader f)
        {
            var forest = new Forest();
            while (await f.ReadLineAsync() is string line)
            {
                var asNumbers = line.ToCharArray().Select(c => CharUtils.ParseInt(c));
                forest.AddRow(asNumbers);
            }
            return forest;
        }
    }
}

