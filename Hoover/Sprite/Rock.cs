using System;
using Microsoft.Xna.Framework;

namespace Hoover
{
	public class Rock : Sprite
	{
		public Rock (Vector2 position)
		{
			_assetName = "Rock";
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

