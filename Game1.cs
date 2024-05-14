using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Win32;


namespace SpaceShooter3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private State state = State.SplashScreen;

        Texture2D splashScreenTexture;
        SpriteFont splachScreenFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            splachScreenFont = Content.Load<SpriteFont>("Fonts/splashScreenFont");
            SplashScreen.Font = splachScreenFont;
        }



        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch (state)
            {
                case State.SplashScreen:

                    SplashScreen.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)) state = State.Game;
                    break;

                case State.Game:

                    break;

            }


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
            }
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
