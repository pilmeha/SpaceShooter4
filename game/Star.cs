﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter3.game;

namespace SpaceShooter3
{
    internal class Star
    {
        private Vector2 positionVector;
        public Vector2 PositionVector
        {
            get
            {
                return positionVector;
            }
            set
            {
                positionVector = value;
            }
        }
        public Color Color { get; set; }
        private int speed;    

        public Star()
        {
            RandomSet();
        }

        public void Update()
        {
            positionVector.X -= speed;
            if (positionVector.X < 0)
                RandomSet();
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(Art.StarTexture, positionVector, Color);
        }

        public void RandomSet()
        {
            speed = Position.GetRandomInt(1, 4);
            positionVector = new Vector2(
                Position.GetRandomInt(Globals.Container.Width.X2, Globals.Container.Width.X2 + 300), 
                Position.GetRandomInt(0, Globals.Container.Height.X2)
                );

            Color = Color.FromNonPremultiplied(
                Position.GetRandomInt(0, 256), 
                Position.GetRandomInt(0, 256),
                Position.GetRandomInt(0, 256),
                Position.GetRandomInt(0, 256)
                );
        }
    }
}
