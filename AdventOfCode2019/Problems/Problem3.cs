namespace AdventOfCode2019.Problems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    internal class Problem3 : Problem
    {
        public Problem3() : base(3)
        {
        }

        public override string Answer()
        {
            var grid = new Grid();

            grid.ExecuteInstructions(Input[0].Split(','), Input[1].Split(','));

            return $"Smallest manhattan distance to intersection: {grid.FindClosestIntersection()}\nSmallest actual distance to intersection: {grid.FindIntersectionWithFewestSteps()}";
        }
    }

    internal class Location
    {
        public int WireOneDistance { get; set; }
        public int WireTwoDistance { get; set; }

        public int X { get; }
        public int Y { get; }

        public int Distance => WireOneDistance + WireTwoDistance;

        public bool IsIntersection => WireOneDistance != int.MaxValue && WireTwoDistance != int.MaxValue;

        public Location(int x, int y)
        {
            X = x;
            Y = y;

            WireOneDistance = int.MaxValue;
            WireTwoDistance = int.MaxValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is Location other)
            {
                return X == other.X && Y == other.Y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
    }

    internal class Grid
    {
        private readonly Coordinate[] _wires;
        private readonly HashSet<Location> _visitedLocations;

        private int[] _wireDistances;
        
        internal Grid()
        {
            _wires = new []{ new Coordinate(), new Coordinate() };
            _wireDistances = new[] {0, 0};

            _visitedLocations = new HashSet<Location>();
        }

        internal void ExecuteInstructions(string[] firstWire, string[] secondWire)
        {
            foreach (var instruction in firstWire)
            {
                FollowInstruction(0, instruction);
            }

            foreach (var instruction in secondWire)
            {
                FollowInstruction(1, instruction);
            }
        }

        private void FollowInstruction(int wire, string instruction)
        {
            var direction = instruction[0];
            var distance = int.Parse(instruction.Substring(1));

            switch (direction)
            {
                case 'R':
                    for (var n = 0; n < distance; n++)
                    {
                        _wires[wire].X++;
                        _wireDistances[wire]++;
                        AddCoordinateToVisitedCoordinates(_wires[wire], wire);
                    }

                    break;
                case 'L':
                    for (var n = 0; n < distance; n++)
                    {
                        _wires[wire].X--;
                        _wireDistances[wire]++;
                        AddCoordinateToVisitedCoordinates(_wires[wire], wire);
                    }

                    break;
                case 'U':
                    for (var n = 0; n < distance; n++)
                    {
                        _wires[wire].Y--;
                        _wireDistances[wire]++;
                        AddCoordinateToVisitedCoordinates(_wires[wire], wire);
                    }

                    break;
                case 'D':
                    for (var n = 0; n < distance; n++)
                    {
                        _wires[wire].Y++;
                        _wireDistances[wire]++;
                        AddCoordinateToVisitedCoordinates(_wires[wire], wire);
                    }

                    break;
                default:
                    throw new ArgumentException($"Unknown direction \"{direction}\".");
            }
        }

        internal int FindClosestIntersection()
        {
            var distancesToIntersections = new List<int>();

            foreach (var intersection in _visitedLocations.Where(l => l.IsIntersection))
            {
                distancesToIntersections.Add(Coordinate.ManhattanDistance(new Coordinate(), new Coordinate(intersection.X, intersection.Y)));
            }

            return distancesToIntersections.Min();
        }

        internal int FindIntersectionWithFewestSteps()
        {
            return _visitedLocations.Where(l => l.IsIntersection).Select(d => d.Distance).Min();
        }

        private void AddCoordinateToVisitedCoordinates(Coordinate coordinate, int wire)
        {
            var location = new Location(coordinate.X, coordinate.Y);

            if (_visitedLocations.Contains(location))
            {
                _visitedLocations.TryGetValue(location, out var existing);
                if (wire == 0)
                {
                    existing.WireOneDistance = _wireDistances[wire];
                }
                else
                {
                    existing.WireTwoDistance = _wireDistances[wire];
                }
            }
            else
            {
                if (wire == 0)
                {
                    location.WireOneDistance = _wireDistances[wire];
                }
                else
                {
                    location.WireTwoDistance = _wireDistances[wire];
                }

                _visitedLocations.Add(location);
            }
        }
    }
}