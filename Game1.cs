using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
                Position.ComputePosition(container)
                );

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
                        heart.SetPosition(Position.ComputePosition(container));
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
                    break;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
