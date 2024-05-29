using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShooter3
{
    internal class GameState : Game
    {
        public State state = State.SplashScreen;
        private SplashScreen splashScreen;
        public KeyboardState keyBoardCurrent;
        public KeyboardState keyBoardOld;
        public SoundEffect menuSound;
        public SoundEffect enterSound;
        public void Update(GameTime gameTime)
        {
            keyBoardCurrent = Keyboard.GetState();

            switch (state)
            {
                case State.SplashScreen:
                    splashScreen.Update(this);
                    break;

                case State.Game:

                    break;
            }

            keyBoardOld = keyBoardCurrent;
        }

        public void Draw()
        {
            switch (state)
            {
                case State.SplashScreen:

                    break;

                case State.Game:

                    break;
            }
        }


    }
}
