using System;
using System.Reflection.PortableExecutable;

namespace Advent_of_Code_2022.Day2
{
    public class Day2Runner
    {
        public Day2Runner()
        {
        }

        public async Task Run()
        {
            List<GameRound> rounds;
            using (var file = File.OpenText("./res/Day2.txt"))
            {
                rounds = await ReadInRounds(file);
            }

            var totalScore = rounds.Sum(r => r.Score);
            Console.WriteLine($"With the given strategy guide, your score would be {totalScore}");
        }

        private async Task<List<GameRound>> ReadInRounds(StreamReader file)
        {
            List<GameRound> rounds = new();
            while (await file.ReadLineAsync() is string line)
            {
                if (!GameRound.TryParse(line, out var gameRound))
                    throw new AoCException("Error reading line: " + line);
                rounds.Add(gameRound);
            }

            return rounds;
        }
    }
}

