using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace dcsg.gObj
{
	class Transform : Component
	{
		Vector2 _position = Vector2.Zero;
		Vector2 _scale = Vector2.One;
		float _rotation = 0;

		public Vector2 position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}

		public Vector2 scale
		{
			get
			{
				return _scale;
			}
		}

		public float rotation
		{
			get
			{
				return _rotation;
			}
		}

		public void Translate(float x, float y)
		{
			_position.X += x;
			_position.Y += y;
		}

		public void Translate(Vector2 v)
		{
			_position += v;
		}

		public void Rotate(float angle)
		{
			_rotation += angle;
			if (_rotation > 360) _rotation = 0;
			if (_rotation < 0) _rotation = 360;
		}
	}
}
