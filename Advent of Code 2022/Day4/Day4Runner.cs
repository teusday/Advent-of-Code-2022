using System;

namespace Advent_of_Code_2022.Day4
{
    public class Day4Runner
    {
        public Day4Runner()
        {
        }

        public async Task Run()
        {
            List<(SearchRange a, SearchRange b)> searchPairs;
            using (var file = File.OpenText("./res/Day4.txt"))
            {
                searchPairs = await ReadInSearchPairs(file);
            }

            var fullyOverlappingPairs = searchPairs
                .Where(pair => pair.a.Contains(pair.b) || pair.b.Contains(pair.a));

            var overlappingPairs = searchPairs
                .Where(pair => pair.a.Overlaps(pair.b));

            Console.WriteLine($"{fullyOverlappingPairs.Count()} pairs have a fully overlapped range");
            Console.WriteLine($"{overlappingPairs.Count()} pairs have any overlapping range");
        }

        private async Task<List<(SearchRange, SearchRange)>> ReadInSearchPairs(StreamReader file)
        {
            List<(SearchRange, SearchRange)> searchPairs = new();
            while (await file.ReadLineAsync() is string line)
            {
                var pair = line.Split(",");
                if (pair.Length != 2)
                    throw new AoCException("Cannot parse line: " + line);
                searchPairs.Add((new SearchRange(pair[0]), new SearchRange(pair[1])));
            }

            return searchPairs;
        }
    }
}

