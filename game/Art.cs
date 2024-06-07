using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter3.game
{
    internal class Art
    {
        public static Texture2D SplashScreenTexture { get; private set; }
        public static Texture2D SpaceShipTexture { get; private set; }
        public static Texture2D BottleTexture { get; private set; }
        public static Texture2D TextureBoom { get; private set; }
        public static Texture2D TextureAsteroid { get; private set; }
        public static Texture2D TextureFire { get; private set; }
        public static Texture2D StarTexture { get; private set; }
        public static SpriteFont BigFont { get; private set; }
        public static SpriteFont SmallFont { get; private set; }
        public static SpriteFont CopyrightFont { get; private set; }

        public static void Load(ContentManager content)
        {
            SplashScreenTexture = content.Load<Texture2D>("Images/background");
            SpaceShipTexture = content.Load<Texture2D>("Images/spaceShip");
            BottleTexture = content.Load<Texture2D>("Images/bottle");
            TextureBoom = content.Load<Texture2D>("Images/boomAsteroid");
            TextureAsteroid = content.Load<Texture2D>("Images/asteroid");
            TextureFire = content.Load<Texture2D>("Images/fire");
            StarTexture = content.Load<Texture2D>("Images/star");

            BigFont = content.Load<SpriteFont>("Fonts/bigFont");
            SmallFont = content.Load<SpriteFont>("Fonts/smallFont");
            CopyrightFont = content.Load<SpriteFont>("Fonts/copyrightFont");
        }
    }
}
