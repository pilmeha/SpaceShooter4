using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter3
{
    internal class SplashScreen
    {
        public Texture2D Backgorund {  get; set; }
        private int timeCounter = 0;
        private Color color;
        public SpriteFont BigFont {  get; set; }
        public SpriteFont SmallFont { get; set; }
        public SpriteFont CopyrightFont { get; set; }
        private string gameTitle = "SpaceShooter!";
        private string gameNew = "New";
        private string gameResume = "Resume";
        private string gameExit = "Exit";
        private string gameCopyright = "(c) Ayur Garmaev, 2024";
        public MenuState MenuState { get; set; } = MenuState.New;
        public bool StartedAtFirstTime { get; set; } = true;

        private int optionsCounter = 1;
        public int OptionsCounter
        {
            get
            {
                return optionsCounter;
            }
            set
            {
                if (value > 3)
                    optionsCounter = 3;

                else if (value < 1)
                    optionsCounter = 1;

                else
                    optionsCounter = value;

                if (optionsCounter == 1)
                    MenuState = MenuState.New;

                else if (optionsCounter == 2)
                    MenuState = MenuState.Resume;

                else
                    MenuState = MenuState.Exit;

            }
        }

        public void Update()
        {
            color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 256);
            timeCounter++;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(
                Backgorund,
                new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight),
                Color.White
                );

            spriteBatch.DrawString(
                BigFont,
                gameTitle,
                new Vector2(
                    (graphics.PreferredBackBufferWidth / 2),
                    (graphics.PreferredBackBufferHeight / 2)
                    ),
                color
                );

            //spriteBatch.DrawString(
            //    SmallFont,
            //    "Press Enter to start",
            //    new Vector2(
            //        (graphics.PreferredBackBufferWidth / 100) * 50,
            //        (graphics.PreferredBackBufferHeight / 100) * 78
            //        ),
            //    Color
            //    );


            if (MenuState == MenuState.New)
            {
                spriteBatch.DrawString(
                    SmallFont,
                    gameNew,
                    new Vector2(
                        (graphics.PreferredBackBufferWidth / 100) * 50,
                        (graphics.PreferredBackBufferHeight / 100) * 78
                        ),
                    Color.Violet
                    );
            }
            else
            {
                spriteBatch.DrawString(
                    SmallFont,
                    gameNew,
                    new Vector2(
                        (graphics.PreferredBackBufferWidth / 100) * 50,
                        (graphics.PreferredBackBufferHeight / 100) * 78
                    ),
                    color
                    );
            }

            if (StartedAtFirstTime)
            {
                spriteBatch.DrawString(
                    SmallFont,
                    gameResume,
                    new Vector2(
                        (graphics.PreferredBackBufferWidth / 100) * 50,
                        (graphics.PreferredBackBufferHeight / 100) * 86
                        ),
                    Color.Gray
                    );
            }
            else
            {
                if (MenuState == MenuState.Resume)
                {
                    spriteBatch.DrawString(
                        SmallFont,
                        gameResume,
                        new Vector2(
                            (graphics.PreferredBackBufferWidth / 100) * 50,
                            (graphics.PreferredBackBufferHeight / 100) * 86
                            ),
                        Color.Violet
                        );
                }
                else
                {
                    spriteBatch.DrawString(
                        SmallFont,
                        gameResume,
                        new Vector2(
                            (graphics.PreferredBackBufferWidth / 100) * 50,
                            (graphics.PreferredBackBufferHeight / 100) * 86
                        ),
                        color
                        );
                }
            }

            if (MenuState == MenuState.Exit)
            {
                spriteBatch.DrawString(
                    SmallFont,
                    gameExit,
                    new Vector2(
                        (graphics.PreferredBackBufferWidth / 100) * 50,
                        (graphics.PreferredBackBufferHeight / 100) * 94
                        ),
                    Color.Red
                    );
            }
            else
            {
                spriteBatch.DrawString(
                    SmallFont,
                    gameExit,
                    new Vector2(
                        (graphics.PreferredBackBufferWidth / 100) * 50,
                        (graphics.PreferredBackBufferHeight / 100) * 94
                    ),
                    color
                    );
            }

            spriteBatch.DrawString(
                CopyrightFont,
                gameCopyright,
                new Vector2(
                    (graphics.PreferredBackBufferWidth / 100) * 50,
                    (graphics.PreferredBackBufferHeight / 100) * 106
                    ),
                color
                );
        }
    }
}
