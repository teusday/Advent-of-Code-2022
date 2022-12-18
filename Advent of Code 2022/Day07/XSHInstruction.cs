using System;
namespace Advent_of_Code_2022.Day7
{
    //X-mas Shell
    public interface IXSHInstruction { }
    public class XSH_CD : IXSHInstruction
    {
        public XSH_CD(string directory)
        {
            Directory = directory;
        }
        public string Directory { get; set; }
    }

    public class XSH_LS : IXSHInstruction
    {
        public XSH_LS()
        {
            Results = new List<LSResult>();
        }
        public List<LSResult> Results { get; set; }
    }

    public class LSResult
    {
        public LSResult(string[] tokens)
        {
            if (tokens[0] == "dir")
            {
                IsDir = true;
            }
            else
            {
                if (!int.TryParse(tokens[0], out var size))
                    throw new AoCException("Cannot read size of non-dir ls result: " + string.Join(" ", tokens));
                Size = size;
            }
            Name = tokens[1];
        }

        public bool IsDir { get; set; }
        public int? Size { get; set; }

        public string Name { get; set; }
    }
}

