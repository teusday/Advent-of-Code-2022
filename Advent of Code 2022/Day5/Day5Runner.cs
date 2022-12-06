using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Advent_of_Code_2022.Day4;

namespace Advent_of_Code_2022.Day5
{
    public partial class Day5Runner
    {
        private bool ShowIntermediateSteps;
        private CrateMoverModel Model;
        public Day5Runner(bool showIntermediateSteps, CrateMoverModel model)
        {
            ShowIntermediateSteps = showIntermediateSteps;
            Model = model;
        }

        private void OutputContainers(ContainerStack[] containers)
        {
            foreach (var (c, i) in containers.Select((c, i) => (c, i)))
            {
                Console.WriteLine($"{i + 1}: {c.ToString()}");
            }
        }

        public async Task Run()
        {
            ContainerStack[] containers;
            List<CraneInstruction> instructions;

            using (var file = File.OpenText("./res/Day5.txt"))
            {
                (containers, instructions) = await ReadInData(file);
            }

            Console.WriteLine("Initial setup:");
            OutputContainers(containers);

            Console.WriteLine("Moving...");
            foreach (var i in instructions)
            {
                i.Apply(containers, Model);

                if (ShowIntermediateSteps)
                    OutputContainers(containers);
            }

            Console.WriteLine("Final setup:");
            OutputContainers(containers);
        }

        private async Task<(ContainerStack[], List<CraneInstruction>)> ReadInData(StreamReader file)
        {
            Stack<char?>[]? containerStacksForBuilding = null;
            while (await file.ReadLineAsync() is string line)
            {
                if (string.IsNullOrEmpty(line))
                    break; //Continue on to instruction parsing

                var lineResult = GetLineResult(line);

                if (lineResult is Containers(var c))
                {
                    if (containerStacksForBuilding == null)
                    {
                        containerStacksForBuilding = c.Select(_ => new Stack<char?>()).ToArray();
                    }

                    foreach (var (ch, stack) in c.Zip(containerStacksForBuilding))
                    {
                        stack.Push(ch);
                    }
                }
                else if (lineResult is StackNumbers(var stackNums))
                {
                    continue; //If we get errors, this is the place to look
                }
                else
                {
                    throw new AoCException("Could not determine line type");
                }
            }

            var containers = containerStacksForBuilding!.Select(s => new ContainerStack(s))
                .ToArray();

            Regex pattern = InstructionPattern();
            List<CraneInstruction> instructions = new List<CraneInstruction>();
            while (await file.ReadLineAsync() is string line)
            {
                var matches = pattern.Match(line);
                instructions.Add(new()
                {
                    Count = int.Parse(matches.Groups[1].Value),
                    StartStack = int.Parse(matches.Groups[2].Value),
                    EndStack = int.Parse(matches.Groups[3].Value),
                });
            }

            return (containers, instructions);
        }

        ILineResult GetLineResult(string line)
        {
            var resultType = LineResult.Unknown;

            var a = line
                .Chunk(4)
                .Select(ParseChunk)
                .ToArray();
            return resultType switch
            {
                LineResult.Container => new Containers(a),
                LineResult.StackNumber => new StackNumbers(a.Select(a => int.Parse(a.ToString()!)).ToArray()),
                _ => throw new AoCException("Could not determine line type")
            };


            char? ParseChunk(char[] c)
            {
                //every chunk is either 3 or 4 chars in length, and either formatted as '[x]', ' n ', or '   '
                if (c[1] == ' ')
                    return null;
                else if (c[0] == '[')
                {
                    resultType = LineResult.Container;
                    return c[1];
                }
                else
                {
                    resultType = LineResult.StackNumber;
                    return c[1];
                }
            }
        }

        [GeneratedRegex("move (\\d+) from (\\d+) to (\\d+)")]
        private static partial Regex InstructionPattern();
    }

    enum LineResult
    {
        Unknown,
        Container,
        StackNumber
    }

    class ContainerStack
    {
        private Stack<char> stack = new Stack<char>();
        public ContainerStack(IEnumerable<char?> chars)
        {
            stack = new Stack<char>(chars.Where(e => e.HasValue).Select(c => (char)c!));
        }

        public char Pop()
        {
            return stack.Pop();
        }

        public void Push(char c)
        {
            stack.Push(c);
        }

        public override string ToString()
        {
            return string.Join("|", stack.Reverse().ToList());
        }
    }

    public class CraneInstruction
    {
        public int StartStack { get; set; }
        public int EndStack { get; set; }

        public int Count { get; set; }

        internal void Apply(ContainerStack[] containers, CrateMoverModel model)
        {
            var startStack = containers[StartStack - 1];
            var endStack = containers[EndStack - 1];
            //This isn't efficient, but I'm tired
            if (model == CrateMoverModel.CrateMover9000)
            {
                foreach (var _ in Enumerable.Range(0, Count))
                {
                    var container = startStack.Pop();
                    endStack.Push(container);
                }
            }
            else if (model == CrateMoverModel.CrateMover9001)
            {
                var mediumStack = new Stack<char>();
                foreach (var _ in Enumerable.Range(0, Count))
                {
                    var container = startStack.Pop();
                    mediumStack.Push(container);
                }
                while (mediumStack.TryPeek(out var _))
                {
                    var container = mediumStack.Pop();
                    endStack.Push(container);
                }
            }
        }
    }
}

