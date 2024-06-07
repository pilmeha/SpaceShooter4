using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter3.game;

namespace SpaceShooter3
{
    internal class Bottle
    {
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                rectangle = value;
            }
        }

        public int X
        {
            get
            {
                return rectangle.X;
            }
            set
            {
                rectangle.X = value;
            }
        }

        public int Y
        {
            get
            {
                return rectangle.Y;
            }
            set
            {
                rectangle.Y = value;
            }
        }

        public bool WasEaten { get; set; } = false;

        public Bottle(Rectangle rectangle, Position position)
        {
            this.rectangle = rectangle;

            X = position.X;
            Y = position.Y;
        }

        public void SetPosition(Position position)
        {
            X = position.X;
            Y = position.Y;
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }
    }
}
