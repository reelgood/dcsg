using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace dcsg.Engine
{
    public class Screen
    {
        protected SpriteBatch sb;
        PointerType _pointer;
        static Screen _activescreen;
        public static Screen ActiveScreen { get { return _activescreen; } }
        public Screen()
        {
            _pointer = PointerType.DEFAULT;
            sb = new SpriteBatch(DCSG.MainObject.GraphicsDevice);
            DCSG.OnUpdate += new DCSG.XNAHookEvent(Update);
            DCSG.OnDraw += new DCSG.XNADrawEvent(Draw);
        }
        protected virtual void Update() { }
        void Draw(SpriteBatch _sb)
        {
            sb.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, DCSG.MainObject.GraphicsDevice.DepthStencilState, DCSG.MainObject.GraphicsDevice.RasterizerState);
            InternalDraw();
            sb.End();
        }
        protected virtual void InternalDraw()
        {
            MouseState ms = Mouse.GetState();
            float scale = 2f;
            int xOffset = 0;
            int yOffset = 0;
            Texture2D textToDraw;
            switch (_pointer)
            {
                default:
                    //Change to pointer... just null for now.
                    textToDraw = Textures.pointerDefault; xOffset = -2;
                    break;
            }
            sb.Draw(textToDraw, new Rectangle(ms.X + (int)((float)xOffset * scale), ms.Y + yOffset, (int)((float)textToDraw.Width * scale), (int)((float)textToDraw.Height * scale)), Color.White);
        }
    }
}
