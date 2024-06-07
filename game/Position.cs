using System;

namespace SpaceShooter3.game
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

        public static Position ComputePosition()
        {
            return new Position(
                Random.Next(Globals.Container.Width.X1, Globals.Container.Width.X2),
                Random.Next(Globals.Container.Height.X1, Globals.Container.Height.X2)
                );
        }

        public static int GetRandomInt(int min, int max)
        {
            return Random.Next(min, max);
        }

        public static Position CumputePositionForAsteroid()
        {
            return new Position(
                Random.Next(Globals.Container.Width.X2, Globals.Container.Width.X2 + 150),
                Random.Next(Globals.Container.Height.X1 - 100, Globals.Container.Height.X2 - 100)
                );
        }

        public static Position ComputePositionForBottle()
        {
            return new Position(
                Random.Next(Globals.Container.Width.X1, Globals.Container.Width.X2 / 3 * 2),
                Random.Next(Globals.Container.Height.X1 - 10, Globals.Container.Height.X2 - 10)
                );
        }

    }
}
