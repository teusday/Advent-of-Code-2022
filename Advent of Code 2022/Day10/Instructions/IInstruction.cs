using System;
namespace Advent_of_Code_2022.Day10.Instructions
{
    public interface IInstruction
    {
        public int Cycles { get; }

        /// <returns>Whether the instruction is complete</returns>
        public bool DoCycle(Device d);
    }
}

