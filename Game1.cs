using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShooter3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private State state = State.SplashScreen;

        private Texture2D splashScreenTexture;
        private SpriteFont bigFont;
        private SpriteFont smallFont;
        private SpriteFont copyrightFont;

        private Container container;

        private Texture2D spaceShipTexture;
        private SpaceShip spaceShip;

        private KeyboardState keyBoardCurrent;
        private KeyboardState keyBoardOld;

        private Texture2D bottleTexture;
        private Bottle bottle;

        private int score = 0;
        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                if (value < 0)
                    score = 0;
                else
                    score = value;
            }
        }

        private Texture2D textureBoom;
        private Texture2D textureAsteroid;
        private List<Asteroid> asteroids = new List<Asteroid>();
        private int countAsteroids = 0;

        private Texture2D textureFire;
        private List<Fire> fires = new List<Fire>();

        private int countFires = 10;

        private Texture2D starTexture;
        private Star[] stars = new Star[50];

        private SplashScreen splashScreen = new SplashScreen();

        //private Song menuSong;
        private SoundEffect fireSound;
        private SoundEffect boomSound;
        private SoundEffect menuSound;
        private SoundEffect enterSound;
        private SoundEffect intersectedSound;
        private SoundEffect bottleSound;
        private SoundEffect buySound;

        private Song gameSong;

        private int heart = 3;
        public int Heart
        {
            get
            {
                return heart;
            }
            set
            {
                if (value < 0)
                    heart = 0;
                else
                    heart = value;
            }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            container = new Container(
                new Line(0, _graphics.PreferredBackBufferWidth),
                new Line(0, _graphics.PreferredBackBufferHeight)
                );
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            splashScreenTexture = Content.Load<Texture2D>("Images/background");
            splashScreen.Backgorund = splashScreenTexture;
            bigFont = Content.Load<SpriteFont>("Fonts/bigFont");
            splashScreen.BigFont = bigFont;
            smallFont = Content.Load<SpriteFont>("Fonts/smallFont");
            splashScreen.SmallFont = smallFont;
            copyrightFont = Content.Load<SpriteFont>("Fonts/copyrightFont");
            splashScreen.CopyrightFont = copyrightFont;

            spaceShipTexture = Content.Load<Texture2D>("Images/spaceShip");
            spaceShip = new SpaceShip(
                spaceShipTexture, 
                new Rectangle(
                    0, 
                    _graphics.PreferredBackBufferHeight / 2,
                    spaceShipTexture.Width,
                    spaceShipTexture.Height
                    ),
                container
                );

            bottleTexture = Content.Load<Texture2D>("Images/bottle");
            bottle = new Bottle(
                bottleTexture,
                new Rectangle(
                    0,
                    0,
                    bottleTexture.Width,
                    bottleTexture.Height
                    ),
                Position.ComputePositionForBottle(container)
                );

            textureBoom = Content.Load<Texture2D>("Images/boomAsteroid");
            textureAsteroid = Content.Load<Texture2D>("Images/asteroid");

            textureFire = Content.Load<Texture2D>("Images/fire");

            starTexture = Content.Load<Texture2D>("Images/star");
            Star.Texture = starTexture;
            Star.Container = container;
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star();
            }

            fireSound = Content.Load<SoundEffect>("Sounds/fireSound");
            boomSound = Content.Load<SoundEffect>("Sounds/boomSound");
            menuSound = Content.Load<SoundEffect>("Sounds/menuSound");
            enterSound = Content.Load<SoundEffect>("Sounds/enterSound");
            intersectedSound = Content.Load<SoundEffect>("Sounds/intersectedSound");
            bottleSound = Content.Load<SoundEffect>("Sounds/heartSound");
            buySound = Content.Load<SoundEffect>("Sounds/buySound");
            gameSong = Content.Load<Song>("Sounds/travaUDomaPlus");
        }

        protected override void Update(GameTime gameTime)
        {
            keyBoardCurrent = Keyboard.GetState();
            switch (state)
            {
                case State.SplashScreen:
                    MediaPlayer.Play(gameSong);
                    splashScreen.Update();

                    if (keyBoardCurrent.IsKeyDown(Keys.W) && keyBoardOld.IsKeyUp(Keys.W))
                    {
                        menuSound.Play();
                        splashScreen.OptionsCounter--; 
                    }

                    if (keyBoardCurrent.IsKeyDown(Keys.S) && keyBoardOld.IsKeyUp(Keys.S))
                    {
                        menuSound.Play();
                        splashScreen.OptionsCounter++;
                    }

                    if (keyBoardCurrent.IsKeyDown(Keys.Enter) && keyBoardOld.IsKeyUp(Keys.Enter))
                    {
                        
                        switch (splashScreen.MenuState)
                        {
                            case MenuState.New:
                                enterSound.Play();
                                StartNewGame();
                                state = State.Game;
                                break;

                            case MenuState.Resume:
                                if (splashScreen.StartedAtFirstTime == false)
                                {
                                    enterSound.Play();
                                    state = State.Game; 
                                }
                                break;

                            case MenuState.Exit:
                                enterSound.Play();
                                Exit();
                                break;
                        }
                    }

                    break;

                case State.Game:
                    if (Heart <= 0)
                    {
                        //state = State.GameOver;
                        enterSound.Play();
                        splashScreen.StartedAtFirstTime = true;
                        splashScreen.LastScore = Score;
                        if (Score > splashScreen.BestScore || splashScreen.BestScore == 0)
                            splashScreen.BestScore = Score;
                        splashScreen.OptionsCounter = 1;
                        state = State.SplashScreen;
                    }

                    if (keyBoardCurrent.IsKeyDown(Keys.Escape) && keyBoardOld.IsKeyUp(Keys.Escape)) 
                    {
                        enterSound.Play();
                        splashScreen.OptionsCounter = 2;
                        state = State.SplashScreen;
                    }
                    foreach (var star in stars)
                        star.Update();

                    if (bottle.WasEaten)
                    {
                        bottle.SetPosition(Position.ComputePositionForBottle(container));
                        bottle.WasEaten = false;
                    }

                    if (keyBoardCurrent.IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
                        spaceShip.Left();

                    if (keyBoardCurrent.IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
                        spaceShip.Right();

                    if (keyBoardCurrent.IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                        spaceShip.Up();

                    if (keyBoardCurrent.IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                        spaceShip.Down();

                    if (keyBoardCurrent.IsKeyDown(Keys.LeftShift) && keyBoardOld.IsKeyUp(Keys.LeftShift) &&  countFires > 0)
                    {
                        fireSound.Play();
                        fires.Add(new Fire(
                            textureFire,
                            new Rectangle(
                                spaceShip.X + 95,
                                spaceShip.Y + 25,
                                textureFire.Width,
                                textureFire.Height
                                )
                            ));
                        countFires--;
                    }

                    if (keyBoardCurrent.IsKeyDown(Keys.Tab) && keyBoardOld.IsKeyUp(Keys.Tab) && Score >= 5)
                    {
                        buySound.Play();
                        countFires += 10;
                        Score -= 5;
                    }

                    if (spaceShip.Rectangle.Intersects(bottle.Rectangle))
                    {
                        bottleSound.Play();
                        bottle.WasEaten = true;
                        Score++;
                        countAsteroids++;
                    }

                    if (countAsteroids >= 2)
                    {
                        var asteroidSize = Position.GetRandomInt(
                            textureAsteroid.Width / 5,
                            (int)(textureAsteroid.Width * 1.5)
                            );
                        asteroids.Add(new Asteroid(
                        textureBoom,
                        textureAsteroid,
                        new Rectangle(
                            0,
                            0,
                            asteroidSize,
                            asteroidSize
                            ),
                        Position.CumputePositionForAsteroid(container),
                        container
                        ));
                        countAsteroids = 0;
                    }

                    foreach (var asteroid in asteroids)
                    {
                        asteroid.Update();
                        foreach (var fire in fires)
                        {
                            if (fire.Rectangle.Intersects(asteroid.Rectangle)) 
                            {
                                boomSound.Play();
                                fire.Intersected = true;
                                asteroid.Intersected = true;
                            }
                        }
                        if (spaceShip.Rectangle.Intersects(asteroid.Rectangle) && asteroid.Intersected == false)
                        {
                            intersectedSound.Play();
                            Score -= 2;
                            Heart -= 1;
                            asteroid.Intersected = true;
                        }
                    }

                    foreach (var fire in fires.Reverse<Fire>())
                    {
                        if (fire.IsOutOfScreen(container))
                            fires.Remove(fire);
                    }

                    break;
            }
            keyBoardOld = keyBoardCurrent;
            base.Update(gameTime);
        }

        private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Volume -= 0.1f;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            switch (state)
            {
                case State.SplashScreen:
                    splashScreen.Draw(_spriteBatch, _graphics);
                    break;

                case State.Game:

                    foreach (var star in stars)
                    {
                        _spriteBatch.Draw(Star.Texture, star.PositionVector, star.Color);
                    }

                    _spriteBatch.DrawString(
                        copyrightFont, 
                        $"Hearts: {Heart} | Bottle: {Score} | Fires {countFires}",
                        new Vector2(10, 5),
                        Color.White
                        );

                    spaceShip.Draw(gameTime, _spriteBatch);

                    _spriteBatch.Draw(bottle.Texture, bottle.Rectangle, Color.White);

                    foreach (var asteroid in asteroids)
                    {
                        _spriteBatch.Draw(
                            asteroid.Texture,
                            asteroid.Rectangle,
                            Color.White
                            );
                    }

                    foreach (var fire in fires)
                    {
                        fire.Update();
                        _spriteBatch.Draw(
                            fire.Texture,
                            fire.Rectangle,
                            null,
                            Color.White,
                            0f,
                            new Vector2(fire.Texture.Width / 2, fire.Texture.Height / 2),
                            SpriteEffects.None,
                            0f
                            );
                    }

                    foreach (var asteroid in asteroids.Reverse<Asteroid>())
                    {
                        if (asteroid.Intersected)
                            asteroids.Remove(asteroid);
                    }

                    break;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void StartNewGame()
        {
            if (splashScreen.StartedAtFirstTime)
                splashScreen.StartedAtFirstTime = false;

            spaceShip.X = 0;
            spaceShip.Y = _graphics.PreferredBackBufferHeight / 2;

            countAsteroids = 0;
            asteroids.Clear();

            fires.Clear();
            countFires = 10;

            score = 0;
            heart = 3;

            foreach (var star in stars)
            {
                star.RandomSet();
            }
        }
    }
}
