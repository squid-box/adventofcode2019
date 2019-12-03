namespace AdventOfCode2019.Utils
{
	using System;
    using System.Collections;

    public class Coordinate : IComparable
    {
        public int X { get; set; }
        
        public int Y { get; set; }

        public Coordinate(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public override bool Equals(object obj)
        {
	        if (obj is Coordinate otherCoordinate)
	        {
		        return otherCoordinate.X.Equals(X) && otherCoordinate.Y.Equals(Y);
			}

			return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj is Coordinate other)
            {
                if (other.Equals(this))
                {
                    return 0;
                }
            }

            return -1;
        }

        public static int ManhattanDistance(Coordinate origin, Coordinate destination)
        {
            if (origin.Equals(destination))
            {
                return 0;
            }

            return Math.Abs(destination.X - origin.X) + Math.Abs(destination.Y - origin.Y);
        }
    }
}
