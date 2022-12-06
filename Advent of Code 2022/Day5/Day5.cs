using System;
using System.CommandLine;

namespace Advent_of_Code_2022.Day5
{
    public static class Day5
    {
        public static Command Command()
        {
            var c = new Command("5", "Crate Stacking");

            var intermediateSteps = new Option<bool>("-i", "Show intermediate steps");
            c.AddOption(intermediateSteps);

            var model = new Option<CrateMoverModel>("--model", () => CrateMoverModel.CrateMover9000, "Model of Crate Mover");
            c.AddOption(model);

            c.SetHandler(async (bool intermediate, CrateMoverModel cmm) =>
            {
                await new Day5Runner(intermediate, cmm).Run();
            }, intermediateSteps, model);
            return c;
        }
    }
}

