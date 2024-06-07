using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;

namespace SpaceShooter3
{
    internal class Globals
    {
        public static SpriteBatch spriteBatch;
        public static int WIDTH = 700, HEIGHT = 580;
        public static int score;
        public static Container Container {  get; private set; }
    }
}
