using System;
namespace Advent_of_Code_2022.Day10.Instructions
{
    public class InstructionFactory
    {
        public static IInstruction Create(string textRepresentation)
        {
            var tokens = textRepresentation.Split(" ");
            if (tokens[0] == "noop")
                return new NoOp();
            else if (tokens[0] == "addx")
            {
                if (!int.TryParse(tokens[1], out int value))
                    throw new AoCException("Cannot parse " + textRepresentation);
                return new AddX(value);
            }
            else
            {
                throw new AoCException("Unknown instruction: " + textRepresentation);
            }
        }
    }
}

