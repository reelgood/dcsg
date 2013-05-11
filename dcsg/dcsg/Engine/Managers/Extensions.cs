using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dcsg.Engine
{
    public static class ExtensionMethods
    {
        public static bool Contains(this Rectangle v, Point point)
        {
            if (point.X > v.X &&
                point.Y > v.Y &&
                point.X < v.X + v.Width &&
                point.Y < v.Y + v.Height)
                return true;
            return false;
        }
        public static void WriteString(this SpriteBatch sb, string writefont, string text, int x, int y, Color color, float scale = 1f)
        {
            WriteString(sb, Fonts.GetFont(writefont), text, x, y, color, scale);
        }
        public static void WriteString(this SpriteBatch sb, Font writefont, string text, int x, int y, Color color, float scale = 1f)
        {
            if (writefont == null) { throw new MissingFieldException("Missing font for WriteString"); }
            writefont.ResetWriter();
            for (int i = 0; i < text.Length; i++)
            {
                FontLetter fn = writefont.GetLetter(text[i]);
                //new Rectangle(x + ((int)Math.Round((float)writefont.XOffset * scale)) + ((int)Math.Round((float)fn.xOff * scale)), y + ((int)Math.Round((float)fn.yOff * scale)), ((int)Math.Round((float)fn.rect.Width * scale)), ((int)Math.Round((float)fn.rect.Height * scale)))
                sb.Draw(writefont.texture, new Rectangle(x + (int)((float)writefont.XOffset * scale) + (int)((float)fn.xOff * scale), y + (int)((float)fn.yOff * scale), (int)((float)fn.rect.Width * scale), (int)((float)fn.rect.Height * scale)), fn.rect, color);
                writefont.AdvanceHead(fn);
            }
        }
    }
}
