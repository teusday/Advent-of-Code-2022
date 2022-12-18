using System;
using Advent_of_Code_2022.Day10.Instructions;

namespace Advent_of_Code_2022.Day10
{
    public class Day10Runner
    {
        public async Task Run(FileInfo file)
        {
            int initialSample = 20;
            int cycleSampleRate = 40;

            Queue<IInstruction> instructionQueue;
            using (var f = file.OpenText())
            {
                instructionQueue = new Queue<IInstruction>(await ReadInInstructions(f));
            }

            List<int> samples = new List<int>();

            Device d = new Device();
            int cycle = 0;

            while (instructionQueue.Any())
            {
                cycle++;
                var ins = instructionQueue.Peek();
                if (ins.DoCycle(d))
                    instructionQueue.Dequeue(); //Instruction completed; dequeue

                if ((cycle - initialSample) % cycleSampleRate == 0)
                    samples.Add(cycle * d.X);
            }

            Console.WriteLine("Sample sum is " + samples.Sum());
        }

        public async Task<List<IInstruction>> ReadInInstructions(StreamReader file)
        {
            List<IInstruction> instructions = new List<IInstruction>();
            while (await file.ReadLineAsync() is string line)
            {
                instructions.Add(InstructionFactory.Create(line));
            }
            return instructions;
        }
    }
}

