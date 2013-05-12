using System;
using System.Collections.Generic;
using System.Linq;
using dcsg.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace dcsg
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DCSG : Microsoft.Xna.Framework.Game
    {

        #region Private Variable Declarations
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static DCSG _mgo;
        static int _fps = 100;
        GameObject mainCamera;
        #endregion

        #region Public Variable Declarations
        public delegate void XNAHookEvent();
        public delegate void XNADrawEvent(SpriteBatch sb);
        public static event XNAHookEvent OnUpdate;
        public static event XNADrawEvent OnDraw;

        public static DCSG MainObject { get { return _mgo; } }
        public static ContentManager Contents { get { return _mgo.Content; } }
        public static int TargetFrameRate { get { return _fps; } set { _fps = value; } }
        public static int ScreenWidth { get { return DCSG.MainObject.GraphicsDevice.Viewport.Width; } }
        public static int ScreenHeight { get { return DCSG.MainObject.GraphicsDevice.Viewport.Height; } }
        #endregion

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
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Initializer.Initialize(); //Initialize Everything

			mainCamera = new GameObject("Main Camera");
			mainCamera.AddComponent(typeof(Camera));
        }
        protected override void UnloadContent()
        {
            
        }
        protected override void Update(GameTime gameTime)
        {
            if (OnUpdate != null) { OnUpdate(); }

            if ((double)(10000000.0 / (double)_fps) - (double)gameTime.ElapsedGameTime.Ticks > 0.0)
                System.Threading.Thread.Sleep(new TimeSpan((long)((10000000.0 / (double)_fps) - (double)gameTime.ElapsedGameTime.Ticks)));

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (OnDraw != null) { OnDraw(spriteBatch); }
			base.Draw(gameTime);
		}
	}
}
