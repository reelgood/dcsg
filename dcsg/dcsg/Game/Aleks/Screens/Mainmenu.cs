﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dcsg.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace dcsg.Game.Aleks
{
    public class Mainmenu : Screen
    {
        public Mainmenu()
            : base()
        {
        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void InternalDraw()
        {
            //sb.WriteString(Fonts.GetFont("mainfont"), "DCSG", (DCSG.ScreenWidth / 2) - (Fonts.GetFont("mainfont").LengthOfString("DCSG", 2f) / 2), 100, Color.Salmon, 2f);
            //sb.WriteString(Fonts.GetFont("std"), "Dungeon Crawler Survival Game", (DCSG.ScreenWidth / 2) - (Fonts.GetFont("std").LengthOfString("Dungeon Crawler Survival Game", 2f) / 2), 230, Color.Red, 2f);
            base.InternalDraw();
        }
    }
}
