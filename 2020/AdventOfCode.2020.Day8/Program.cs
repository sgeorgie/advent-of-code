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
            var instructionParser = new Regex(@"^(\w{3}) ([-\+]\d+)$");
            var instructions = File.ReadAllLines("input.txt")
                .Select(instruction => instructionParser.Match(instruction))
                .Select(match => new Instruction(match.Groups[1].Value, int.Parse(match.Groups[2].Value)))
                .ToArray();

            // Part 1
            TryExecuteInstructions(instructions, out var accBeforeLoopCutOff);

            Console.WriteLine($"Part 1: Accumulated {accBeforeLoopCutOff} before hitting an infinite loop.");

            // Part 2
            for (int i = 0; i < instructions.Length; i++)
            {
                var originalInstruction = instructions[i];

                if (originalInstruction.Operation != "acc")
                {
                    var original = instructions[i];
                    instructions[i] = originalInstruction with { Operation = originalInstruction.Operation == "jmp" ? "nop" : "jmp" };

                    if (TryExecuteInstructions(instructions, out var acc))
                    {
                        Console.WriteLine($"Part 2: Accumulated {acc} after fixing the infinite loop.");
                        break;
                    }

                    instructions[i] = original;
                }
            }
        }

        private static bool TryExecuteInstructions(Instruction[] instructions, out int acc)
        {
            acc = 0;
            var instructionIndex = 0;

            var executedInstructions = new HashSet<int>();

            while (instructionIndex < instructions.Length)
            {
                if (executedInstructions.Contains(instructionIndex))
                    return false;

                executedInstructions.Add(instructionIndex);

                var instruction = instructions[instructionIndex];

                switch (instruction.Operation)
                {
                    case "acc":
                        acc += instruction.Argument;
                        instructionIndex++;
                        break;

                    case "jmp":
                        instructionIndex += instruction.Argument;
                        break;

                    case "nop":
                        instructionIndex++;
                        break;
                }
            }

            return true;
        }
    }
}