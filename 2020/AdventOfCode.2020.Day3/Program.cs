using System;
using System.IO;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\input.txt");

            var grid = new char[input[0].Length, input.Length];

            // Convert input to 2-dimensional array, must flip the dimensions because an array of lines has flipped dimensions
            for (int i = 0; i < input[0].Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    grid[i, j] = input[j][i];
                }
            }

            var part1Result = GetTraversedTrees(grid, 3, 1);

            Console.WriteLine($"Trees traversed in part 1: {part1Result}");

            var testSlopes = new[]
            {
                new[] {1, 1},
                new[] {3, 1},
                new[] {5, 1},
                new[] {7, 1},
                new[] {1, 2},
            };

            var part2Result = 1;

            foreach (var slope in testSlopes)
            {
                part2Result *= GetTraversedTrees(grid, slope[0], slope[1]);
            }

            Console.WriteLine($"Part 2 result: {part2Result}");
        }

        /// <summary>
        /// A recursive solution
        /// </summary>
        static int GetTraversedTreesRecursively(char[,] grid, int right, int down, int currentX = 0, int currentY = 0)
        {
            var nextX = currentX + right;
            var nextY = currentY + down;

            if (nextY >= grid.GetLength(1))
            {
                return 0;
            }

            var normalizedX = nextX % grid.GetLength(0);

            var encounteredTrees = 0;

            if (grid[normalizedX, nextY] == '#')
            {
                encounteredTrees++;
            }

            return encounteredTrees + GetTraversedTreesRecursively(grid, right, down, nextX, nextY);
        }

        /// <summary>
        /// A solution using a while loop
        /// </summary>
        static int GetTraversedTrees(char[,] grid, int right, int down)
        {
            var x = 0;
            var y = 0;

            var encounteredTrees = 0;

            while (y < grid.GetLength(1))
            {
                // The array repeats indefinitely to the right, the modulus operator (%) to the rescue with all repetitive patterns
                var normalizedX = x % grid.GetLength(0);

                if (grid[normalizedX, y] == '#')
                {
                    encounteredTrees++;
                }

                x += right;
                y += down;
            }

            return encounteredTrees;
        }
    }
}