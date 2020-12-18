using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Instruction
    {
        public char Action { get; }

        public int Value { get; }

        public Instruction(char action, int value)
        {
            Action = action;
            Value = value;
        }

        public void ExecuteV1(Boat boat)
        {
            switch (Action)
            {
                case 'N':
                case 'E':
                case 'S':
                case 'W':
                    var direction = Coords.ParseDirection(Action);
                    boat.Coords = boat.Coords.Translate(direction, Value);
                    break;

                case 'R':
                case 'L':
                    boat.Waypoint = boat.Waypoint.Rotate(Action, Value);
                    break;

                case 'F':
                    boat.Coords = boat.Coords.Translate(boat.Waypoint, Value);
                    break;
            }
        }

        public void ExecuteV2(Boat boat)
        {
            switch (Action)
            {
                case 'N':
                case 'E':
                case 'S':
                case 'W':
                    var direction = Coords.ParseDirection(Action);
                    boat.Waypoint = boat.Waypoint.Translate(direction, Value);
                    break;

                case 'R':
                case 'L':
                    boat.Waypoint = boat.Waypoint.Rotate(Action, Value);
                    break;

                case 'F':
                    boat.Coords = boat.Coords.Translate(boat.Waypoint, Value);
                    break;
            }
        }

        public static Instruction Parse(string str)
        {
            var instructionParser = new Regex(@"^(\w)(\d+)");

            var match = instructionParser.Match(str);

            return new Instruction(match.Groups[1].Value.First(), int.Parse(match.Groups[2].Value));
        }
    }
}