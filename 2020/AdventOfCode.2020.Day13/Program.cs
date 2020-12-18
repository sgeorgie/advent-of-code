using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");


            {
                    // Part 1
                var departureTime = int.Parse(input[0]);

                var buses = input[1]
                    .Split(',')
                    .Where(b => b != "x")
                    .Select(int.Parse)
                    .ToArray();

                var earliestBus = buses[0];
                var bestDelay = -1;
                foreach (var bus in buses)
                {
                    var delay = departureTime % bus;
                    if (delay == 0)
                    {
                        earliestBus = bus;
                        bestDelay = 0;
                        break;
                    }

                    delay = bus - delay;

                    if (delay < bestDelay || bestDelay == -1)
                    {
                        earliestBus = bus;
                        bestDelay = delay;
                    }
                }

                Console.WriteLine($"Part 1: Earliest bus times delay is {earliestBus * bestDelay}");
            }

            {
                // Part 2
                var busDefinitions = input[1]
                    .Split(',')
                    .ToList();

                var busIndex = busDefinitions
                    .Where(d => d != "x")
                    .ToDictionary(int.Parse, bus => busDefinitions.IndexOf(bus));

                var buses = busDefinitions
                    .Where(d => d != "x")
                    .Select(int.Parse)
                    .ToList();

                long multiplier = buses[0];
                long currentIndex = 0;

                buses.RemoveAt(0);

                while (buses.Any())
                {
                    currentIndex += multiplier;

                    // Check which of the remaining numbers align
                    foreach (var bus in buses.ToArray())
                    {
                        var index = busIndex[bus];
                        if ((currentIndex + index) % bus == 0)
                        {
                            buses.Remove(bus);
                            multiplier *= bus;
                        }
                    }
                }

                Console.WriteLine($"Part2: Earliest timestamp with synced buses is {currentIndex}");
            }
        }
    }
}