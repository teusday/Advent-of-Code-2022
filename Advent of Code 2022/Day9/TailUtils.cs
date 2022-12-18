using System;
namespace Advent_of_Code_2022.Day9
{
    public class TailUtils
    {
        public static (MovementDirection, RelativePosition, bool CanRepeat)
            ComputeNewRelativePosition(RelativePosition startPosition, MovementDirection headMovement)
        {
            //Head only moves in 4 directions - Up, Right, Down, Left
            return (startPosition, headMovement) switch
            {
                (RelativePosition.Up, MovementDirection.Up) => (MovementDirection.None, RelativePosition.Beneath, false),
                (RelativePosition.Up, MovementDirection.Right) => (MovementDirection.None, RelativePosition.UpLeft, false),
                (RelativePosition.Up, MovementDirection.Down) => (MovementDirection.Down, RelativePosition.Up, true),
                (RelativePosition.Up, MovementDirection.Left) => (MovementDirection.None, RelativePosition.UpRight, false),

                (RelativePosition.UpRight, MovementDirection.Up) => (MovementDirection.None, RelativePosition.Right, false),
                (RelativePosition.UpRight, MovementDirection.Right) => (MovementDirection.None, RelativePosition.Up, false),
                (RelativePosition.UpRight, MovementDirection.Down) => (MovementDirection., RelativePosition., true),
                (RelativePosition.UpRight, MovementDirection.Left) => (MovementDirection., RelativePosition., false),

                (RelativePosition., MovementDirection.Up) => (MovementDirection., RelativePosition., ),
                (RelativePosition., MovementDirection.Right) => (MovementDirection., RelativePosition., ),
                (RelativePosition., MovementDirection.Down) => (MovementDirection., RelativePosition., ),
                (RelativePosition., MovementDirection.Left) => (MovementDirection., RelativePosition., ),

            }
        }


    }
}


/*
--T
-H-
---
*/