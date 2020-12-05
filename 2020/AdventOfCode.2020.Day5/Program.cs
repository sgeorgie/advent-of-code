using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt")
                .Select(s => s.ToArray())
                .ToArray();

            // Part 1
            var highestSeat = 0;

            foreach (var seat in input)
            {
                var seatNumber = FindSeatNumber(8, 0, 127, 7, 0, seat);
                if (seatNumber > highestSeat)
                    highestSeat = seatNumber;
            }

            Console.WriteLine($"Part 1: Hightest seat number is {highestSeat}");

            // Part 2
            var allSeats = new HashSet<int>(8 * 128);

            for (int i = 0; i < 8 * 128; i++)
            {
                allSeats.Add(i);
            }

            foreach (var seat in input)
            {
                var seatNumber = FindSeatNumber(8, 0, 127, 7, 0, seat);
                allSeats.Remove(seatNumber);
            }

            foreach (var seat in allSeats)
            {
                // Check if the seat is in the middle of the plane
                if (!allSeats.Contains(seat + 1) && !allSeats.Contains(seat - 1))
                {
                    Console.WriteLine($"Found my seat {seat}");
                    break;
                }
            }
        }

        static int FindSeatNumber(int columnCount, int topRow, int bottomRow, int rightMostColumn, int leftMostColumn, char[] directions)
        {
            if (directions.Length == 0)
            {
                // Found the seat
                return topRow * columnCount + rightMostColumn;
            }

            var direction = directions.First();

            switch (direction)
            {
                case 'F':
                    var nextBottom = (bottomRow + topRow) / 2;
                    return FindSeatNumber(columnCount, topRow, nextBottom, rightMostColumn, leftMostColumn, directions[1..]);
                case 'B':
                    var nextTop = (bottomRow + topRow) / 2 + 1;
                    return FindSeatNumber(columnCount, nextTop, bottomRow, rightMostColumn, leftMostColumn, directions[1..]);
                case 'L':
                    var nextRight = (rightMostColumn + leftMostColumn) / 2;
                    return FindSeatNumber(columnCount, topRow, bottomRow, nextRight, leftMostColumn, directions[1..]);
                case 'R':
                    var nextLeft = (rightMostColumn + leftMostColumn) / 2 + 1;
                    return FindSeatNumber(columnCount, topRow, bottomRow, rightMostColumn, nextLeft, directions[1..]);
                default:
                    throw new Exception("Got invalid directions");
            }
        }
    }
}