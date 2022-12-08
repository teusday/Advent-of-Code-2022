using System;
namespace Advent_of_Code_2022.Day7
{
    //Extra Large File System
    public class ELFS
    {
        public ELFSDir Root { get; set; }
        public ELFSDir WorkingDirectory { get; set; }
        public ELFS()
        {
            Root = new ELFSDir("/", null);
            WorkingDirectory = Root;
        }

        public void ChangeDirectory(string directory)
        {
            if (directory == "/")
                WorkingDirectory = Root;
            else if (directory == "..")
                WorkingDirectory = WorkingDirectory.ContainingDir ?? throw new AoCException("Cannot 'cd ..' from root");
            else
            {
                var destDir = WorkingDirectory.Children.Where(i => i is ELFSDir d && d.Name == directory).FirstOrDefault();
                if (destDir is not ELFSDir d)
                    throw new AoCException($"Cannot navigate to '{directory}' from '{WorkingDirectory.Name}'");
                WorkingDirectory = d;
            }
        }

        public void AddFile(string name, int size)
        {
            WorkingDirectory.Children.Add(new ELFSFile(name, size, WorkingDirectory));
        }

        public void AddDir(string name)
        {
            WorkingDirectory.Children.Add(new ELFSDir(name, WorkingDirectory));
        }

        public IEnumerable<ELFSDir> AllDirectories()
        {
            return AllDirectories(Root);
        }

        private IEnumerable<ELFSDir> AllDirectories(ELFSDir node)
        {
            var directoryChildren = node.Children
                .Where(e => e is ELFSDir)
                .Select(e => (ELFSDir)e);
            return directoryChildren.SelectMany(c => AllDirectories(c)).Append(node);
        }
    }

    public interface IELFSEntity
    {
        public ELFSDir? ContainingDir { get; set; }
        int GetSize();
    }

    public class ELFSDir : IELFSEntity
    {
        public ELFSDir(string name, ELFSDir? containingDir)
        {
            ContainingDir = containingDir;
            Name = name;
            Children = new List<IELFSEntity>();
        }

        public ELFSDir? ContainingDir { get; set; }
        public string Name { get; set; }
        public List<IELFSEntity> Children { get; set; }

        public int GetSize()
        {
            return Children.Sum(c => c.GetSize());
        }
    }

    public class ELFSFile : IELFSEntity
    {
        public ELFSFile(string name, int size, ELFSDir containingDir)
        {
            ContainingDir = containingDir;
            Name = name;
            Size = size;
        }

        public ELFSDir? ContainingDir { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }

        public int GetSize()
        {
            return Size;
        }
    }
}

