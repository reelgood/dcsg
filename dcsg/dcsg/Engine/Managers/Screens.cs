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
        public Screen()
        {
            _pointer = PointerType.DEFAULT;
            sb = new SpriteBatch(DCSG.MainObject.GraphicsDevice);
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, DCSG.MainObject.GraphicsDevice.DepthStencilState, DCSG.MainObject.GraphicsDevice.RasterizerState);
            InternalDraw(gameTime);
            sb.End();
        }
        protected virtual void InternalDraw(GameTime gameTime)
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
