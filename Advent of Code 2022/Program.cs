using System.CommandLine;

class Program
{
    public static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand("Runner for Advent of Code 2022");
        Argument<int> dayArg = new()
        {
            Description = "Day of AoC to run",
            Name = "Day",
        };
        rootCommand.AddArgument(dayArg);

        rootCommand.SetHandler(async day =>
        {
            try
            {
                IAoCRunner runner = day switch
                {
                    1 => new Day1Runner(),
                    _ => throw new AoCException($"Day {day} runner not found!")
                };
                await runner.Run();
            }
            catch (AoCException e)
            {
                var originalForeground = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("Encountered an error:\n" + e.Message);
                Console.ForegroundColor = originalForeground;
            }
        }, dayArg);

        await rootCommand.InvokeAsync(args);
    }
}