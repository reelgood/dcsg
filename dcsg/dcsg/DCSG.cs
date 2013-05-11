using System;
using System.Collections.Generic;
using System.Linq;
using dcsg.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using dcsg.gObj;


namespace dcsg
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DCSG : Microsoft.Xna.Framework.Game
    {
        //PRIVATES
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static DCSG _mgo;
        Screen _loadedScreen;
        GameObject mainCamera;

        //PUBLICS
        public Screen ActiveScreen { get { return _loadedScreen; } }
        public static DCSG MainObject { get { return _mgo; } }
        public static ContentManager Contents { get { return _mgo.Content; } }
        public static int ScreenWidth { get { return DCSG.MainObject.GraphicsDevice.Viewport.Width; } }
        public static int ScreenHeight { get { return DCSG.MainObject.GraphicsDevice.Viewport.Height; } }

        public DCSG()
        {
            if (_mgo != null)
                throw new InstancePlayLimitException("MainGame started twice. This is not possible!");
            _mgo = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
			Textures.Initialize();
            Inputhandler.BindKey(Managers.Keybindmode.KEYUP, Keys.Escape, delegate(Keybindmode kbm) { this.Exit(); });
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _loadedScreen = new dcsg.Screens.Mainmenu();
            Fonts.LoadFont("mainfont");
            Fonts.LoadFont("std");

			mainCamera = new GameObject("Main Camera");
			mainCamera.AddComponent(typeof(Camera));
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            Inputhandler.Update(gameTime); //Inputhandling first
            if (_loadedScreen != null) { _loadedScreen.Update(gameTime); }
			GameObjectBase.Update();

            if (50000 - gameTime.ElapsedGameTime.Ticks > 0)
                System.Threading.Thread.Sleep(new TimeSpan(50000 - gameTime.ElapsedGameTime.Ticks));

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (_loadedScreen != null) { _loadedScreen.Draw(gameTime); }
			GameObjectBase.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
