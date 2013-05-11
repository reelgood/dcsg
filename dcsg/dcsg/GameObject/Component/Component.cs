using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dcsg.gObj
{
	class Component
	{
		public bool enabled = true;

		public Transform transform { get { return _gameObject.transform; } }

		protected GameObject _gameObject = null;

		public GameObject gameObject
		{
			get
			{
				return _gameObject;
			}

			set
			{
				if (_gameObject == null) _gameObject = value;
				else throw new Exception("Cant set gameObject");
			}
		}

		protected Component() { }
		public virtual void Update() { }
		public virtual void Start() { }
		public virtual void OnDestroy() { }
		public virtual void OnDraw() { }
	}
}
