using System;

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
                        }
                    }
                }
            }

            return visibility;
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
}

