using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        private static readonly string[] RequiredFields = new string[] {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

        private static readonly Regex HairColorValidator = new Regex("^#[0-9a-f]{6}$");

        private static readonly Regex PassportIdValidator = new Regex(@"^\d{9}$");

        private static readonly HashSet<string> ValidEyeColors = new HashSet<string>() {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        private static readonly Dictionary<string, Func<string, bool>> Validators = new Dictionary<string, Func<string, bool>>
        {
            ["byr"] = (string field) => int.TryParse(field, out var year) && year >= 1920 && year <= 2002,
            ["iyr"] = (string field) => int.TryParse(field, out var year) && year >= 2010 && year <= 2020,
            ["eyr"] = (string field) => int.TryParse(field, out var year) && year >= 2020 && year <= 2030,
            ["hgt"] = (string field) =>
            {
                if (field.Length < 3)
                    return false;

                if (!int.TryParse(field.Substring(0, field.Length - 2), out var value))
                    return false;

                if (field.EndsWith("cm"))
                    return value >= 150 && value <= 193;

                if (field.EndsWith("in"))
                    return value >= 59 && value <= 76;

                return false;
            },
            ["hcl"] = (string field) => HairColorValidator.IsMatch(field),
            ["ecl"] = (string field) => ValidEyeColors.Contains(field),
            ["pid"] = (string field) => PassportIdValidator.IsMatch(field),
        };

        static void Main(string[] args)
        {
            var passports = File.ReadAllText("input.txt")
                // Split the input into multiple passports (separated by empty line)
                .Split("\n\n")
                // split a single passport into key:value pairs (separated by space or new line)
                .Select(passportStr => passportStr
                    .Split(new string[] {" ", "\n"}, StringSplitOptions.TrimEntries)
                    .Where(fieldPair => !string.IsNullOrWhiteSpace(fieldPair))
                    .ToDictionary(fieldPair => fieldPair.Substring(0, 3),
                        fieldPair => fieldPair.Substring(4)))
                .ToArray();

            ValidatePassportWithFieldKeys(passports);
            ValidatePassportsWithFieldValues(passports);
        }

        static void ValidatePassportWithFieldKeys(Dictionary<string, string>[] passports)
        {
            var validPassports = 0;

            foreach (var passport in passports)
            {
                // Check if the passports contains all the required fields
                if (RequiredFields.All(field => passport.ContainsKey(field)))
                {
                    validPassports++;
                }
            }

            Console.WriteLine($"Part 1: Found {validPassports} valid passports");
        }

        static void ValidatePassportsWithFieldValues(Dictionary<string, string>[] passports)
        {
            var validPassports = 0;

            foreach (var passport in passports)
            {
                var isValid = true;

                foreach (var requiredField in RequiredFields)
                {
                    // Check if the required field is present and validate its value
                    if (!passport.ContainsKey(requiredField) || !Validators[requiredField](passport[requiredField]))
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                    validPassports++;
            }

            Console.WriteLine($"Part 2: Found {validPassports} valid passports");
        }
    }
}