using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var startingNumbers = new int[] {14, 8, 16, 0, 1, 17};

            Console.WriteLine($"Part 1: Value at turn 2020 is {CalculateValueAtTurn(startingNumbers, 2020)}");

            Console.WriteLine($"Part 1: Value at turn 30M is {CalculateValueAtTurn(startingNumbers, 30000000)}");
        }

        static int CalculateValueAtTurn(int[] startingNumbers, int turns)
        {
            var memory = new Dictionary<int, int>();

            for (int i = 1; i < startingNumbers.Length; i++)
            {
                memory[startingNumbers[i - 1]] = i;
            }

            var turn = startingNumbers.Length + 1;
            var latest = startingNumbers[^1];

            while (turn <= turns)
            {
                // Find out when the number was last spoken
                var newLatest = memory.ContainsKey(latest) ? turn - memory[latest] - 1 : 0;

                // Add the latest to the memory
                memory[latest] = turn - 1;

                // Set the new latest
                latest = newLatest;

                turn++;
            }

            return latest;
        }
    }
}