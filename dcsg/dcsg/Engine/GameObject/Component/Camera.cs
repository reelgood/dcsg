using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace dcsg.Engine
{
	public class Camera : Component
	{
		static public Camera mainCamera;

		private Vector2 _cameraPosition = Vector2.Zero;
		public Vector2 cameraPosition { get { return _cameraPosition; } }

		public override void Start()
		{
			// Set up main camera, if we dont have one
			if (mainCamera == null) mainCamera = this;
		}
		
		public override void Update()
		{
			// Need to offset the camera transform by half the screen on X and Y, so 0, 0 is center screen.
			Vector2 offSet = new Vector2(DCSG.MainObject.GraphicsDevice.Viewport.Width / 2, DCSG.MainObject.GraphicsDevice.Viewport.Height / 2);
			_cameraPosition = transform.worldPosition - offSet;
		}
	}
}
