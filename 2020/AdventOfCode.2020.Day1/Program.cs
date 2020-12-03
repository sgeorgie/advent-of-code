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
                .Select(int.Parse)
                .ToArray();

            SolvePartOne(input, 2020);
            SolvePartTwo(input, 2020);
        }

        static void SolvePartOne(int[] input, int target)
        {
            var set = new HashSet<int>(input);

            foreach (var firstValue in input)
            {
                var secondValue = target - firstValue;

                if (set.Contains(secondValue))
                {
                    Console.WriteLine($"The two numbers adding up to {target} are {firstValue} and {secondValue}. Their product is {firstValue * secondValue}");
                    return;
                }
            }
        }

        static void SolvePartTwo(int[] input, int target)
        {
            var set = new HashSet<int>(input);

            for (int i = 0; i < input.Length; i++)
            {
                // No need to start from 0, we know that no value there is part of the valid triplet
                for (int j = i + 1; j < input.Length; j++)
                {
                    var firstValue = input[i];
                    var secondValue = input[j];
                    var thirdValue = target - firstValue - secondValue;

                    if (set.Contains(thirdValue))
                    {
                        Console.WriteLine($"The three numbers adding up to {target} are {firstValue}, {secondValue}, and {thirdValue}. Their product is {firstValue * secondValue * thirdValue}");
                        return;
                    }
                }
            }
        }
    }
}