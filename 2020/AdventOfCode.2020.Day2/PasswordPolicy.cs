using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class PasswordPolicy
    {
        private static readonly Regex _parserRegex = new Regex(@"^(\d+)-(\d+) (.): (.+)$");

        private PasswordPolicy(int policyLowerBound, int policyUpperBound, char policyLetter, string password)
        {
            PolicyLowerBound = policyLowerBound;
            PolicyUpperBound = policyUpperBound;
            PolicyLetter = policyLetter;
            Password = password;
        }

        public int PolicyLowerBound { get; }

        public int PolicyUpperBound { get; }

        public char PolicyLetter { get; }

        public string Password { get; }

        public bool IsValidForFirstCorporation()
        {
            var occurences = Password.Count(c => c == PolicyLetter);

            return occurences >= PolicyLowerBound && occurences <= PolicyUpperBound;
        }

        public bool IsValidForSecondCorporation()
        {
            return (Password.Length >= PolicyLowerBound && Password[PolicyLowerBound - 1] == PolicyLetter) ^
                (Password.Length >= PolicyUpperBound && Password[PolicyUpperBound - 1] == PolicyLetter);
        }

        public static PasswordPolicy Parse(string input)
        {
            var match = _parserRegex.Match(input);
            var lower = Convert.ToInt32(match.Groups[1].Value);
            var upper = Convert.ToInt32(match.Groups[2].Value);
            var letter = match.Groups[3].Value.First();
            var password = match.Groups[4].Value;

            return new PasswordPolicy(lower, upper, letter, password);
        }
    }
}