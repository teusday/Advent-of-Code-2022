using System;

namespace Advent_of_Code_2022.Day6
{
    public class Day6Runner
    {
        public Day6Runner()
        {
        }

        public async Task Run(FileInfo f)
        {
            char[] signal;
            using (var file = f.OpenText())
            {
                signal = (await file!.ReadLineAsync())!.ToCharArray();
            }

            int? packetStart = null;
            for (int i = 3; i < signal.Length; i++)
            {
                var test = new ArraySegment<char>(signal, i - 3, 4);
                var allDistinct = test.Distinct().Count() == 4;

                if (allDistinct)
                {
                    packetStart = i;
                    break;
                }
            }

            if (packetStart == null)
                Console.WriteLine("Could not locate signal start");
            else
                Console.WriteLine($"Signal start at character {packetStart + 1}");
        }
    }
}

