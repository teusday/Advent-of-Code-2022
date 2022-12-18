using System;
namespace Advent_of_Code_2022.Day3
{
    public static class ItemUtil
    {
        public static int ItemPriority(char item)
        {
            if ('a' <= item && 'z' >= item)
            {
                return item - 'a' + 1;
            }
            else if ('A' <= item && 'Z' >= item)
            {
                return item - 'A' + 27;
            }
            else
            {
                throw new AoCException("Invalid item for priority calculation: " + item);
            }
        }
    }
}

