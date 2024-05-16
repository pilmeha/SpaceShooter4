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
        }

        protected override void Update(GameTime gameTime)
        {
            keyBoardCurrent = Keyboard.GetState();
            switch (state)
            {
                case State.SplashScreen:
                    SplashScreen.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)) 
                        state = State.Game;

                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && keyBoardOld.IsKeyUp(Keys.Escape))
                        Exit();
                    break;

                case State.Game:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && keyBoardOld.IsKeyUp(Keys.Escape)) 
                        state = State.SplashScreen;

                    if (heart.WasEaten)
                    {
                        heart.SetPosition(Position.ComputePositionForHeart(container));
                        heart.WasEaten = false;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
                        spaceShip.Left();

                    if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
                        spaceShip.Right();

                    if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                        spaceShip.Up();

                    if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                        spaceShip.Down();

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
                        if (spaceShip.Rectangle.Intersects(asteroid.Rectangle) && asteroid.Intersected == false)
                        {
                            Score -= 2;
                            asteroid.Intersected = true;
                        }
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
                        $"Hearts count: {Score}",
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
