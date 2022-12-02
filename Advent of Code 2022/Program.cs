using System.CommandLine;

class Program
{
    public static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand("Runner for Advent of Code 2022");

        rootCommand.AddCommand(Day1.Command());

        try
        {
            await rootCommand.InvokeAsync(args);
        }catch(AoCException e)
        {
            var originalForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("Encountered an error:\n" + e.Message);
            Console.ForegroundColor = originalForeground;
        }
    }
}