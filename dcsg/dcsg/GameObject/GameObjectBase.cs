using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace dcsg.gObj
{
	public class GameObjectBase
	{
		static private Queue<GameObject> deleteQueue = new Queue<GameObject>();
		
		protected bool taggedForDestruction = false;
		protected static List<GameObject> _gameObjects = new List<GameObject>();
		protected GameObjectBase()
		{
			_gameObjects.Add((GameObject)this);
		}
		
		protected virtual void _internalUpdate() { }
		protected virtual void _internalDraw(SpriteBatch sBatch) { }
		protected virtual void _onDraw() { }
		
		static public void Update()
		{
			for (int i = 0; i < _gameObjects.Count; i++)
			{
				_gameObjects[i]._internalUpdate();

				if (_gameObjects[i].taggedForDestruction)
				{
					// destroy it and remove from list.
					deleteQueue.Enqueue(_gameObjects[i]);
				}
			}

			while (deleteQueue.Count > 0)
			{
				_gameObjects.Remove(deleteQueue.Dequeue());
			}

		}

		static public void Draw(SpriteBatch sBatch)
		{
			sBatch.Begin();
			for (int i = 0; i < _gameObjects.Count; i++)
				_gameObjects[i]._internalDraw(sBatch);
			sBatch.End();

			for (int i = 0; i < _gameObjects.Count; i++) _gameObjects[i]._onDraw();
		}

	}
}
