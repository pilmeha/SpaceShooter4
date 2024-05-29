using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter3
{
    internal class Asteroid
    {
        public Container Container { get; set; }
        private int speed = 5;
        private Texture2D textureBoom;
        private Texture2D textureAsteroid;
        //public float Rotation { get; set; } = 0;
        //public float RotationSpeed { get; set; } = (float)(Position.Random.NextDouble() - 0.5) / 2;
        public Texture2D Texture 
        {
            get
            {
                if (Intersected)
                    return textureBoom;
                return textureAsteroid;
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

        public Asteroid(Texture2D textureBoom, Texture2D textureAsteroid, Rectangle rectangle, Position position, Container container)
        {
            this.textureBoom = textureBoom;
            this.textureAsteroid = textureAsteroid;
            this.rectangle = rectangle;
            X = position.X;
            Y = position.Y;

            Container = container;
        }

        public void Update()
        {
            X -= speed;
            //Rotation += RotationSpeed;
            if (X < Container.Width.X1 - 200)
            {
                this.SetPosition(Position.CumputePositionForAsteroid(Container));
                this.Intersected = false;
            }
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        public void SetPosition(Position position)
        {
            X = position.X;
            Y = position.Y;
            //RotationSpeed = (float)(Position.Random.NextDouble() - 0.5) / 2;
            var asteroidSize = Position.GetRandomInt(
                textureAsteroid.Width / 5,
                (int)(textureAsteroid.Width * 1.5)
                );
            this.rectangle.Width = asteroidSize;
            this.rectangle.Height = asteroidSize;

        }
    }
}
