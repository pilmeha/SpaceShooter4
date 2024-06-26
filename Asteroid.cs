﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter3.game;

namespace SpaceShooter3
{
    internal class Asteroid
    {
        private int speed = 5;

        public Texture2D Texture 
        {
            get
            {
                if (Intersected)
                    return Art.TextureBoom;
                return Art.TextureAsteroid;
            }     
        }
        public bool Intersected { get; set; } = false;
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                rectangle = value;
            }
        }

        public int X
        {
            get
            {
                return rectangle.X;
            }
            set
            {
                rectangle.X = value;
            }
        }
        public int Y
        {
            get
            {
                return rectangle.Y;
            }
            set
            {
                rectangle.Y = value;
            }
        }

        public Asteroid(Rectangle rectangle, Position position)
        {
            this.rectangle = rectangle;
            X = position.X;
            Y = position.Y;
        }

        public void Update()
        {
            X -= speed;
            if (X < Globals.Container.Width.X1 - 200)
            {
                this.SetPosition(Position.CumputePositionForAsteroid());
                this.Intersected = false;
            }
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        public void SetPosition(Position position)
        {
            X = position.X;
            Y = position.Y;
            var asteroidSize = Position.GetRandomInt(
                Art.TextureAsteroid.Width / 5,
                (int)(Art.TextureAsteroid.Width * 1.5)
                );
            this.rectangle.Width = asteroidSize;
            this.rectangle.Height = asteroidSize;

        }
    }
}
