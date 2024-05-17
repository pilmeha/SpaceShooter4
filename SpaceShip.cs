using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter3
{
    internal class SpaceShip
    {
        public Texture2D Texture { get; set; }
        public int Speed { get; set; } = 8;
        public Container Container { get; set; }
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

        public SpaceShip(Texture2D texture, Rectangle rectangle, Container container)
        {
            Texture = texture;
            this.rectangle = rectangle;
            Container = container;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, rectangle, Color.White);
        }

        public void Left()
        {
            if (X > 0)
                X -= Speed;
        }

        public void Right()
        {
            if (X < Container.Width.X2 - Texture.Width)
                X += Speed;
        }

        public void Up()
        {
            if (Y > 0)
                Y -= Speed;
        }

        public void Down()
        {
            if (Y < Container.Height.X2 - Texture.Height)
                Y += Speed;
        }
    }
}
