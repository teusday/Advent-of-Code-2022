using System;
namespace Advent_of_Code_2022.Utils
{
    public static class CharUtils
    {
        public static int ParseInt(char c)
        {
            // It's probably safe to just treat the char like a number
            if (c < '0' || c > '9')
                throw new AoCException($"Cannot parse {c} to int");
            return c - '0';
        }
    }
}

