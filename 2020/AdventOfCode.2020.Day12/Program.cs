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
            var instructions = File.ReadAllLines("input.txt")
                .Select(Instruction.Parse)
                .ToArray();

            {
                var boat = new Boat(new Coords(0, 0), Coords.East);

                foreach (var instruction in instructions)
                {
                    instruction.ExecuteV1(boat);
                }

                var result = Math.Abs(boat.Coords.X) + Math.Abs(boat.Coords.Y);
                Console.WriteLine($"Part 1: Distance travelled is {result}");
            }

            {
                var boat = new Boat(new Coords(0, 0), new Coords(10, -1));

                foreach (var instruction in instructions)
                {
                    instruction.ExecuteV2(boat);
                }

                var result = Math.Abs(boat.Coords.X) + Math.Abs(boat.Coords.Y);
                Console.WriteLine($"Part 2: Distance travelled is {result}");
            }
        }
    }
}