using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var expressions = File.ReadAllLines("input.txt")
                .Select(Expression.Parse)
                .ToArray();

            {
                // Part 1
                var result = 0L;
                foreach (var expression in expressions)
                {
                    result += expression.Evaluate();
                }

                Console.WriteLine($"Part 1: Sum of all expressions is {result}");
            }

            {
                // Part 2
                var result = 0L;
                foreach (var expression in expressions)
                {
                    result += expression.EvaluateV2();
                }

                Console.WriteLine($"Part 2: Sum of all expressions is {result}");
            }
        }
    }
}