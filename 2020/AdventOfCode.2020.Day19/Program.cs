using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").ToList();
            var separatorIndex = input.IndexOf(string.Empty);

            foreach (var rule in input.ToArray()[0..separatorIndex])
                Rule.Parse(rule);

            var messages = input.ToArray()[(separatorIndex + 1)..];
            var ruleZero = Rule.Registry[0];

            Console.WriteLine($"Part 1: Found {messages.Count(m => ruleZero.IsMatch(m))} matching messages");

            Rule.Parse("8: 42 | 42 8");
            Rule.Parse("11: 42 31 | 42 11 31");

            Console.WriteLine($"Part 2: Found {messages.Count(m => ruleZero.IsMatch(m))} matching messages after updating the rules");
        }
    }
}