using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt")
                .Select(PasswordPolicy.Parse)
                .ToArray();

            var validForFirstCorporation = input.Count(policy => policy.IsValidForFirstCorporation());

            Console.WriteLine($"Valid passwords according to the first corporation rules: {validForFirstCorporation}");

            var validForSecondCorporation = input.Count(policy => policy.IsValidForSecondCorporation());

            Console.WriteLine($"Valid passwords according to the second corporation rules: {validForSecondCorporation}");
        }
    }
}