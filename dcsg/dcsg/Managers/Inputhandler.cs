using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace dcsg.Managers
{
    public static class Inputhandler
    {
        static MouseState prev_ms;
        static MouseState ms = Mouse.GetState();
        static KeyboardState ks = Keyboard.GetState();
        static KeyboardState prev_ks;
        static Point _mp;
        public static Point Mousepoint { get { return _mp; } }
        public static void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            prev_ms = ms;
            ms = Mouse.GetState();
            prev_ks = ks;
            ks = Keyboard.GetState();
            _mp.X = ms.X;
            _mp.Y = ms.Y;
        }
        public static bool KeyDown(Keys key) { return (!prev_ks.IsKeyDown(key) && ks.IsKeyDown(key)); }
        public static bool KeyPressed(Keys key) { return ks.IsKeyDown(key); }
        public static bool KeyUp(Keys key) { return (prev_ks.IsKeyDown(key) && !ks.IsKeyDown(key)); }
        public static bool MouseClick(int button)
        {
            switch (button)
            {
                case 1:
                    if (ms.LeftButton == ButtonState.Pressed && prev_ms.LeftButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if (ms.RightButton == ButtonState.Pressed && prev_ms.RightButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if (ms.MiddleButton == ButtonState.Pressed && prev_ms.MiddleButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
    }
}
