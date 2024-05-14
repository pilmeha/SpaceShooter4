using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter3
{
    internal class SplashScreen
    {
        public static Texture2D Backgorund {  get; set; }
        static int TimeCounter = 0;
        static Color Color;
        public static SpriteFont BigFont {  get; set; }
        public static SpriteFont SmallFont { get; set; }

        public static void Update()
        {
            Color = Color.FromNonPremultiplied(255, 255, 255, TimeCounter % 256);
            TimeCounter++;
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(
                Backgorund,
                new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight),
                Color.White
                );

            spriteBatch.DrawString(
                BigFont,
                "SpaceShooter!",
                new Vector2(
                    (graphics.PreferredBackBufferWidth / 2),
                    (graphics.PreferredBackBufferHeight / 2)
                    ),
                Color
                );

            spriteBatch.DrawString(
                SmallFont,
                "Press Enter to start",
                new Vector2(
                    (graphics.PreferredBackBufferWidth / 100) * 50,
                    (graphics.PreferredBackBufferHeight / 100) * 78
                    ),
                Color
                );
        }
    }
}
