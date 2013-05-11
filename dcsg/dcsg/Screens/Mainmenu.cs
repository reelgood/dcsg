using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dcsg.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace dcsg.Screens
{
    public class Mainmenu : Screen
    {
        public Mainmenu()
            : base()
        {
        }
        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }
        protected override void InternalDraw(GameTime gameTime)
        {
            sb.WriteString(Fonts.GetFont("mainfont"), "DCSG", (DCSG.ScreenWidth / 2) - (Fonts.GetFont("mainfont").LengthOfString("DCSG", 2f) / 2), 100, Color.Salmon, 2f);
            sb.WriteString(Fonts.GetFont("std"), "Dungeon Crawler Survival Game", (DCSG.ScreenWidth / 2) - (Fonts.GetFont("std").LengthOfString("Dungeon Crawler Survival Game", 2f) / 2), 230, Color.Red, 2f);
            base.InternalDraw(gameTime);
        }
    }
}
