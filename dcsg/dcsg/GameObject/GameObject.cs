using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace dcsg.gObj
{
	class GameObject : GameObjectBase
	{
		public bool enabled;
		public string name;
		private List<Component> _components;

		private Transform _transform;
		public Transform transform { get { return _transform; } }

		private SpriteRenderer _renderer;
		public SpriteRenderer renderer { get { return _renderer; } }

		public GameObject(string name)
			: base()
		{
			enabled = true;
			this.name = name;
			_components = new List<Component>();
			AddComponent(typeof(Transform));
		}


		protected override void _internalUpdate()
		{
			if (!enabled) return;
			for (int i = 0; i < _components.Count; i++)
			{
				if (_components[i].enabled == false) continue;
				_components[i].Update();
			}
		}

		protected override void _internalDraw(SpriteBatch sBatch)
		{
			if (!enabled) return;
			if (!renderer.enabled) return;
			renderer.Draw(sBatch);
		}

		protected override void _onDraw()
		{
			if (!enabled) return;
			for (int i = 0; i < _components.Count; i++)
			{
				if (_components[i].enabled == false) continue;
				_components[i].OnDraw();
			}
		}

		static public void Destroy(GameObject gameObject)
		{
			gameObject.taggedForDestruction = true;
			for (int i = 0; i < gameObject._components.Count; i++)
			{
				gameObject._components[i].enabled = false;
				gameObject._components[i].OnDestroy();
			}
			gameObject._components = null;
		}

		public Component AddComponent(Type componentType)
		{
			for (int i = 0; i < _components.Count; i++)
			{
				if (_components[i].GetType() == componentType)
				{
					// Already contains this component
					return _components[i];
				}
			}
			Component c = Activator.CreateInstance(componentType) as Component;
			if (c is Transform) _transform = (Transform)c;
			if (c is SpriteRenderer) _renderer = (SpriteRenderer)c;
			
			_components.Add(c);
			c.gameObject = this;
			c.Start();
			return c;
		}

		public Component GetComponent(Type componentType)
		{
			for (int i = 0; i < _components.Count; i++)
			{
				if (_components.GetType() == componentType)
				{
					return _components[i];
				}
			}
			return null;
		}
	}
}
