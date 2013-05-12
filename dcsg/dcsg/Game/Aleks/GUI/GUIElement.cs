using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dcsg.Game.Aleks.GUI
{
    public abstract class GUIElement
    {
        Rectangle _bounds;
        bool _mouseFunctions;
        bool _isMouseIn = false;
        int _mbc = 0;

        protected GUIElement(Rectangle bounds, bool mouseFunctions)
        {
            _bounds = bounds;
            _mouseFunctions = mouseFunctions;
            DCSG.OnUpdate += new DCSG.XNAHookEvent(_update);
            DCSG.OnDraw += new DCSG.XNADrawEvent(_draw);
        }
        public virtual void Destroy()
        {
            DCSG.OnUpdate -= new DCSG.XNAHookEvent(_update);
            DCSG.OnDraw -= new DCSG.XNADrawEvent(_draw);
        }
        protected virtual void _draw(SpriteBatch sb) { }

        /// <summary>
        /// Basic update function received from DCSG.
        /// Base uses update to check for mousefunctions (like mousehover, mousein, mouseclicks, and mouse out)
        /// </summary>
        protected virtual void _update()
        {
            if (_mouseFunctions)
            {
                _mbc = 0;
                if (dcsg.Engine.Inputhandler.MouseClick(3)) { _mbc = 3; }
                if (dcsg.Engine.Inputhandler.MouseClick(2)) { _mbc = 2; }
                if (dcsg.Engine.Inputhandler.MouseClick(1)) { _mbc = 1; }
                if (_isMouseIn)
                {
                    if (_bounds.Contains(dcsg.Engine.Inputhandler.Mousepoint))
                    {
                        if (_mbc > 0) {
                            _onClick(_mbc);
                        }
                        _onMouseHover();
                    }
                    else
                    {
                        _isMouseIn = false;
                        _onMouseOut();
                    }
                }
                else
                {
                    if (_bounds.Contains(dcsg.Engine.Inputhandler.Mousepoint))
                    {
                        _isMouseIn = true;
                        _onMouseIn();
                    }
                }
            }
        }
        protected virtual void _onClick(int button) { }
        protected virtual void _onMouseIn() { }
        protected virtual void _onMouseHover() { }
        protected virtual void _onMouseOut() { }
    }
}
