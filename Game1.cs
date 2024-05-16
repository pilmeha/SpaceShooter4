using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private Container container;

        private Texture2D spaceShipTexture;
        private SpaceShip spaceShip;

        private KeyboardState keyBoardCurrent;
        private KeyboardState keyBoardOld;

        private Texture2D heartTexture;
        private Heart heart;

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
        private bool CageIsEnable
        {
            get
            {
                if (countFires > 0)
                    return true;
                return false;
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
            SplashScreen.Backgorund = splashScreenTexture;
            bigFont = Content.Load<SpriteFont>("Fonts/bigFont");
            SplashScreen.BigFont = bigFont;
            smallFont = Content.Load<SpriteFont>("Fonts/smallFont");
            SplashScreen.SmallFont = smallFont;

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

            heartTexture = Content.Load<Texture2D>("Images/heart");
            heart = new Heart(
                heartTexture,
                new Rectangle(
                    0,
                    0,
                    heartTexture.Width,
                    heartTexture.Height
                    ),
                Position.ComputePositionForHeart(container)
                );

            textureBoom = Content.Load<Texture2D>("Images/boomAsteroid");
            textureAsteroid = Content.Load<Texture2D>("Images/asteroid");

            textureFire = Content.Load<Texture2D>("Images/fire");
        }

        protected override void Update(GameTime gameTime)
        {
            keyBoardCurrent = Keyboard.GetState();
            switch (state)
            {
                case State.SplashScreen:
                    SplashScreen.Update();
                    if (keyBoardCurrent.IsKeyDown(Keys.Enter)) 
                        state = State.Game;

                    if (keyBoardCurrent.IsKeyDown(Keys.Escape) && keyBoardOld.IsKeyUp(Keys.Escape))
                        Exit();
                    break;

                case State.Game:
                    if (keyBoardCurrent.IsKeyDown(Keys.Escape) && keyBoardOld.IsKeyUp(Keys.Escape)) 
                        state = State.SplashScreen;

                    if (heart.WasEaten)
                    {
                        heart.SetPosition(Position.ComputePositionForHeart(container));
                        heart.WasEaten = false;
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
                        countFires += 10;
                        Score -= 5;
                    }

                    if (spaceShip.Rectangle.Intersects(heart.Rectangle))
                    {
                        heart.WasEaten = true;
                        Score++;
                        countAsteroids++;
                    }

                    if (countAsteroids >= 2)
                    {
                        asteroids.Add(new Asteroid(
                        textureBoom,
                        textureAsteroid,
                        new Rectangle(
                            0,
                            0,
                            textureAsteroid.Width,
                            textureAsteroid.Height
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
                                fire.Intersected = true;
                                asteroid.Intersected = true;
                            }
                        }
                        if (spaceShip.Rectangle.Intersects(asteroid.Rectangle) && asteroid.Intersected == false)
                        {
                            Score -= 2;
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            switch (state)
            {
                case State.SplashScreen:
                    SplashScreen.Draw(_spriteBatch, _graphics);
                    break;

                case State.Game:
                    _spriteBatch.DrawString(
                        smallFont, 
                        $"Hearts count: {Score} | In flight: {fires.Count} | Count fires {countFires}",
                        new Vector2(10, 5),
                        Color.White
                        );

                    spaceShip.Draw(gameTime, _spriteBatch);

                    _spriteBatch.Draw(heart.Texture, heart.Rectangle, Color.White);

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

                    break;
            }
            _spriteBatch.End();

            foreach (var asteroid in asteroids.Reverse<Asteroid>())
            {
                if (asteroid.Intersected)
                    asteroids.Remove(asteroid);
            }

            base.Draw(gameTime);
        }
    }
}
