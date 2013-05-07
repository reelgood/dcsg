using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dcsg.Managers
{
    public static class Textures
    {
        static bool _init = false;
        public static Texture2D pointerDefault;
        public static Texture2D NullTexture;
        public static void Initialize()
        {
            if (_init)
                return;
            _init = true;
            NullTexture = new Texture2D(DCSG.MainObject.GraphicsDevice, 1, 1);
        }
    }
}
