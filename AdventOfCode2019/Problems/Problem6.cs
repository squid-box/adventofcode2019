namespace AdventOfCode2019.Problems
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Problem6 : Problem
    {
        public Problem6() : base(6)
        {
        }

        internal static Dictionary<string, (string Direct, List<string> Indirect)> ParseInput(string[] input)
        {
            var result = new Dictionary<string, (string Direct, List<string> Indirect)>();
            
            foreach (var line in input)
            {
                var split = line.Split(')');
                var center = split[0];
                var orbiter = split[1];

                result.Add(orbiter, (center, new List<string>()));
            }

            return result;
        }

        internal static (Dictionary<string, (string Direct, List<string> Indirect)> Orbits, List<string> List) CollectIndirectOrbits(Dictionary<string, (string Direct, List<string> Indirect)> orbits, string orbiter)
        {
            var list = new List<string>();
            var set = orbits[orbiter].Indirect;
            
            for (var orb = orbiter; orbits.ContainsKey(orb); orb = orbits[orb].Direct)
            {
                set.Add(orb);
                list.Add(orb);
            }
            
            return (orbits, list);
        }

        public override string Answer()
        {
            var orbits = ParseInput(Input);

            foreach (var orbiter in orbits.Keys)
            {
                CollectIndirectOrbits(orbits, orbiter);
            }

            var partOne = orbits.Sum(x => x.Value.Indirect.Count).ToString();

            var santa = CollectIndirectOrbits(orbits, "SAN").List;
            var me = CollectIndirectOrbits(orbits, "YOU").List;
            var both = new List<string>(me.Concat(santa));

            var partTwo = (both.Count - (both.Count - new HashSet<string>(both).Count + 1) * 2).ToString();

            return $"Part 1: {partOne}\nPart 2: {partTwo}";
        }
    }
}