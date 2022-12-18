using System;
namespace Advent_of_Code_2022.Day9
{
    public readonly record struct Movement(MovementDirection direction, int Count);

    public enum MovementDirection
    {
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft,
        None
    }
}

