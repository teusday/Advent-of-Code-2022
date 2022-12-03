using System.Diagnostics.CodeAnalysis;

namespace Advent_of_Code_2022.Day2
{
    class GameRound
    {
        public RPSChoice TheirThrow { get; set; }
        public RPSChoice MyThrow { get; set; }

        public int Score
        {
            get
            {
                var throwValue = MyThrow switch
                {
                    RPSChoice.Rock => 1,
                    RPSChoice.Paper => 2,
                    RPSChoice.Scissors => 3,
                    _ => throw new AoCException("Did you... add new items? To Rock Paper Scissors?")
                };
                var outcomeValue = DetermineWinner() switch
                {
                    GameOutcome.TheyWin => 0,
                    GameOutcome.Draw => 3,
                    GameOutcome.IWin => 6,
                    _ => throw new AoCException("If you didn't win, and they didn't win, and it wasn't a draw... what was it?")
                };

                return throwValue + outcomeValue;
            }
        }

        private GameOutcome DetermineWinner()
        {
            if (TheirThrow == MyThrow)
                return GameOutcome.Draw;
            else if (GameRules.PlayThatLosesTo[TheirThrow] == MyThrow)
                return GameOutcome.TheyWin;
            else if (GameRules.PlayThatLosesTo[MyThrow] == TheirThrow)
                return GameOutcome.IWin;
            else
                throw new AoCException($"Could not find winner of {TheirThrow}(theirs) and {MyThrow}(mine)");
        }

        public static bool TryParse(string toParse, [NotNullWhen(true)] out GameRound? gameRound)
        {
            gameRound = null;
            if (toParse.Length != 3)
                return false;

            var theirThrow = toParse[0];
            var desiredOutcome = toParse[2];

            try //There are two idioms competing here - TryParse means we just return false when there's an error, but the switch statement won't let us return from the function.
            {
                var theirThrowParsed = theirThrow switch
                {
                    'A' => RPSChoice.Rock,
                    'B' => RPSChoice.Paper,
                    'C' => RPSChoice.Scissors,
                    _ => throw new AoCException("Invalid 'their throw' column: " + theirThrow)
                };

                var desiredOutcomeParsed = desiredOutcome switch
                {
                    'X' => GameOutcome.TheyWin,
                    'Y' => GameOutcome.Draw,
                    'Z' => GameOutcome.IWin,
                    _ => throw new AoCException("Invalid 'desired outcome' column: " + desiredOutcome)
                };

                gameRound = new()
                {
                    TheirThrow = theirThrowParsed,
                    MyThrow = DetermineMyThrow(desiredOutcomeParsed, theirThrowParsed)
                };
            }
            catch (AoCException)
            {
                gameRound = null;
                return false;
            }
            return true;

            RPSChoice DetermineMyThrow(GameOutcome desiredOutcome, RPSChoice theirThrow)
            {
                return desiredOutcome switch
                {
                    GameOutcome.TheyWin => GameRules.PlayThatLosesTo[theirThrow],
                    GameOutcome.Draw => theirThrow,
                    GameOutcome.IWin => GameRules.PlayThatBeats[theirThrow],
                    _ => throw new AoCException("C# hates switch expressions")
                };
            }
        }
    }

    public enum RPSChoice
    {
        Rock,
        Paper,
        Scissors
    }

    public enum GameOutcome
    {
        TheyWin,
        Draw,
        IWin
    }

    public static class GameRules
    {
        public static Dictionary<RPSChoice, RPSChoice> PlayThatLosesTo = new()
        {
            {RPSChoice.Rock, RPSChoice.Scissors},
            {RPSChoice.Paper, RPSChoice.Rock },
            {RPSChoice.Scissors, RPSChoice.Paper }
        };
        public static Dictionary<RPSChoice, RPSChoice> PlayThatBeats = new()
        {
            {RPSChoice.Rock, RPSChoice.Paper },
            {RPSChoice.Paper, RPSChoice.Scissors },
            {RPSChoice.Scissors, RPSChoice.Rock }
        };
    }
}

