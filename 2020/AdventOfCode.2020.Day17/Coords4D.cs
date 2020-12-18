using System.Collections.Generic;

namespace AdventOfCode
{
    record Coords4D
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int W { get; }

        public Coords4D(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public IEnumerable<Coords4D> GetNeighbors()
        {
            var neighbors = new HashSet<Coords4D>();

            for (int x = X -1; x <= X + 1; x++)
            {
                for (int y = Y -1; y <= Y + 1; y++)
                {
                    for (int z = Z -1; z <= Z + 1; z++)
                    {
                        for (int w = W - 1; w <= W + 1; w++)
                        {
                            if (!(x == X && y == Y && z == Z && w == W))
                                neighbors.Add(new Coords4D(x, y, z, w));
                        }
                    }
                }
            }

            return neighbors;
        }
    }
}