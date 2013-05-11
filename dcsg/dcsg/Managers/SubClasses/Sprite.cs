using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace dcsg.Managers.SubClasses
{
	public class Sprite
	{
		bool isAnimation;
		bool unifromAnimation;
		int numFrames;
		float animationSpeed;

		Texture2D baseTexture;
		Rectangle sourceRect;
		Vector2 origin;

		AnimationFrame[] animationFrames;

		// If its a uniform animation, origin, rect etc is the same for each frame, just increased by frame.width


		public void StartAnimation()
		{

		}

		public void StopAnimation()
		{

		}

		public void GetFrame()
		{
		
		}

		public void DrawSprite(SpriteBatch sBatch)
		{

		}
	}
}
