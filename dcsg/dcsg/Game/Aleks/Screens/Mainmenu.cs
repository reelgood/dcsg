using System;
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
        float _secCount = 0;
        public Mainmenu()
            : base()
        {
        }
        protected override void Update()
        {
            _secCount += Time.DeltaTime;
            if (Inputhandler.MouseClick(1)) { Sounds.SimplePlay("gen_click"); }
            base.Update();
        }
        protected override void InternalDraw()
        {
            sb.WriteString(Fonts.GetFont("mainfont"), "The Crust", (DCSG.ScreenWidth / 2) - (Fonts.GetFont("mainfont").LengthOfString("The Crust", 2f) / 2), 100, Color.Red, 2f);
            sb.WriteString(Fonts.GetFont("std"), "Dungeon Crawler Survival Game", (DCSG.ScreenWidth / 2) - (Fonts.GetFont("std").LengthOfString("Dungeon Crawler Survival Game", 2f) / 2), 230, Color.Salmon, 2f);
            //sb.WriteString(Fonts.GetFont("std"), "DTC: " + _secCount.ToString(), 5, 5, Color.White, 2f);
            //sb.WriteString(Fonts.GetFont("std"), "ETC: " + Time.ElapsedTime.ToString(), 5, 55, Color.White, 2f);
            base.InternalDraw();
        }
    }
}