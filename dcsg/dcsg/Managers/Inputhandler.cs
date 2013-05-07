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
        static MouseState ms;
        static KeyboardState ks;
        static Point _mp;
        public static Point Mousepoint { get { return _mp; } }
        public static void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            ms = Mouse.GetState();
            ks = Keyboard.GetState();
            _mp.X = ms.X;
            _mp.Y = ms.Y;
        }
    }
}
