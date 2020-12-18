using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public record Range
    {
        private static readonly Regex _regex = new Regex(@"^(\d+)-(\d+)$");

        public int LowerBound { get; }

        public int UpperBound { get; }

        public Range(int lowerBound, int upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        public bool IsInRange(int value) => value >= LowerBound && value <= UpperBound;

        public static Range Parse(string str)
        {
            var match = _regex.Match(str);
            return new Range(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        }
    }
}