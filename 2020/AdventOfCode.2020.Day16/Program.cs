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

            Ticket myTicket;
            var nearbyTickets = new List<Ticket>();
            var rules = new List<Rule>();

            var lineNumber = 0;

            // Parse rules
            while (!string.IsNullOrEmpty(input[lineNumber]))
            {
                var line = input[lineNumber];
                var rule = Rule.Parse(line);

                rules.Add(rule);

                lineNumber++;
            }

            lineNumber += 2;

            // Parse my ticket
            {
                var line = input[lineNumber];

                myTicket = Ticket.Parse(line);
            }

            lineNumber += 3;

            // Parse nearby tickets
            while (lineNumber < input.Length)
            {
                var line = input[lineNumber];
                var ticket = Ticket.Parse(line);

                nearbyTickets.Add(ticket);

                lineNumber++;
            }

            // Part 1
            {
                var invalidValueCount = 0;

                foreach (var ticket in nearbyTickets)
                {
                    foreach (var value in ticket.Values)
                    {
                        if (!rules.Any(r => r.Validate(value)))
                            invalidValueCount += value;
                    }
                }

                Console.WriteLine($"Part 1: Ticket error rate is {invalidValueCount}");
            }

            // Part 2
            {
                var validTickets = nearbyTickets
                    .Where(ticket => ticket.Values.All(v => rules.Any(r => r.Validate(v))))
                    .ToArray();

                var fieldRuleMatches = new Dictionary<int, List<string>>();

                // Find all possible matches for fields
                for (int i = 0; i < myTicket.Values.Length; i++)
                {
                    foreach (var rule in rules)
                    {
                        if (validTickets.Select(t => t.Values[i]).All(v => rule.Validate(v)))
                        {
                            if (!fieldRuleMatches.ContainsKey(i))
                            {
                                fieldRuleMatches[i] = new List<string>();
                            }
                            fieldRuleMatches[i].Add(rule.FieldName);
                        }
                    }
                }

                var fieldMap = new Dictionary<string, int>();

                while (fieldRuleMatches.Any())
                {
                    var singularRule = fieldRuleMatches.First(kv => kv.Value.Count == 1);

                    var field = singularRule.Value.First();
                    var index = singularRule.Key;

                    fieldMap.Add(field, index);

                    fieldRuleMatches.Remove(index);
                    foreach (var (_, fields) in fieldRuleMatches)
                    {
                        fields.Remove(field);
                    }
                }

                var result = 1L;
                foreach (var field in fieldMap.Keys.Where(k => k.StartsWith("departure")))
                {
                    result *= myTicket.Values[fieldMap[field]];
                }

                Console.WriteLine($"Part 2: Multiplication result of all departure fields is {result}");
            }
        }
    }
}