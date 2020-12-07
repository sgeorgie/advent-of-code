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
            var rules = File.ReadAllLines("input.txt");

            var bags = new List<Bag>();

            var ruleParser = new Regex(@"^(.+) bags contain (.+)\.$");
            var bagNameParser = new Regex(@"^(\d+) ([\w\s]+) bags?$");

            foreach (var rule in rules)
            {
                var match = ruleParser.Match(rule);

                var carrierBagName = match.Groups[1].Value;

                var carrierBag = bags.FirstOrDefault(b => b.Name == carrierBagName);

                if (carrierBag == null)
                {
                    carrierBag = new Bag(carrierBagName);
                    bags.Add(carrierBag);
                }

                foreach (var definition in match.Groups[2].Value.Split(',', StringSplitOptions.TrimEntries))
                {
                    if (definition == "no other bags")
                        continue;

                    var count = int.Parse(bagNameParser.Match(definition).Groups[1].Value);
                    var carriedBagName = bagNameParser.Match(definition).Groups[2].Value;

                    var carriedBag = bags.FirstOrDefault(b => b.Name == carriedBagName);

                    if (carriedBag == null)
                    {
                        carriedBag = new Bag(carriedBagName);
                        bags.Add(carriedBag);
                    }

                    carrierBag.AddCarriedBag(carriedBag, count);
                }
            }

            var myBag = bags.First(b => b.Name == "shiny gold");

            Console.WriteLine($"Part 1: shiny gold bags can be carried inside {myBag.GetPossibleCarrierBags().Count} other bags");
            Console.WriteLine($"Part 2: shiny gold bags can carry {myBag.GetTotalCarriedBags()} other bags");
        }
    }
}