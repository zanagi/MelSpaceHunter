using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MelSpaceHunter
{
    /// <summary>
    /// Handles mouse and keyboard input
    /// </summary>
    class InputManager
    {
        private static MouseState mouseState, prevMouseState;
        private static KeyboardState keyboardState, prevKeyboardState;
        private static Vector2 mousePos;

        //Update keyboard and mouse states, (and mouse position)
        public static void Update(GameTime gameTime)
        {
            prevKeyboardState = keyboardState;
            prevMouseState = mouseState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            mousePos = new Vector2(mouseState.X, mouseState.Y);
        }

        //Access input data from static methods

        public static Vector2 MousePosition
        {
            get { return mousePos; }
        }

        public static bool MouseClicked()
        {
            return prevMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released;
        }

        public static bool MousePressed()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }
        
        public static bool KeyTapped(Keys key)
        {
            return prevKeyboardState.IsKeyDown(key) && keyboardState.IsKeyUp(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }
    }
}
