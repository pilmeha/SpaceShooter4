﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        public static Container Container { get; set; }
        public static Texture2D Texture { get; set; }
        

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
            Globals.spriteBatch.Draw(Star.Texture, positionVector, Color);
        }

        public void RandomSet()
        {
            speed = Position.GetRandomInt(1, 4);
            positionVector = new Vector2(
                Position.GetRandomInt(Container.Width.X2, Container.Width.X2 + 300), 
                Position.GetRandomInt(0, Container.Height.X2)
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
