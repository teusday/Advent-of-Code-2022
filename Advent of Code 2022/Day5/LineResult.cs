using System;
namespace Advent_of_Code_2022.Day5
{
    //C# Hates Sum Types
    public interface ILineResult
    {

    }

    public readonly record struct Containers(char?[] ContainersArray) : ILineResult
    {
    }

    public readonly record struct StackNumbers(int[] Numbers) : ILineResult
    {
    }
}

