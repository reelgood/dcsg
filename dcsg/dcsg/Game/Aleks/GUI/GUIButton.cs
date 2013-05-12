using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dcsg.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dcsg.Game.Aleks.GUI
{
    public class GUIButton : GUIElement
    {
        public GUIButton(Rectangle BoundingArea)
            : base(BoundingArea, true)
        {

        }
        protected override void _onMouseIn()
        {
            Debug.DebugLog("Mouse Went In");
            base._onMouseIn();
        }
        protected override void _onClick(int button)
        {
            Debug.DebugLog("Mouse Clicked");
            base._onClick(button);
        }
        protected override void _onMouseOut()
        {
            Debug.DebugLog("Mouse Went Out");
            base._onMouseOut();
        }
    }
}
