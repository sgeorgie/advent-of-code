namespace AdventOfCode
{
    internal class Boat
    {
        public Coords Coords { get; set; }
        public Coords Waypoint { get; set; }

        public Boat(Coords coords, Coords waypoint)
        {
            Coords = coords;
            Waypoint = waypoint;
        }
    }
}