using System;

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

        public static Random Random = new Random();

        public static Position ComputePosition(Container container)
        {
            return new Position(
                Random.Next(container.Width.X1, container.Width.X2),
                Random.Next(container.Height.X1, container.Height.X2)
                );
        }

        public static int GetRandomInt(int min, int max)
        {
            return Random.Next(min, max);
        }

        public static Position CumputePositionForAsteroid(Container container)
        {
            return new Position(
                Random.Next(container.Width.X2, container.Width.X2 + 150),
                Random.Next(container.Height.X1 - 100, container.Height.X2 - 100)
                );
        }

        public static Position ComputePositionForHeart(Container container)
        {
            return new Position(
                Random.Next(container.Width.X1, (container.Width.X2 / 3) * 2),
                Random.Next(container.Height.X1 - 10, container.Height.X2 - 10)
                );
        }

    }
}
