using System;
namespace Advent_of_Code_2022.CustomExtensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ElementsIn<T>(this IEnumerable<T> e, Range range)
        {
            var (offset, length) = range.GetOffsetAndLength(e.Count());
            return e.Skip(offset).Take(length);
        }
    }
}

