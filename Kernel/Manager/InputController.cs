using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Kernel
{
    public static class InputController
    {
        private static KeyboardState keyboardState { get { return Keyboard.GetState(); } }

        private static MouseState mouseState { get { return Mouse.GetState(); } }

        private static MouseState lastMouseState { get; set; }

        public static bool GetKeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public static bool GetKeyUp(Keys key)
        {
            return keyboardState.IsKeyUp(key);
        }

        public static bool IsMouseActive()
        {
            return lastMouseState != mouseState;
        }

        public static Vector2 GetMousePosition()
        {
            if (lastMouseState != mouseState)
            {
                lastMouseState = mouseState;
            }

            return new Vector2(mouseState.X, mouseState.Y);
        }

        public static bool GetLeftMouseDown()
        {
            return mouseState.XButton1 == ButtonState.Pressed;
        }

        public static bool GetRightMouseDown()
        {
            return mouseState.XButton2 == ButtonState.Pressed;
        }

        public static bool GetGamePadKeyDown(Buttons button, PlayerIndex playerIndex = PlayerIndex.One)
        {
            var gamePadState = GamePad.GetState(playerIndex);

            return gamePadState.IsButtonDown(button);
        }

        public static bool GetGamePadKeyUp(Buttons button, PlayerIndex playerIndex = PlayerIndex.One)
        {
            var gamePadState = GamePad.GetState(playerIndex);

            return gamePadState.IsButtonUp(button);
        }

        public static Vector2 GetGamePadLeftStickPosition(PlayerIndex playerIndex = PlayerIndex.One)
        {
            var gamePadState = GamePad.GetState(playerIndex);

            return gamePadState.ThumbSticks.Left;
        }

        public static Vector2 GetGamePadRightStickPosition(PlayerIndex playerIndex = PlayerIndex.One)
        {
            var gamePadState = GamePad.GetState(playerIndex);

            return gamePadState.ThumbSticks.Left;
        }
    }
}
