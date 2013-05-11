using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using dcsg.Managers;

namespace dcsg.gObj
{
	public class SpriteRenderer : Component
	{
		private Texture2D _sprite;
		private Rectangle _sourceRect = Rectangle.Empty;
		private Color _color = Color.White;
		private Vector2 _origin = Vector2.Zero;
		private SpriteEffects _spriteEffects = SpriteEffects.None;
		private LayerDepth _layerDepth = LayerDepth.Floor;
		
		public void SetSprite(Texture2D sprite)
		{
			if (_sprite != null) _sprite = null;
			_sprite = sprite;
			_sourceRect = sprite.Bounds;

			_origin.Y = _sourceRect.Height / 2;
			_origin.X = _sourceRect.Width / 2;
		}


		public override void Start()
		{
			_sprite = Textures.NullTexture;
			_sourceRect = _sprite.Bounds;

			_origin.Y = _sourceRect.Height / 2;
			_origin.X = _sourceRect.Width / 2;
		}

		public void Draw(SpriteBatch sBatch)
		{
			// Cant render if there is no main camera
			if (Camera.mainCamera == null) return;
			if (_sprite == null) return;
			
			// TODO: offset draw by the camera position
			sBatch.Draw(_sprite,
						transform.worldPosition - Camera.mainCamera.cameraPosition, // - camera position? * zoom level?
						_sourceRect,
						_color,
						transform.rotation, // - camera rotation?
						_origin,
						transform.scale, // Should add zooming, although that needs to increase speed aswell
						_spriteEffects,
						(float)_layerDepth);
		}


		public override void OnDestroy()
		{
			_sprite = null;
		}
	}
}
