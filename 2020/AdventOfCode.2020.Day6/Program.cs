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
            var groups = File.ReadAllText("input.txt")
                .Split("\n\n")
                .Select(group => group
                    .Split("\n")
                    .Select(person => person.ToCharArray())
                    .ToArray())
                .ToArray();

            // Part 1
            var totalAnswers = 0;

            foreach (var group in groups)
            {
                totalAnswers += group
                    .SelectMany(person => person)
                    .ToHashSet()
                    .Count;
            }

            Console.WriteLine($"Part 1: Sum of unique yes answers in all groups is {totalAnswers}");

            // Part 2
            var totalCommonAnswers = 0;

            foreach (var group in groups)
            {
                IEnumerable<char> commonAnswers = group.First();

                for (int i = 1; i < group.Length; i++)
                {
                    var person = group[i];
                    commonAnswers = commonAnswers.Intersect(person);
                }

                totalCommonAnswers += commonAnswers.Count();
            }

            Console.WriteLine($"Part2: Sum of common answers in all groups is {totalCommonAnswers}");
        }
    }
}