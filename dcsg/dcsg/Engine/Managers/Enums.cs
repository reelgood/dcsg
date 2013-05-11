using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dcsg.Engine
{
    enum PointerType
    {
        DEFAULT
    }
    public enum Keybindmode
    {
        KEYUP,
        KEYDOWN,
        KEYHOLD
    }
	enum LayerDepth
	{
		Floor = 0,
		Walls = 1,
		Player = 2,
		Roof = 3
	}
}
