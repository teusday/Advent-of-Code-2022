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

            return (TheirThrow, MyThrow) switch
            {
                (RPSChoice.Rock, RPSChoice.Paper) => GameOutcome.IWin,
                (RPSChoice.Rock, RPSChoice.Scissors) => GameOutcome.TheyWin,
                (RPSChoice.Paper, RPSChoice.Rock) => GameOutcome.TheyWin,
                (RPSChoice.Paper, RPSChoice.Scissors) => GameOutcome.IWin,
                (RPSChoice.Scissors, RPSChoice.Rock) => GameOutcome.IWin,
                (RPSChoice.Scissors, RPSChoice.Paper) => GameOutcome.TheyWin,
                _ => throw new AoCException("Did you... add new items? To Rock Paper Scissors?")
            };
        }

        public static bool TryParse(string toParse, [NotNullWhen(true)] out GameRound? gameRound)
        {
            gameRound = null;
            if (toParse.Length != 3)
                return false;

            var theirThrow = toParse[0];
            var myThrow = toParse[2];

            try //There are two idioms competing here - TryParse means we just return false when there's an error, but the switch statement won't let us return from the function.
            {
                gameRound = new()
                {
                    TheirThrow = theirThrow switch
                    {
                        'A' => RPSChoice.Rock,
                        'B' => RPSChoice.Paper,
                        'C' => RPSChoice.Scissors,
                        _ => throw new AoCException("Invalid 'their throw' column: " + theirThrow)
                    },
                    MyThrow = myThrow switch
                    {
                        'X' => RPSChoice.Rock,
                        'Y' => RPSChoice.Paper,
                        'Z' => RPSChoice.Scissors,
                        _ => throw new AoCException("Invalid 'my throw' column: " + theirThrow)
                    }
                };
            }
            catch (AoCException)
            {
                gameRound = null;
                return false;
            }
            return true;
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
}

