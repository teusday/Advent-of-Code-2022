using System;
namespace Advent_of_Code_2022.Day10.Instructions
{
    public class NoOp : IInstruction
    {
        public NoOp()
        {
        }

        public int Cycles => 1;

        public bool DoCycle(Device d)
        {
            return true;
        }
    }
}

