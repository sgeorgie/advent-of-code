using System;

namespace AdventOfCode
{
    public struct Coords
    {
        public static readonly Coords East = new Coords(1, 0);
        public static readonly Coords South = new Coords(0, 1);
        public static readonly Coords West = new Coords(-1, 0);
        public static readonly Coords North = new Coords(0, -1);

        public int X { get; }

        public int Y { get; }

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coords Translate(Coords waypoint, int distance)
        {
            return new Coords(X + waypoint.X * distance, Y + waypoint.Y * distance);
        }

        public Coords Rotate(char direction, int degrees)
        {
            var quarterTurns = (degrees / 90) % 4;

            if (direction == 'L')
                quarterTurns = (4 - quarterTurns) % 4;

            var x = X;
            var y = Y;

            for (int i = 0; i < quarterTurns; i++)
            {
                (x, y) = (-y, x);
            }

            return new Coords(x, y);
        }

        public static Coords ParseDirection(char direction)
        {
            return direction switch
            {
                'N' => Coords.North,
                'E' => Coords.East,
                'S' => Coords.South,
                'W' => Coords.West,
                _ => throw new Exception(),
            };
        }
    }
}