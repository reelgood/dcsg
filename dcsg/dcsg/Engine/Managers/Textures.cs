using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dcsg.Engine
{
    public class Textures
    {
        static Textures mainTex;
        public Textures()
        {
            if (mainTex != null) { throw new NullReferenceException("Textures object created twice"); }

            mainTex = this;
            NullTexture = new Texture2D(DCSG.MainObject.GraphicsDevice, 1, 1);
            pointerDefault = DCSG.Contents.Load<Texture2D>("Textures\\pointer");
        }
        public static Texture2D pointerDefault;
        public static Texture2D NullTexture;
    }
}

/*
 * Load inn all big texture files
 * Read XML documentation for what sprite goes where, aswell as animation information
 * Assign Texture and AnimationTexture classes based on XML documentation
 * Need a list/hashtable of all textures
*/
