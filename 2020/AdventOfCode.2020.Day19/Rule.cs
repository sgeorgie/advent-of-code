using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Rule
    {
        public static readonly Dictionary<int, Rule> Registry = new Dictionary<int, Rule>();

        public int Id { get; }

        public int[][] SubRules { get; }

        public string Literal { get; }

        private Rule(int id)
        {
            Id = id;

            Registry[id] = this;
        }

        private Rule(int id, int[][] subRules) : this(id)
        {
            SubRules = subRules;
        }

        private Rule(int id, string literal) : this(id)
        {
            Literal = literal;
        }

        public static Rule Parse(string str)
        {
            var indexOfColon = str.IndexOf(':');
            var id = int.Parse(str[0..indexOfColon]);

            if (str[indexOfColon + 2] == '"')
            {
                var literal = str[(indexOfColon + 3)..^1];
                return new Rule(id, literal);
            }

            var ruleSets = str[(indexOfColon + 1)..]
                .Split('|', StringSplitOptions.TrimEntries)
                .Select(ruleSet => ruleSet
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();

            return new Rule(id, ruleSets);
        }

        private static bool TryGetPossibleMatches(int[] rules, string str, out HashSet<string> matches)
        {
            if (str.Length == 0 || rules.Length == 0)
            {
                matches = new HashSet<string>();
                return rules.Length <= str.Length;
            }

            // Get matches of the first rule
            var firstRule = Registry[rules.First()];

            if (!firstRule.TryGetPossibleMatches(str, out var subMatches))
            {
                matches = null;
                return false;
            }

            if (rules.Length == 1)
            {
                matches = subMatches;
                return true;
            }

            matches = new HashSet<string>();

            foreach (var subMatch in subMatches)
            {
                if (str.Length == subMatch.Length)
                {
                    // the remaining rules cannot match
                    continue;
                }

                var remaining = str[subMatch.Length..];

                if (TryGetPossibleMatches(rules[1..], remaining, out var subSubMatches))
                {
                    foreach (var subSubMatch in subSubMatches)
                    {
                        matches.Add(subMatch + subSubMatch);
                    }
                }
            }

            return matches.Any();
        }

        public bool IsMatch(string str)
        {
            return TryGetPossibleMatches(str, out var matches) && matches.Contains(str);
        }

        private bool TryGetPossibleMatches(string str, out HashSet<string> matches)
        {
            if (Literal != null)
            {
                if (str.StartsWith(Literal))
                {
                    matches = new HashSet<string>() {Literal};
                    return true;
                }

                matches = null;
                return false;
            }

            matches = new HashSet<string>();

            foreach (var ruleSet in SubRules)
            {
                if (TryGetPossibleMatches(ruleSet, str, out var subMatches))
                {
                    foreach (var match in subMatches)
                    {
                        matches.Add(match);
                    }
                }
            }

            return matches.Any();
        }
    }
}