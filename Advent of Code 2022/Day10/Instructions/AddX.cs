using System;
namespace Advent_of_Code_2022.Day10.Instructions
{
    public class AddX : IInstruction
    {
        private readonly int addValue;
        public AddX(int addValue)
        {
            this.addValue = addValue;
        }

        public int Cycles => 2;

        private int cyclesCompleted = 0;
        public bool DoCycle(Device d)
        {
            cyclesCompleted += 1;
            if (cyclesCompleted == 2)
            {
                d.X += addValue;
                return true;
            }
            else
                return false;
        }
    }
}

