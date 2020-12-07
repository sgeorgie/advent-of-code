using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Bag
    {
        public Bag(string name)
        {
            Name = name;
            Relations = new List<CarrierRelation>();
        }

        public string Name { get; }

        public List<CarrierRelation> Relations { get; }

        public void AddCarriedBag(Bag bag, int count)
        {
            var relation = new CarrierRelation(this, bag, count);
            Relations.Add(relation);
            bag.Relations.Add(relation);
        }

        public HashSet<Bag> GetPossibleCarrierBags()
        {
            var carriers = Relations
                .Where(r => r.Carried == this)
                .Select(r => r.Carrier)
                .ToHashSet();

            foreach (var bag in carriers.ToArray())
            {
                carriers.UnionWith(bag.GetPossibleCarrierBags().Where(b => b != this));
            }

            return carriers;
        }

        public int GetTotalCarriedBags() => Relations
                .Where(r => r.Carrier == this)
                .Sum(relation => (relation.Carried.GetTotalCarriedBags() + 1) * relation.Count);
    }
}