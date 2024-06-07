using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter3.game;



namespace SpaceShooter3
{
    internal class Globals
    {
        public static SpriteBatch spriteBatch;
        public static int WIDTH = 700, HEIGHT = 580;
        public static Container Container { get; private set; }

        public static void Load(GraphicsDeviceManager graphics)
        {
            Container = new Container(new Line(0, graphics.PreferredBackBufferWidth),
                new Line(0, graphics.PreferredBackBufferHeight));
        }
    }
}
