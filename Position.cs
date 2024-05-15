using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter3
{
    internal class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position ComputePosition(Container container)
        {
            Random random = new Random();
            return new Position(
                random.Next(container.Width.X1, container.Width.X2),
                random.Next(container.Height.X1, container.Height.X2)
                );
        }
    }
}
