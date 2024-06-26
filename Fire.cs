﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter3.game;

namespace SpaceShooter3
{
    internal class Fire
    {
        private int speed = 5;
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
        public bool Intersected { get; set; } = false;

        public Fire(Rectangle rectangle)
        {
            Rectangle = rectangle;
        }

        public bool IsOutOfScreen()
        {
            if (X > Globals.Container.Width.X2 || X < Globals.Container.Width.X1 || Y > Globals.Container.Height.X2 || Y < Globals.Container.Height.X1 || this.Intersected)
                return true;
            return false;
        }

        public void Update()
        {
            X += speed;
        }
    }
}
