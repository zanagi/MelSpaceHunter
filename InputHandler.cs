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
    class InputHandler
    {
        private MouseState mouseState, prevMouseState;
        private KeyboardState keyboardState, prevKeyboardState;
        private Vector2 mousePos;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            prevKeyboardState = keyboardState;
            prevMouseState = mouseState;
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);
        }

        //Access input data from static methods

        public Vector2 MousePosition
        {
            get { return mousePos; }
        }

        public bool MouseClicked()
        {
            return prevMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released;
        }

        public bool MousePressed()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }
        
        public bool KeyTapped(Keys key)
        {
            return prevKeyboardState.IsKeyDown(key) && keyboardState.IsKeyUp(key);
        }

        public bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }
    }
}
