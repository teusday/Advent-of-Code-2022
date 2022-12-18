using System;
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
        public Point Head { get; set; }
        public Point Tail { get; set; }

        private Action<Point>? OnTailChange;

        public void SetOnTailChange(Action<Point> onTailChange)
        {
            OnTailChange = onTailChange;
            OnTailChange(Tail);
        }

        public void MoveHead(Movement movement)
        {
            foreach (var _ in Enumerable.Range(0, movement.Count))
            {
                Head = MovePoint(movement.direction, Head);
                ComputeTail();
            }
        }

        /*
         * *946*
         * 91116
         * 31T12
         * 81117
         * *857*
         */

        private void ComputeTail()
        {
            var xDiff = Head.x - Tail.x;
            var yDiff = Head.y - Tail.y;
            if (Math.Abs(yDiff) <= 1 && Math.Abs(xDiff) <= 1)
                return; //Still near enough to not move (1)
            if (yDiff == 0)
            { // Along same x-axis as head
                if (xDiff > 0) //2
                    SetTail(Tail with { x = Tail.x + 1 });
                else //3
                    SetTail(Tail with { x = Tail.x - 1 });
            }
            else if (xDiff == 0)
            { // Along same y-axis as head
                if (yDiff > 0) //4
                    SetTail(Tail with { y = Tail.y + 1 });
                else //5
                    SetTail(Tail with { y = Tail.y - 1 });
            }
            else if (xDiff > 0 && yDiff > 0) //6
                SetTail(MovePoint(MovementDirection.UpRight, Tail));
            else if (xDiff > 0 && yDiff < 0) //7
                SetTail(MovePoint(MovementDirection.DownRight, Tail));
            else if (xDiff < 0 && yDiff < 0)//8
                SetTail(MovePoint(MovementDirection.DownLeft, Tail));
            else if (xDiff < 0 && yDiff > 0)
                SetTail(MovePoint(MovementDirection.UpLeft, Tail));
            else
                throw new AoCException("I goofed!!");
        }

        private void SetTail(Point p)
        {
            var lastTail = Tail;
            Tail = p;
            if (Tail != lastTail && OnTailChange != null)
                OnTailChange(p);
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

