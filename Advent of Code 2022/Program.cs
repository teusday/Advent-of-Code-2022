using System.CommandLine;
using Advent_of_Code_2022.Day1;
using Advent_of_Code_2022.Day2;
using Advent_of_Code_2022.Day3;
using Advent_of_Code_2022.Day4;
using Advent_of_Code_2022.Day5;
using Advent_of_Code_2022.Day6;
using Advent_of_Code_2022.Day7;

class Program
{
    public static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand("Runner for Advent of Code 2022");

        var fileOption = new Option<FileInfo?>(
              name: "--file",
              description: "The puzzle input");

        rootCommand.AddOption(fileOption);

        rootCommand.AddCommand(Day1.Command());
        rootCommand.AddCommand(Day2.Command());
        rootCommand.AddCommand(Day3.Command());
        rootCommand.AddCommand(Day4.Command(fileOption));
        rootCommand.AddCommand(Day5.Command());
        rootCommand.AddCommand(Day6.Command(fileOption));
        rootCommand.AddCommand(Day7.Command(fileOption));

        try
        {
            await rootCommand.InvokeAsync(args);
        }
        catch (AoCException e)
        {
            var originalForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("Encountered an error:\n" + e.Message);
            Console.ForegroundColor = originalForeground;
        }
    }
}