using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter3
{
    internal class Fire
    {
        private int speed = 5;
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
        public bool Intersected { get; set; } = false;

        public Fire(Texture2D texture, Rectangle rectangle)
        {
            Texture = texture;
            Rectangle = rectangle;
        }

        public bool IsOutOfScreen(Container container)
        {
            if (X > container.Width.X2 || X < container.Width.X1 || Y > container.Height.X2 || Y < container.Height.X1 || this.Intersected)
                return true;
            return false;
        }

        public void Update()
        {
            X += speed;
        }

        //public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
        //    X += speed;
        //    //spriteBatch.Draw(
        //    //    Texture,
        //    //    Rectangle,
        //    //    null,
        //    //    Color.White,
        //    //    0f,
        //    //    new Vector2(Texture.Width / 2, Texture.Height / 2),
        //    //    SpriteEffects.None,
        //    //    0f
        //    //    );
        //}
    }
}
