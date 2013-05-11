using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace dcsg.Engine
{
	public class Transform : Component
	{
		#region Fields
		private List<Transform> _children = new List<Transform>();
		private Vector2 _position = Vector2.Zero;
		private Vector2 _scale = Vector2.One;
		private float _rotation = 0;
		private Transform _parent;
		#endregion

		#region Properties
		public Transform parent
		{
			get
			{
				return _parent;
			}
			set
			{
				// If parent is set to null, we have no parent.
				if (value == null)
				{
					// Check if we have a parent
					if (_parent != null)
					{
						// Reset our position to world position
						this._position = this._position + _parent.worldPosition;
						
						// Remove us from parents child list
						if (_parent._children.Contains(this))
						{
							_parent._children.Remove(this);
						}
						// Set parent to nothing
						_parent = null;
					}
				}
				else
				{
					// If we have a parent, we need to remove us from its list
					if (_parent != null)
					{
						// Reset position since we're unparenting
						this._position = this._position + _parent.worldPosition;

						// Remove us from parents child list
						if (_parent._children.Contains(this))
						{
							_parent._children.Remove(this);
						}
						
						// Set parent to nothing
						_parent = null;
					}
					
					// Set our parent to the new value
					_parent = value;
					
					// If it contains us, dont add us to the list
					if (!_parent._children.Contains(this))
					{
						_parent._children.Add(this);
					}

					// Update position so that our position is local to parent
					this._position = this._position - _parent.worldPosition;
				}
			}
		}
		public Vector2 worldPosition
		{
			get
			{
				// If we have a parent, we need to add its world position to our own
				// This in turn runs this property again, if our parent has a parent
				if (_parent != null)
				{
					return _parent.worldPosition + _position;
				}
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
		#endregion

		#region Methods
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
		}
		#endregion
	}
}
