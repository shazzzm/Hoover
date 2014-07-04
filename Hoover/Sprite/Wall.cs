using System;
using Microsoft.Xna.Framework;

namespace Hoover
{
	public class Wall : Sprite
	{
		public Wall (Vector2 position)
		{
			_assetName = "Tech";
			_Position = position;
			_Velocity = Vector2.Zero;
			_textureSize = new Vector2 (20, 20);
		}

		#region implemented abstract members of Sprite

		public override void Update (GameTime gmt)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

