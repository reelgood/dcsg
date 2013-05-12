using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace dcsg.Engine
{
    /// <summary>
    /// The initializer is used to create instances that "are static" but need one instance to control it trough the game. Theese can be created here.
    /// </summary>
    internal class Initializer
    {
        static bool _init = false;
        static Textures _textureHandlerObject;
        static Time _timeHandlerObject;
        static Inputhandler _ihObject;
        static Screen _firstScreen;
        static Sounds _sounds;
        static Debug _debugger;
        public static void Initialize()
        {
            if (_init) { return; }
            //***********************
            //Initialize Stuff here:
            Fonts.LoadFont("std_small");
            _debugger = new Debug();

            _ihObject = new Inputhandler();
            _textureHandlerObject = new Textures();
            _timeHandlerObject = new Time();
            _sounds = new Sounds();
            Fonts.LoadFont("mainfont");
            Fonts.LoadFont("std");

            _firstScreen = new Game.Aleks.Mainmenu();

            //Don't initialize stuff here:
            //***********************

            BindKeys();
            _init = true;
        }
        static void BindKeys()
        {
            Inputhandler.BindKey(Keybindmode.KEYDOWN, Keys.Escape, delegate(Keybindmode kbm) { DCSG.MainObject.Exit(); });
        }
    }
}
