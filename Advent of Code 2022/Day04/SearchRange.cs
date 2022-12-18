using System;
namespace Advent_of_Code_2022.Day4
{
    public class SearchRange
    {
        public SearchRange(int start, int end)
        {
            Start = start;
            End = end;
        }

        public SearchRange(string startToEnd)
        {
            var range = startToEnd.Split("-");
            if (range.Length != 2)
                throw new AoCException($"Incorrect argument for SearchRange: {startToEnd}");
            if (!int.TryParse(range[0], out int start))
                throw new AoCException($"Could not parse {range[0]}");
            if (!int.TryParse(range[1], out int end))
                throw new AoCException($"Could not parse {range[1]}");

            Start = start;
            End = end;
        }

        public int Start { get; }
        public int End { get; }

        public bool Contains(SearchRange otherRange)
        {
            return otherRange.Start >= Start && otherRange.End <= End;
        }

        public bool Overlaps(SearchRange otherRange)
        {
            var nonOverlapping = otherRange.End < Start || otherRange.Start > End;
            return !nonOverlapping;
        }
    }
}

