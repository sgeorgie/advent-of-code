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
                .OrderBy(value => value)
                .ToList();

            // Insert the outlet and the device
            input.Insert(0, 0);
            input.Add(input[^1] + 3);

            // Part 1
            var oneJoltDiffs = 0;
            var threeJoltDiffs = 0;

            for (int i = 0; i < input.Count - 1; i++)
            {
                var diff = input[i + 1] - input[i];

                if (diff == 1)
                    oneJoltDiffs++;
                else if (diff == 3)
                    threeJoltDiffs++;
            }

            Console.WriteLine($"Part 1: 1 jolt diffs times 3 jolt diffs is {oneJoltDiffs * threeJoltDiffs}");

            // Part 2
            var paths = new Dictionary<int, long>
            {
                [0] = 1,
            };

            for (int i = 1; i < input.Count; i++)
            {
                var value = 0L;

                for (int j = i - 1; j >= i - 3; j--)
                {
                    if (j < 0 || input[i] - input[j] > 3)
                        break;

                    value += paths[j];
                }

                paths[i] = value;
            }

            Console.WriteLine($"Part 2: Found {paths[input.Count - 1]} possible combinations");
        }
    }
}