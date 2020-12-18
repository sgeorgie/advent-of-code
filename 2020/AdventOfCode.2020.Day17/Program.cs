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
            var input = File.ReadAllLines("input.txt");

            {
                var space = new Dictionary<Coords, bool>();

                for (int x = 0; x < input.Length; x++)
                {
                    for (int y = 0; y < input[0].Length; y++)
                    {
                        space[new Coords(x, y, 0)] = input[x][y] == '#';
                    }
                }

                for (int cycle = 0; cycle < 6; cycle++)
                {
                    var newSpace = new Dictionary<Coords, bool>(space);

                    // Expand the space
                    foreach (var (coords, state) in space)
                    {
                        var neighbors = coords.GetNeighbors();

                        if (state)
                        {
                            // Make sure all neighbors are added
                            foreach (var neighbor in neighbors)
                            {
                                if (!newSpace.ContainsKey(neighbor))
                                    newSpace[neighbor] = false;
                            }
                        }
                    }

                    space = new Dictionary<Coords, bool>(newSpace);

                    foreach (var (coords, state) in space)
                    {
                        var activeNeighbors = coords.GetNeighbors()
                            .Count(c => space.ContainsKey(c) && space[c]);

                        if (state && (activeNeighbors < 2 || activeNeighbors > 3))
                        {
                            newSpace[coords] = false;
                        }
                        else if ((!state) && activeNeighbors == 3)
                        {
                            newSpace[coords] = true;
                        }
                    }

                    space = newSpace;
                }

                Console.WriteLine($"Part 1: Active cubes in space after 6 turns is {space.Count(kv => kv.Value)}");
            }

            {
                // Part 2
                var space = new Dictionary<Coords4D, bool>();

                for (int x = 0; x < input.Length; x++)
                {
                    for (int y = 0; y < input[0].Length; y++)
                    {
                        space[new Coords4D(x, y, 0, 0)] = input[x][y] == '#';
                    }
                }

                for (int cycle = 0; cycle < 6; cycle++)
                {
                    var newSpace = new Dictionary<Coords4D, bool>(space);

                    // Expand the space
                    foreach (var (coords, state) in space)
                    {
                        var neighbors = coords.GetNeighbors();

                        if (state)
                        {
                            // Make sure all neighbors are added
                            foreach (var neighbor in neighbors)
                            {
                                if (!newSpace.ContainsKey(neighbor))
                                    newSpace[neighbor] = false;
                            }
                        }
                    }

                    space = new Dictionary<Coords4D, bool>(newSpace);

                    foreach (var (coords, state) in space)
                    {
                        var activeNeighbors = coords.GetNeighbors()
                            .Count(c => space.ContainsKey(c) && space[c]);

                        if (state && (activeNeighbors < 2 || activeNeighbors > 3))
                        {
                            newSpace[coords] = false;
                        }
                        else if ((!state) && activeNeighbors == 3)
                        {
                            newSpace[coords] = true;
                        }
                    }

                    space = newSpace;
                }

                Console.WriteLine($"Part 2: Active cutes in space after 6 turns is {space.Count(kv => kv.Value)}");
            }
        }
    }
}