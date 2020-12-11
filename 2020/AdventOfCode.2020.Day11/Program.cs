using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        private static int[][] _directions = new[]
        {
            new []{1, 1}, new []{0, 1}, new []{-1, 1}, new []{-1, 0}, new []{-1, -1}, new []{0, -1}, new []{1, -1}, new []{1, 0}
        };

        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt").ToArray();

            var seats = new char[input[0].Length, input.Length];

            for (int i = 0; i < input[0].Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    seats[i, j] = input[j][i];
                }
            }

            Console.WriteLine($"Part 1: {CountOccupiableSeats(AdjacentSeatsCounterV1, seats, 4)} Occupied seats");

            Console.WriteLine($"Part 2: {CountOccupiableSeats(AdjacentSeatsCounterV2, seats, 5)} occupied seats");
        }

        private static int CountOccupiableSeats(Func<char[,], int, int, int> adjacentSeatsCounter, char[,] seats, int adjacencyThreshold)
        {
            var result = new char[seats.GetLength(0), seats.GetLength(1)];

            var modified = 0;

            for (int x = 0; x < seats.GetLength(0); x++)
            {
                for (int y = 0; y < seats.GetLength(1); y++)
                {
                    var seat = seats[x, y];
                    result[x, y] = seat;

                    if (seat == '.')
                        continue;

                    var occupied = adjacentSeatsCounter(seats, x, y);

                    if (seat == 'L' && occupied == 0)
                    {
                        result[x, y] = '#';
                        modified++;
                    }

                    else if (seat == '#' && occupied >= adjacencyThreshold)
                    {
                        result[x, y] = 'L';
                        modified++;
                    }
                }
            }

            if (modified > 0)
                return CountOccupiableSeats(adjacentSeatsCounter, result, adjacencyThreshold);

            var occupiable = 0;

            foreach (var seat in result)
            {
                if (seat == '#')
                    occupiable++;
            }

            return occupiable;
        }

        private static int AdjacentSeatsCounterV1(char[,] seats, int x, int y)
        {
            var maxX = seats.GetLength(0) - 1;
            var maxY = seats.GetLength(1) - 1;

            var occupied = 0;

            foreach (var direction in _directions)
            {
                var adjX = x + direction[0];
                var adjY = y + direction[1];

                if (adjX >= 0 && adjX <= maxX && adjY >= 0 && adjY <= maxY)
                {
                    if (seats[adjX, adjY] == '#')
                        occupied++;
                }
            }

            return occupied;
        }

        private static int AdjacentSeatsCounterV2(char[,] seats, int x, int y)
        {
            var maxX = seats.GetLength(0) - 1;
            var maxY = seats.GetLength(1) - 1;

            var occupied = 0;

            foreach (var direction in _directions)
            {
                var adjX = x + direction[0];
                var adjY = y + direction[1];

                while (adjX >= 0 && adjX <= maxX && adjY >= 0 && adjY <= maxY)
                {
                    var adjacentSeat = seats[adjX, adjY];

                    if (adjacentSeat == '#')
                        occupied++;

                    if (adjacentSeat != '.')
                        break;

                    adjX += direction[0];
                    adjY += direction[1];

                }
            }

            return occupied;
        }
    }
}