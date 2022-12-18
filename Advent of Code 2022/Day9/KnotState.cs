namespace Advent_of_Code_2022.Day9
{
    /*
    +y
    /\
    |
    |
    +--> +X
    */
    public class KnotState
    {
        public Point[] Knots { get; set; }

        public KnotState(int knotCount)
        {
            Knots = new Point[knotCount];
        }

        private Action<Point>? OnLastKnotChange;

        public void SetOnLastKnotChange(Action<Point> onLastKnotChange)
        {
            if (Knots.Any(k => k != new Point(0, 0)))
                throw new AoCException("Cannot change OnLastKnotChange after setting head");
            OnLastKnotChange = onLastKnotChange;
            OnLastKnotChange(Knots.Last());
        }

        public void MoveHead(Movement movement)
        {
            foreach (var _ in Enumerable.Range(0, movement.Count))
            {
                var tailInitial = Knots.Last();
                Knots[0] = MovePoint(movement.direction, Knots[0]);
                for (int i = 1; i < Knots.Length; i++)
                {
                    Knots[i] = ComputeFollower(Knots[i - 1], Knots[i]);
                }

                if (Knots.Last() != tailInitial && OnLastKnotChange != null)
                    OnLastKnotChange(Knots.Last());
            }
        }

        /*
         * *734*
         * 71114
         * 21T12
         * 61115
         * *635*
         */

        private Point ComputeFollower(Point leader, Point follower)
        {
            var xDiff = leader.x - follower.x;
            var yDiff = leader.y - follower.y;
            if (Math.Abs(yDiff) <= 1 && Math.Abs(xDiff) <= 1)
                return follower; //Still near enough to not move (1)
            if (yDiff == 0)
            { // Along same x-axis as head (2)
                return follower with { x = follower.x + Math.Sign(xDiff) };
            }
            else if (xDiff == 0)
            { // Along same y-axis as head (3)
                return follower with { y = follower.y + Math.Sign(yDiff) };
            }
            else if (xDiff > 0 && yDiff > 0) //4
                return MovePoint(MovementDirection.UpRight, follower);
            else if (xDiff > 0 && yDiff < 0) //5
                return MovePoint(MovementDirection.DownRight, follower);
            else if (xDiff < 0 && yDiff < 0)//6
                return MovePoint(MovementDirection.DownLeft, follower);
            else if (xDiff < 0 && yDiff > 0) //7
                return MovePoint(MovementDirection.UpLeft, follower);
            else
                throw new AoCException("I goofed!!");
        }

        private static Point MovePoint(MovementDirection direction, Point p)
        {
            return direction switch
            {
                MovementDirection.Up => p with { y = p.y + 1 },
                MovementDirection.UpRight => p with { x = p.x + 1, y = p.y + 1 },
                MovementDirection.Right => p with { x = p.x + 1 },
                MovementDirection.DownRight => p with { x = p.x + 1, y = p.y - 1 },
                MovementDirection.Down => p with { y = p.y - 1 },
                MovementDirection.DownLeft => p with { x = p.x - 1, y = p.y - 1 },
                MovementDirection.Left => p with { x = p.x - 1 },
                MovementDirection.UpLeft => p with { x = p.x - 1, y = p.y + 1 },
                MovementDirection.None => p,
                _ => throw new AoCException("Unexpected direction")
            };
        }
    }

    public readonly record struct Point(int x, int y);
}

