using Microsoft.Xna.Framework;

namespace dcsg.Engine
{
	public class AnimationFrame
	{
		public Vector2 origin;
		public Rectangle sourceRect;

		public AnimationFrame(Vector2 Origin, Rectangle SourceRect)
		{
			this.origin = Origin;
			this.sourceRect = SourceRect;
		}
	}
}
