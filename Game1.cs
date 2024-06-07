using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceShooter3.game;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShooter3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        SpaceShooterGame game = new SpaceShooterGame();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Globals.Load(_graphics);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Art.Load(Content);
            Sound.Load(Content);
            game.Load(_graphics);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            game.Update();

            base.Update(gameTime);
        }

        private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Volume -= 0.1f;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Globals.spriteBatch.Begin();

            game.Draw(gameTime, _graphics);

            Globals.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
