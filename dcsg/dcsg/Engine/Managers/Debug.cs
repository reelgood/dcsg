using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dcsg.Engine
{
    struct debugLine
    {
        public int id;
        public float fadeTime;
        public float startTime;
        public string text;
    }
    public class Debug
    {
        static int idCount = 1;
        static Font debugFont;
        static List<debugLine> debugLines = new List<debugLine>();
        public Debug()
        {
            debugFont = Fonts.GetFont("std_small");
            DCSG.OnDraw += new DCSG.XNADrawEvent(DCSG_OnDraw);
            DCSG.OnUpdate += new DCSG.XNAHookEvent(DCSG_OnUpdate);
        }

        void DCSG_OnUpdate()
        {
            for (int i = 0; i < debugLines.Count; i++)
            {
                if (Time.ElapsedTime > debugLines[i].startTime + debugLines[i].fadeTime)
                {
                    debugLines.RemoveAt(i);
                    i--;
                }
            }
        }
        public static void DebugLog(string text, float fadeTime = 4f)
        {
            debugLine dl = new debugLine() { startTime = Time.ElapsedTime, fadeTime = fadeTime, id = idCount, text = text };
            idCount++;
            debugLines.Insert(0, dl);
        }
        void DCSG_OnDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DCSG.MainObject.GraphicsDevice.DepthStencilState, DCSG.MainObject.GraphicsDevice.RasterizerState);
            int heightOffset = 0;
            for (int i = 0; i < debugLines.Count; i++)
            {
                sb.WriteString(debugFont, debugLines[i].text, 3, heightOffset + 3, Color.White, 2f);
                heightOffset += 18;
            }
            sb.End();
        }
    }
}
