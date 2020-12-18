using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var memRegex = new Regex(@"^mem\[(\d+)\] = (\d+)$");

            var input = File.ReadAllLines("input.txt");

            {
                // Part 1
                Dictionary<int, long> memory = new Dictionary<int, long>();

                string mask = null;

                foreach (var line in input)
                {
                    if (line.StartsWith("mask"))
                    {
                        mask = line.Substring(7);
                    }
                    else
                    {
                        var match = memRegex.Match(line);

                        var address = int.Parse(match.Groups[1].Value);
                        var value = long.Parse(match.Groups[2].Value);

                        var maskedValue = MaskValue(mask, value);

                        memory[address] = maskedValue;
                    }
                }

                Console.WriteLine($"Part 1: {memory.Values.Sum()}");
            }

            {
                // Part 2
                Dictionary<long, long> memory = new Dictionary<long, long>();
                string mask = null;

                foreach (var line in input)
                {
                    if (line.StartsWith("mask"))
                    {
                        mask = line.Substring(7);
                    }
                    else
                    {
                        var match = memRegex.Match(line);

                        var address = long.Parse(match.Groups[1].Value);
                        var value = long.Parse(match.Groups[2].Value);

                        var addresses = GetMaskedAddressPermutations(mask, address);

                        foreach (var maskedAddress in addresses)
                        {
                            memory[maskedAddress] = value;
                        }
                    }
                }

                Console.WriteLine($"Part 2: {memory.Values.Sum()}");
            }
        }

        static long MaskValue(string mask, long value)
        {
            var binaryValue = Convert.ToString(value, 2)
                .PadLeft(mask.Length, '0')
                .ToCharArray();

            for (int i = 0; i < mask.Length; i++)
            {
                var maskBit = mask[i];

                if (maskBit == 'X')
                    continue;

                binaryValue[i] = maskBit == '1' ? '1' : '0';
            }

            long returnValue = 0;

            var reversed = binaryValue.Reverse().ToArray();

            for (int i = 0; i < binaryValue.Length; i++)
            {
                if (reversed[i] == '1')
                {
                    returnValue += Convert.ToInt64(Math.Pow(2, i));
                }
            }

            return returnValue;
        }

        static long[] GetMaskedAddressPermutations(string mask, long value)
        {
            var addresses = new List<long>();

            var Xs = new List<int>();

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X')
                    Xs.Add(i);
            }

            var result = Convert.ToString(value, 2)
                .PadLeft(mask.Length, '0')
                .ToCharArray();

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '1')
                    result[i] = '1';
            }

            for (int i = 0; i < Math.Pow(2, Xs.Count); i++)
            {
                var binary = Convert.ToString(i, 2).PadLeft(Xs.Count, '0');

                for (int j = 0; j < Xs.Count; j++)
                {
                    result[Xs[j]] = binary[j];
                }

                var address = 0L;
                var reversed = result.ToArray().Reverse().ToArray();

                for (int k = 0; k < result.Length; k++)
                {
                    if (reversed[k] == '1')
                    {
                        address += Convert.ToInt64(Math.Pow(2, k));
                    }
                }

                addresses.Add(address);
            }

            return addresses.ToArray();
        }
    }
}