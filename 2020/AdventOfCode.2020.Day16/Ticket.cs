using System.Linq;

namespace AdventOfCode
{
    public record Ticket
    {
        public int[] Values { get; }

        public Ticket(params int[] values)
        {
            Values = values;
        }

        public static Ticket Parse(string str)
        {
            var values = str
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            return new Ticket(values);
        }
    }
}