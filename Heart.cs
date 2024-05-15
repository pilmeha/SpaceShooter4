using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter3
{
    internal class Heart
    {
        public Texture2D Texture { get; set; }
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

        public Heart(Texture2D texture, Rectangle rectangle)
        {
            Texture = texture;
            this.rectangle = rectangle;
        }

        public Heart(Texture2D texture, Rectangle rectangle, Position position)
        {
            Texture = texture;
            this.rectangle = rectangle;

            X = position.X;
            Y = position.Y;
        }

        public void SetPosition(Position position)
        {
            X = position.X;
            Y = position.Y;
        }
    }
}
