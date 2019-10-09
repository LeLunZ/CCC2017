using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contest
{
    class Location
    {
        public string name;
        public int x;
        public int y;
        public List<int> hasStop;

        public Location(string n, int cx, int cy)
        {
            x = cx;
            y = cy;
            name = n;
            hasStop = new List<int>();
        }

        public double distanceTo(Location other)
        {
            return Math.Sqrt(Math.Pow(x - other.x, 2) + Math.Pow(y - other.y, 2));
        }
    }
}
