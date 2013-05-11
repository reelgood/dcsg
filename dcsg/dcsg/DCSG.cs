using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using dcsg.gObj;
using dcsg.Managers;


namespace dcsg
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DCSG : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static DCSG _mgo;
        public static DCSG MainObject { get { return _mgo; } }
        public static ContentManager Contents { get { return _mgo.Content; } }
        public DCSG()
        {
            if (_mgo != null)
                throw new InstancePlayLimitException("MainGame started twice. This is not possible!");
            _mgo = this;

			
			graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
			Textures.Initialize();
            base.Initialize();
        }

		GameObject gob;
		GameObject gob2;
		GameObject gob3;

		GameObject mainCamera;

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

			Texture2D testTex = Content.Load<Texture2D>("test2") as Texture2D;
			gob = new GameObject("test");
			gob.AddComponent(typeof(SpriteRenderer));
			gob.renderer.SetSprite(testTex);

			gob2 = new GameObject("test");
			gob2.AddComponent(typeof(SpriteRenderer));
			gob2.renderer.SetSprite(testTex);

			gob3 = new GameObject("test");
			gob3.AddComponent(typeof(SpriteRenderer));
			gob3.renderer.SetSprite(testTex);

			mainCamera = new GameObject("Main Camera");
			mainCamera.AddComponent(typeof(Camera));

			//mainCamera.transform.parent = gob.transform;
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
				gob.transform.Translate(0, -1);

			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
				gob.transform.Translate(-1, 0);

			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
				gob.transform.Translate(0, 1);

			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
				gob.transform.Translate(1, 0);

			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Q))
				gob.transform.Rotate(0.1f);
			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.E))
				gob.transform.Rotate(-0.1f);

			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.F1))
			{
				gob2.transform.parent = gob.transform;
			}
			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.F2))
			{
				gob2.transform.parent = null;
			}
			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.F3))
			{
				gob3.transform.parent = gob2.transform;
			}

			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.F4))
			{
				gob3.transform.parent = null;
			}

			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
			{
				gob2.transform.Translate(0, -1);
			}

			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
			{
				gob2.transform.Translate(0, 1);
			}

			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
			{
				gob2.transform.Translate(-1, 0);
			}

			if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
			{
				gob2.transform.Translate(1, 0);
			}

			GameObjectBase.Update();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
			GameObjectBase.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
