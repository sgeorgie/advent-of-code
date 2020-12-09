using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode._2020.Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt")
                .Select(long.Parse)
                .ToArray();

            const int preamble = 25;

            var key = FindKey(input, preamble);

            Console.WriteLine($"Part 1: The key is {key}");

            Console.WriteLine($"Part 2: The weakness is {FindWeakness(input, key)}");
        }

        static long FindKey(long[] range, int preamble)
        {
            var sums = new Dictionary<int, HashSet<long>>();

            // Fill data from the preamble
            for (int i = 0; i < preamble; i++)
            {
                var indexSums = new HashSet<long>();
                sums.Add(i, indexSums);

                for (int j = 0; j < i; j++)
                {
                    indexSums.Add(range[i] + range[j]);
                }
            }

            for (int i = preamble; i < range.Length; i++)
            {
                if (!sums.Any(kv => kv.Value.Contains(range[i])))
                {
                    return range[i];
                }

                sums.Remove(i - preamble);

                var indexSums = new HashSet<long>();
                sums.Add(i, indexSums);

                for (int j = 1; j < preamble; j++)
                {
                    indexSums.Add(range[i] + range[i - j]);
                }
            }

            throw new Exception("Couldn't find the key");
        }

        static long FindWeakness(long[] range, long key)
        {
            var i = 0;
            var j = 1;

            var sum = range[i] + range[j];

            while (sum != key)
            {
                if (sum < key)
                {
                    sum += range[++j];
                }
                else if (j - 1 == 1)
                {
                    sum += range[++j] - range[i++];
                }
                else
                {
                    sum -= range[i++];
                }
            }

            var resultRange = range[i..j];

            return resultRange.Max() + resultRange.Min();
        }
    }
}