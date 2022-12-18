using System;

namespace Advent_of_Code_2022.Day9
{
    public class Day9Runner
    {
        public async Task Run(FileInfo file, int knotCount)
        {
            List<Movement> headMovements;
            using (var f = file.OpenText())
            {
                headMovements = await ReadInMovements(f);
            }

            VisitedPointSet tailVisited = new VisitedPointSet();
            KnotState state = new KnotState(knotCount);
            state.SetOnLastKnotChange((p) => tailVisited.AddPoint(p));

            foreach (var movement in headMovements)
            {
                state.MoveHead(movement);
            }

            Console.WriteLine($"{tailVisited.VisitedPoints.Count} points visited by tail");
        }

        public async Task<List<Movement>> ReadInMovements(StreamReader file)
        {
            var movements = new List<Movement>();
            while (await file.ReadLineAsync() is string line)
            {
                var movement = line.Split(" ");

                var direction = movement[0] switch
                {
                    "U" => MovementDirection.Up,
                    "R" => MovementDirection.Right,
                    "D" => MovementDirection.Down,
                    "L" => MovementDirection.Left,
                    string s => throw new AoCException("Unrecognized direction " + s)
                };

                if (!int.TryParse(movement[1], out int count))
                {
                    throw new AoCException("Cannot parse count " + movement[1]);
                }
                movements.Add(new Movement(direction, count));
            }
            return movements;
        }
    }
}

