using System;
namespace Advent_of_Code_2022.Day9
{
    public class VisitedPointSet
    {
        public HashSet<Point> VisitedPoints { get; set; }

        public VisitedPointSet()
        {
            VisitedPoints = new HashSet<Point>();
        }

        public void AddPoint(Point p)
        {
            VisitedPoints.Add(p);
        }
    }
}

