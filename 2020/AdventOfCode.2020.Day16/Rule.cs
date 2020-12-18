using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public record Rule
    {
        private static readonly Regex _regex = new Regex(@"^([^:]+): (.+)$");

        public string FieldName { get; }

        public Range[] Ranges { get; }

        public Rule(string fieldName, Range[] ranges)
        {
            FieldName = fieldName;
            Ranges = ranges;
        }

        public bool Validate(int value) => Ranges.Any(r => r.IsInRange(value));

        public static Rule Parse(string str)
        {
            var match = _regex.Match(str);

            var fieldName = match.Groups[1].Value;

            var ranges = match.Groups[2].Value
                .Split("or", StringSplitOptions.TrimEntries)
                .Select(Range.Parse)
                .ToArray();

            return new Rule(fieldName, ranges);
        }
    }
}