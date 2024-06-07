using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter3.game;


namespace SpaceShooter3
{
    internal class SplashScreen
    {
        private int timeCounter = 0;
        private Color color;
        private string gameTitle = "SpaceShooter!";
        private string gameNew = "New";
        private string gameResume = "Resume";
        private string gameExit = "Exit";
        private string gameCopyright = "(c) Ayur Garmaev, 2024";
        private string bestScore = "Best score!";
        private string lastScore = "Last score:D";
        public int BestScore { get; set; } = 0;
        public int LastScore { get; set; } = 0;
        public static MenuState MenuState { get; set; } = MenuState.New;
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
                Art.SplashScreenTexture,
                new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight),
                Color.White
                );

            spriteBatch.DrawString(
                Art.BigFont,
                gameTitle,
                new Vector2(
                    ((graphics.PreferredBackBufferWidth / 100) * 11),
                    (graphics.PreferredBackBufferHeight / 2)
                    ),
                color
                );

            if (MenuState == MenuState.New)
            {
                spriteBatch.DrawString(
                    Art.SmallFont,
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
                    Art.SmallFont,
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
                    Art.SmallFont,
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
                        Art.SmallFont,
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
                        Art.SmallFont,
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
                    Art.SmallFont,
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
                    Art.SmallFont,
                    gameExit,
                    new Vector2(
                        (graphics.PreferredBackBufferWidth / 100) * 50,
                        (graphics.PreferredBackBufferHeight / 100) * 94
                    ),
                    color
                    );
            }

            spriteBatch.DrawString(
                Art.CopyrightFont,
                gameCopyright,
                new Vector2(
                    (graphics.PreferredBackBufferWidth / 100) * 50,
                    (graphics.PreferredBackBufferHeight / 100) * 106
                    ),
                color
                );


            spriteBatch.DrawString(
                Art.SmallFont,
                BestScore.ToString(),
                new Vector2(
                    (graphics.PreferredBackBufferWidth / 100) * 15,
                    (graphics.PreferredBackBufferHeight / 100) * 15
                    ),
                color
                );

            spriteBatch.DrawString(
                Art.SmallFont,
                bestScore,
                new Vector2(
                    (graphics.PreferredBackBufferWidth / 100) * 10,
                    (graphics.PreferredBackBufferHeight / 100) * 25
                    ),
                color
                );

            spriteBatch.DrawString(
                Art.CopyrightFont,
                LastScore.ToString(),
                new Vector2(
                    (graphics.PreferredBackBufferWidth / 100) * 75,
                    (graphics.PreferredBackBufferHeight / 100) * 25
                    ),
                color,
                0.6f,
                new Vector2(0, 0),
                1.5f,
                SpriteEffects.None,
                0.0f
                );

            spriteBatch.DrawString(
                Art.CopyrightFont,
                lastScore,
                new Vector2(
                    (graphics.PreferredBackBufferWidth / 100) * 60,
                    (graphics.PreferredBackBufferHeight / 100) * 15
                    ),
                color,
                0.6f,
                new Vector2(0, 0),
                1.5f,
                SpriteEffects.None,
                0.0f
                );
        }
    }
}
