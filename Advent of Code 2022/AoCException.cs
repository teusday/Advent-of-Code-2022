using System;
namespace Advent_of_Code_2022
{
    /// <summary>
    /// Just so I know if it's an error I caused
    /// </summary>
    public class AoCException: Exception
    {
        public AoCException(string message): base(message)
        {
        }
    }
}

