using System;

namespace Advent_of_Code_2022.Day7
{
    public class Day7Runner
    {
        public Day7Runner()
        {
        }

        public async Task Run(FileInfo f)
        {
            // Get XSH instructions
            List<IXSHInstruction> instructions;
            using (var file = f.OpenText())
            {
                instructions = await ReadInXshLines(file);
            }

            // Reverse-engineer filesystem
            var fileSystem = new ELFS();
            foreach (var i in instructions)
            {
                if (i is XSH_CD cd)
                {
                    fileSystem.ChangeDirectory(cd.Directory);
                }
                else if (i is XSH_LS ls)
                {
                    foreach (var r in ls.Results)
                    {
                        if (r.IsDir)
                            fileSystem.AddDir(r.Name);
                        else
                            fileSystem.AddFile(r.Name, r.Size!.Value);
                    }
                }
            }

            // Find directories under max size (100,000)
            var smallDirectories = fileSystem.AllDirectories().Where(d => d.GetSize() < 100_000);
            Console.WriteLine("Directories under 100,000: " + string.Join("; ", smallDirectories.Select(d => d.Name)));
            Console.WriteLine("Their total size is: " + smallDirectories.Sum(d => d.GetSize()));

            //Searching for the smallest directory larger than ideal size
            var idealSize = 30000000;
            var bestFit = fileSystem.AllDirectories().Aggregate<ELFSDir, (ELFSDir? d, int s)>((null, 0), (acc, cur) =>
            {
                var s = cur.GetSize();
                if (s < idealSize)
                    return acc;
                else if (acc.d == null || acc.s > s)
                    return (cur, s); // better than accummulator
                else
                    return acc;
            });
            if (bestFit.d == null)
                Console.WriteLine("No directory fit the requirements");
            else
                Console.WriteLine($"Best fit directory is {bestFit.d.Name} with size {bestFit.s}");
        }

        public async Task<List<IXSHInstruction>> ReadInXshLines(StreamReader file)
        {
            List<IXSHInstruction> instructions = new();
            IXSHInstruction? lastInstruction = null;
            while (await file.ReadLineAsync() is string line)
            {
                var tokens = line.Split(" ");
                if (tokens[0] == "$")
                { //This is an instruction
                    if (lastInstruction != null)
                        instructions.Add(lastInstruction);
                    lastInstruction = ParseInstruction(tokens);
                }
                else
                { //This is a result
                    if (lastInstruction is null)
                        throw new AoCException("Cannot have results without an instruction");
                    if (lastInstruction is not XSH_LS lastLs)
                        throw new AoCException("Non-LS commands can't have results");
                    lastLs.Results.Add(new(tokens));
                }
            }
            if (lastInstruction != null)
                instructions.Add(lastInstruction);
            return instructions;
        }

        private static IXSHInstruction ParseInstruction(string[] tokens)
        {
            if (tokens[1] == "cd")
            {
                return new XSH_CD(tokens[2]);
            }
            else if (tokens[1] == "ls")
            {
                return new XSH_LS();
            }
            else
            {
                throw new AoCException("Unrecognized command: " + string.Join(" ", tokens));
            }
        }
    }
}

