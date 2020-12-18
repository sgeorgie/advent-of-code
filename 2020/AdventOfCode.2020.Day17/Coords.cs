using System.Collections.Generic;

namespace AdventOfCode
{
    record Coords
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Coords(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public IEnumerable<Coords> GetNeighbors()
        {
            var neighbors = new HashSet<Coords>();

            for (int x = X -1; x <= X + 1; x++)
            {
                for (int y = Y -1; y <= Y + 1; y++)
                {
                    for (int z = Z -1; z <= Z + 1; z++)
                    {
                        if (!(x == X && y == Y && z == Z))
                            neighbors.Add(new Coords(x, y, z));
                    }
                }
            }

            return neighbors;
        }
    }
}