using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace SpaceShooter3.game
{
    internal class Input
    {
        public static KeyboardState keyboardState { get; private set; }
        public static KeyboardState lastKeyboardState { get; private set; }
        
        public static void Update()
        {
            lastKeyboardState = keyboardState;

            keyboardState = Keyboard.GetState();
        }

        public static bool WasKeyPressed(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }
    }
}
