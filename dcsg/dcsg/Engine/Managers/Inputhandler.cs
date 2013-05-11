using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace dcsg.Engine
{
    public class Inputhandler
    {
        static Inputhandler mainIh;
        public Inputhandler()
        {
            if (mainIh != null) { throw new NullReferenceException("Inputhandler object created twice"); }
            DCSG.OnUpdate += new DCSG.XNAHookEvent(Update);
            mainIh = this;
        }

        static MouseState prev_ms;
        static MouseState ms = Mouse.GetState();
        static KeyboardState ks = Keyboard.GetState();
        static KeyboardState prev_ks;
        static Point _mp;
        static Hashtable key_ht = new Hashtable();

        public delegate void KeyCallback(Keybindmode keyMode);
        public static Point Mousepoint { get { return _mp; } }

        void Update()
        {
            prev_ms = ms;
            ms = Mouse.GetState();
            prev_ks = ks;
            ks = Keyboard.GetState();
            _mp.X = ms.X;
            _mp.Y = ms.Y;
            Keys key;
            Hashtable tmpht;
            foreach (DictionaryEntry pair in key_ht)
            {
                key = (Keys)pair.Key;
                tmpht = (Hashtable)pair.Value;
                if (tmpht.Contains(Keybindmode.KEYDOWN)) { if (KeyDown(key)) { ((KeyCallback)tmpht[Keybindmode.KEYDOWN]).Invoke(Keybindmode.KEYDOWN); } }
                if (tmpht.Contains(Keybindmode.KEYUP)) { if (KeyUp(key)) { ((KeyCallback)tmpht[Keybindmode.KEYUP]).Invoke(Keybindmode.KEYUP); } }
                if (tmpht.Contains(Keybindmode.KEYHOLD)) { if (KeyPressed(key)) { ((KeyCallback)tmpht[Keybindmode.KEYHOLD]).Invoke(Keybindmode.KEYHOLD); } }
            }
        }
        public static bool BindKey(Keybindmode kbm, Keys key, KeyCallback callback, bool forceOverride = false)
        {
            bool containK = false;
            if (key_ht.ContainsKey(key))
            {
                containK = true;
                if (((Hashtable)key_ht[key]).ContainsKey(kbm)) {
                    if (!forceOverride) { return false; }
                }
            }
            if (!containK) { key_ht.Add(key, new Hashtable()); }
            ((Hashtable)key_ht[key])[kbm] = callback;
            return true;
        }
        public static bool UnBindKey(Keybindmode kbm, Keys key)
        {
            if (key_ht.ContainsKey(key))
            {
                if (((Hashtable)key_ht[key]).ContainsKey(kbm))
                {
                    ((Hashtable)key_ht[key]).Remove(kbm);
                    if (((Hashtable)key_ht[key]).Count == 0)
                    {
                        key_ht.Remove(key);
                    }
                    return true;
                }
            }
            return false;
        }
        public static bool KeyDown(Keys key) { return (!prev_ks.IsKeyDown(key) && ks.IsKeyDown(key)); }
        public static bool KeyPressed(Keys key) { return ks.IsKeyDown(key); }
        public static bool KeyUp(Keys key) { return (prev_ks.IsKeyDown(key) && !ks.IsKeyDown(key)); }
        public static bool MouseClick(int button)
        {
            switch (button)
            {
                case 1:
                    if (ms.LeftButton == ButtonState.Pressed && prev_ms.LeftButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if (ms.RightButton == ButtonState.Pressed && prev_ms.RightButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if (ms.MiddleButton == ButtonState.Pressed && prev_ms.MiddleButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
    }
}
