﻿using Microsoft.Xna.Framework;
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

        private State state = State.SplashScreen;

        private SpaceShip spaceShip;

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

        private List<Asteroid> asteroids = new List<Asteroid>();
        private int countAsteroids = 0;

        private List<Fire> fires = new List<Fire>();
        private int countFires = 10;

        private Star[] stars = new Star[50];
        private SplashScreen splashScreen = new SplashScreen();

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

            spaceShip = new SpaceShip(new Rectangle(0, 
                    _graphics.PreferredBackBufferHeight / 2,
                    Art.SpaceShipTexture.Width,
                    Art.SpaceShipTexture.Height));

            bottle = new Bottle(new Rectangle(0,
                    0,
                    Art.BottleTexture.Width,
                    Art.BottleTexture.Height),
                Position.ComputePositionForBottle());

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star();
            }

        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            switch (state)
            {
                case State.SplashScreen:
                    try
                    {
                        MediaPlayer.IsRepeating = true;
                        MediaPlayer.Play(Sound.GameSong);
                        MediaPlayer.Volume = 0.6f;
                        MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
                    }
                    catch { }
                    splashScreen.Update();

                    if (Input.keyboardState.IsKeyDown(Keys.W) && Input.lastKeyboardState.IsKeyUp(Keys.W))
                    {
                        Sound.MenuSound.Play();
                        splashScreen.OptionsCounter--; 
                    }

                    if (Input.keyboardState.IsKeyDown(Keys.S) && Input.lastKeyboardState.IsKeyUp(Keys.S))
                    {
                        Sound.MenuSound.Play();
                        splashScreen.OptionsCounter++;
                    }

                    if (Input.keyboardState.IsKeyDown(Keys.Enter) && Input.lastKeyboardState.IsKeyUp(Keys.Enter))
                    {
                        
                        switch (splashScreen.MenuState)
                        {
                            case MenuState.New:
                                Sound.EnterSound.Play();
                                StartNewGame();
                                state = State.Game;
                                break;

                            case MenuState.Resume:
                                if (splashScreen.StartedAtFirstTime == false)
                                {
                                    Sound.EnterSound.Play();
                                    state = State.Game; 
                                }
                                break;

                            case MenuState.Exit:
                                Sound.EnterSound.Play();
                                Exit();
                                break;
                        }
                    }
                    break;

                case State.Game:
                    if (Heart <= 0)
                    {
                        Sound.EnterSound.Play();
                        splashScreen.StartedAtFirstTime = true;
                        splashScreen.LastScore = Score;
                        if (Score > splashScreen.BestScore || splashScreen.BestScore == 0)
                            splashScreen.BestScore = Score;
                        splashScreen.OptionsCounter = 1;
                        state = State.SplashScreen;
                    }

                    if (Input.keyboardState.IsKeyDown(Keys.Escape) && Input.lastKeyboardState.IsKeyUp(Keys.Escape)) 
                    {
                        Sound.EnterSound.Play();
                        splashScreen.OptionsCounter = 2;
                        state = State.SplashScreen;
                    }
                    foreach (var star in stars)
                        star.Update();

                    if (bottle.WasEaten)
                    {
                        bottle.SetPosition(Position.ComputePositionForBottle());
                        bottle.WasEaten = false;
                    }

                    spaceShip.Update();

                    if (Input.keyboardState.IsKeyDown(Keys.LeftShift) && Input.lastKeyboardState.IsKeyUp(Keys.LeftShift) &&  countFires > 0)
                    {
                        Sound.FireSound.Play();
                        fires.Add(new Fire(new Rectangle(spaceShip.X + 95,
                                spaceShip.Y + 25,
                                Art.TextureFire.Width,
                                Art.TextureFire.Height)));
                        countFires--;
                    }

                    if (Input.keyboardState.IsKeyDown(Keys.Tab) && Input.lastKeyboardState.IsKeyUp(Keys.Tab) && Score >= 5)
                    {
                        Sound.BuySound.Play();
                        countFires += 10;
                        Score -= 5;
                    }

                    if (spaceShip.Rectangle.Intersects(bottle.Rectangle))
                    {
                        Sound.BottleSound.Play();
                        bottle.WasEaten = true;
                        Score++;
                        countAsteroids++;
                    }

                    if (countAsteroids >= 2)
                    {
                        var asteroidSize = Position.GetRandomInt(
                            Art.TextureAsteroid.Width / 5,
                            (int)(Art.TextureAsteroid.Width * 1.5)
                            );
                        asteroids.Add(new Asteroid(new Rectangle(0,
                                    0,
                                    asteroidSize,
                                    asteroidSize),
                                Position.CumputePositionForAsteroid()));
                        countAsteroids = 0;
                    }

                    foreach (var fire in fires)
                    {
                        fire.Update();
                    }

                    foreach (var asteroid in asteroids)
                    {
                        asteroid.Update();
                        foreach (var fire in fires)
                        {
                            if (fire.Rectangle.Intersects(asteroid.Rectangle)) 
                            {
                                Sound.BoomSound.Play();
                                fire.Intersected = true;
                                asteroid.Intersected = true;
                            }
                        }
                        if (spaceShip.Rectangle.Intersects(asteroid.Rectangle) && asteroid.Intersected == false)
                        {
                            Sound.IntersectedSound.Play();
                            Score -= 2;
                            Heart -= 1;
                            asteroid.Intersected = true;
                        }
                    }

                    foreach (var fire in fires.Reverse<Fire>())
                    {
                        if (fire.IsOutOfScreen())
                            fires.Remove(fire);
                    }
                    break;
            }
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

            switch (state)
            {
                case State.SplashScreen:
                    splashScreen.Draw(Globals.spriteBatch, _graphics);
                    break;

                case State.Game:

                    foreach (var star in stars)
                    {
                        star.Draw();
                    }

                    Globals.spriteBatch.DrawString(
                        Art.CopyrightFont, 
                        $"Hearts: {Heart} | Bottle: {Score} | Fires {countFires}",
                        new Vector2(10, 5),
                        Color.White
                        );

                    spaceShip.Draw(gameTime, Globals.spriteBatch);

                    Globals.spriteBatch.Draw(Art.BottleTexture, bottle.Rectangle, Color.White);

                    foreach (var asteroid in asteroids)
                    {
                        asteroid.Draw();
                    }

                    foreach (var fire in fires)
                    {
                        fire.Update();
                        Globals.spriteBatch.Draw(
                            Art.TextureFire,
                            fire.Rectangle,
                            null,
                            Color.White,
                            0f,
                            new Vector2(Art.TextureFire.Width / 2, Art.TextureFire.Height / 2),
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

            Globals.spriteBatch.End();
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
