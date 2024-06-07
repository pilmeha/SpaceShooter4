using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShooter3.game;

namespace SpaceShooter3
{
    internal class SpaceShip
    {
        public static int Speed { get; set; } = 8;
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

        public SpaceShip(Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }

        public void Update()
        {
            //KeyboardState kstate = Keyboard.GetState();
            if (Input.keyboardState.IsKeyDown(Keys.A) && X > 0)
                X -= Speed;

            if (Input.keyboardState.IsKeyDown(Keys.D) && X < Globals.Container.Width.X2 - rectangle.Width)
                X += Speed;

            if (Input.keyboardState.IsKeyDown(Keys.W) && Y > 0)
                Y -= Speed;

            if (Input.keyboardState.IsKeyDown(Keys.S) && Y < Globals.Container.Height.X2 - rectangle.Height)
                Y += Speed;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Art.SpaceShipTexture, rectangle, Color.White);
        }
    }
}
