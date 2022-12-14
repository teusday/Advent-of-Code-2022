using System;
using Advent_of_Code_2022.CustomExtensions;

namespace Advent_of_Code_2022.Day8
{
    // Using Tree class so it's unique in a HashMap - I know it's not as efficient
    public class Tree
    {
        public Tree(int height)
        {
            Height = height;
        }
        public int Height { get; set; }
    }

    //A forest of perfectly gridded trees (how German)
    public class Forest
    {
        readonly int MAX_HEIGHT = 9;

        List<Tree[]> Trees;
        public Forest()
        {
            Trees = new List<Tree[]>();
        }

        public void AddRow(IEnumerable<int> treeRow)
        {
            Trees.Add(treeRow.Select(i => new Tree(i)).ToArray());
        }

        public IEnumerable<IEnumerable<Tree>> Rows()
        {
            return Trees;
        }

        public IEnumerable<IEnumerable<Tree>> Columns()
        {
            for (var i = 0; i < Trees.First().Length; i++)
            {
                yield return Trees.Select(r => r[i]);
            }
        }

        // After the forest is fully modeled, determine which trees are visible from the outside
        public ForestVisibility DetermineVisibility()
        {
            // Create Visibility container
            var visibility = new ForestVisibility();

            // Search each row LtR
            SetVisible(Rows(), false);

            //Search each row RtL
            SetVisible(Rows(), true);

            // Search each column Top-to-Bottom
            SetVisible(Columns(), false);

            // Search each column Bottom-to-Top
            SetVisible(Columns(), true);

            void SetVisible(IEnumerable<IEnumerable<Tree>> outer, bool reverseInner)
            {
                foreach (var inner in outer)
                {
                    var tallestSoFar = -1;
                    var i = reverseInner ? inner.Reverse() : inner;
                    foreach (var tree in i)
                    {
                        if (tallestSoFar < tree.Height)
                        {
                            tallestSoFar = tree.Height;
                            visibility!.SetVisible(tree);
                            if (tree.Height == MAX_HEIGHT)
                                break; //We hit max height, no sense continuing to iterate
                        }
                    }
                }
            }

            return visibility;
        }

        public ForestScenicMatrix CalculateScenicScores()
        {
            // Create Scenic Matrix
            var rows = Trees.Count;
            var columns = Trees.First().Length;
            var scenic = new ForestScenicMatrix(rows, columns);

            foreach (int row in Enumerable.Range(0, rows))
            {
                foreach (int column in Enumerable.Range(0, columns))
                {
                    scenic.SetScore(row, column, CalculateScenicScore(row, column));
                }
            }

            return scenic;

            int CalculateScenicScore(int row, int column)
            {
                if (row == 0 || column == 0 || row == Rows().Count() || column == Columns().Count())
                    return 0; //No visibility to one side, so 0 total score

                var tree = Trees[row][column];

                var treeRow = Rows().ElementAt(row);
                var right = TreesSeen(treeRow.ElementsIn((column + 1)..));
                var left = TreesSeen(treeRow.ElementsIn(..(column)).Reverse());

                var treeColumn = Columns().ElementAt(column);
                var down = TreesSeen(treeColumn.ElementsIn((row + 1)..));
                var up = TreesSeen(treeColumn.ElementsIn(..(row)).Reverse());

                return right * left * up * down;

                int TreesSeen(IEnumerable<Tree> axis)
                {
                    var s = 0;
                    foreach (var i in axis)
                    {
                        s++;
                        if (i.Height >= tree!.Height)
                            break;
                    }
                    return s;
                }

            }
        }
    }

    public class ForestVisibility
    {
        HashSet<Tree> visible;
        public ForestVisibility()
        {
            visible = new HashSet<Tree>();
        }

        public void SetVisible(Tree t)
        {
            visible.Add(t);
        }

        public int VisibleCount()
        {
            return visible.Count;
        }
    }

    public class ForestScenicMatrix
    {
        int[,] scenicMatrix;
        public ForestScenicMatrix(int rows, int columns)
        {
            scenicMatrix = new int[rows, columns];
        }

        public void SetScore(int row, int column, int score)
        {
            scenicMatrix[row, column] = score;
        }

        public int BestScore()
        {
            int best = 0;
            foreach (var score in scenicMatrix)
            {
                if (score > best)
                    best = score;
            }
            return best;
        }
    }
}

