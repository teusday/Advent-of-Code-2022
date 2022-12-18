namespace Advent_of_Code_2022.Day3
{
    public class ElfGroup
    {
        public ElfGroup(IEnumerable<Rucksack> group)
        {
            if (group.Count() != 3)
                throw new Exception("Groups must be groups of 3");
            Elf1 = group.ElementAt(0);
            Elf2 = group.ElementAt(1);
            Elf3 = group.ElementAt(2);
        }

        public Rucksack Elf1 { get; set; }
        public Rucksack Elf2 { get; set; }
        public Rucksack Elf3 { get; set; }

        public char Badge
        {
            get
            {
                var intersection = Elf1.BothCompartments.ToCharArray()
                    .Intersect(Elf2.BothCompartments.ToCharArray())
                    .Intersect(Elf3.BothCompartments.ToCharArray());

                if (intersection.Count() != 1)
                    throw new AoCException("Can't find badge for group");

                return intersection.First();
            }
        }
    }
}

