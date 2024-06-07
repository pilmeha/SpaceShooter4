using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter3.game;

namespace SpaceShooter3
{
    internal class Asteroid
    {
        public Container Container { get; set; }
        private int speed = 5;

        public Texture2D Texture 
        {
            get
            {
                if (Intersected)
                    return Art.TextureBoom;
                return Art.TextureAsteroid;
            }     
        }
        public bool Intersected { get; set; } = false;
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

        public Asteroid(Rectangle rectangle, Position position, Container container)
        {
            this.rectangle = rectangle;
            X = position.X;
            Y = position.Y;

            Container = container;
        }

        public void Update()
        {
            X -= speed;
            if (X < Container.Width.X1 - 200)
            {
                this.SetPosition(Position.CumputePositionForAsteroid(Container));
                this.Intersected = false;
            }
        }

        public void SetPosition(Position position)
        {
            X = position.X;
            Y = position.Y;
            var asteroidSize = Position.GetRandomInt(
                Art.TextureAsteroid.Width / 5,
                (int)(Art.TextureAsteroid.Width * 1.5)
                );
            this.rectangle.Width = asteroidSize;
            this.rectangle.Height = asteroidSize;

        }
    }
}
