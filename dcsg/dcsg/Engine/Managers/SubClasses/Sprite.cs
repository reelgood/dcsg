using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace dcsg.Engine
{

	public class Sprite
	{
		private bool _isAnimation;
		private bool _isUniform;
		private int _numFrames;
		private float _animationSpeed;

		private Texture2D _baseTexture;
		private Rectangle _sourceRect;
		private Vector2 _origin;

		private AnimationFrame[] _animFrames;
		public int Frame { get { return _currentFrame; } set { _currentFrame = value; _frameTime = 0; } }


		/// <summary>
		/// Creates a new sprite, without any animation information
		/// </summary>
		/// <param name="baseTexture">The base texture file this sprite is located on</param>
		/// <param name="SourceRect">The source rectangle of the sprite</param>
		/// <param name="Origin">The origin of the sprite</param>
		public Sprite(Texture2D baseTexture, Rectangle SourceRect, Vector2 Origin)
		{
			this._baseTexture = baseTexture;
			this._sourceRect = SourceRect;
			this._origin = Origin;

			this._isAnimation = false;
			this._isUniform = false;

			this._animationSpeed = 0;
			this._numFrames = 0;
			this._animFrames = null;
		}

		/// <summary>
		/// Creates a new sprite with a uniform animation
		/// </summary>
		/// <param name="baseTexture">The base texture file this sprite is located on</param>
		/// <param name="SourceRect">The source rectangle of the first frame</param>
		/// <param name="Origin">The origin of the first frame</param>
		/// <param name="NumFrames">The number of frames in the animation</param>
		/// <param name="AnimationSpeed">The speed of the animation, measured in frame/second</param>
		public Sprite(Texture2D baseTexture, Rectangle SourceRect, Vector2 Origin, int NumFrames, float AnimationSpeed)
		{
			this._baseTexture = baseTexture;
			this._sourceRect = SourceRect;
			this._origin = Origin;
			this._numFrames = NumFrames;
			if (NumFrames <= 1) throw new Exception("Need more then 1 frame in a animation");
			this._animationSpeed = AnimationSpeed;

			this._isAnimation = true;
			this._isUniform = true;
			this._animFrames = null;
		}

		/// <summary>
		/// Creates a new sprite with a non-uniform animation
		/// </summary>
		/// <param name="baseTexture">The base texture file this sprite is located on</param>
		/// <param name="animationFrames">Datavalues of all the animation frames</param>
		/// <param name="AnimationSpeed">The speed of the animation, measured in frames/second</param>
		public Sprite(Texture2D baseTexture, AnimationFrame[] animationFrames, float AnimationSpeed)
		{
			this._baseTexture = baseTexture;
			this._animationSpeed = AnimationSpeed;
			this._animFrames = animationFrames;

			this._numFrames = _animFrames.Length;
			if (_numFrames <= 1) throw new Exception("Need more then 1 frame in a animation");

			this._isAnimation = true;
			this._isUniform = false;

			this._origin = _animFrames[0].origin;
			this._sourceRect = _animFrames[0].sourceRect;
		}


		private int _currentFrame = 0;
		private float _startTime;
		private float _frameTime;
		private float _timeTilNextFrame;

		private bool isPlaying = false;

		public void Play()
		{
			_startTime = Time.ElapsedTime;
			isPlaying = true;
		}

		public void Pause()
		{
			isPlaying = false;
		}

		public void Restart()
		{

		}

		/// <summary>
		/// Sets the speed of the animation in frames per second.
		/// Can be > 0 for reversing animation
		/// </summary>
		/// <param name="AnimationSpeed"></param>
		public void SetSpeed(float AnimationSpeed)
		{
			
		}

		/// <summary>
		/// Gets the current active frame based on animation speed
		/// </summary>
		/// <returns>The active frame number</returns>
		private int GetCurrentFrame()
		{
			// Check if reversed
			// Check "distance" from "last frame" timestamp
			// Update "Last Frame" timestamp if we return a new frame
			return 0;
		}

		private Rectangle GetRect()
		{
			// If its not uniforme it needs to get rect information from its animationFrames array
			// Else get input sourcerect + sourcerect.width*current framenumber
			// If at max framenumber, it needs to reset framenumber, or ociliate if thats the case

			return Rectangle.Empty;
		}

		private Vector2 GetOrigin()
		{
			// Same as GetRect just with origin.

			return Vector2.Zero;
		}
	}
}
