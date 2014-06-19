using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hoover
{
	public class Miner : Sprite
	{
		public Miner ()
		{
			_assetName = "Miner";
			_Position = new Vector2 (25, 20);
			_Velocity = new Vector2 (2, 2);
			_Facing = SpriteFacing.Down;
		}

		new public void Draw(SpriteBatch spriteBatch)
		{	
			// We only want to draw the hoover that's facing in the direction we're moving
			Rectangle rect = new Rectangle ();
			if (_Facing == SpriteFacing.Right) {
				rect = new Rectangle (240, 0, 25, 20);
			} else if (_Facing == SpriteFacing.Down) {
				rect = new Rectangle (0, 40, 25, 20);
			} else if (_Facing == SpriteFacing.Left) {
				rect = new Rectangle (80, 0, 25, 20);
			} else if (_Facing == SpriteFacing.Up) {
				rect = new Rectangle (160, 20, 25, 20);
			}
			spriteBatch.Draw (_texture, _Position, rect, Color.White);
		}

		public void Update (GameTime gmt, Rock[] rocks)
		{
			float close = 1000f;
			int closeID = -1;
			// Find the closest rock
			for (int i = 0; i < rocks.Length; i++) {
				if (Vector2.Distance (_Position, rocks [i].Position) < close) {
					closeID = i;
				}
			}

			if (rocks [closeID].Position.X > (_Position.X - 5)) {
				_Position.X += _Velocity.X;
				_Facing = SpriteFacing.Right;
			} else if (rocks [closeID].Position.X < _Position.X) {
				_Position.X -= _Velocity.X;
				_Facing = SpriteFacing.Left;
			} else if (rocks [closeID].Position.Y < _Position.Y) {
				_Position.Y += _Velocity.Y;
				_Facing = SpriteFacing.Down;
			} else if (rocks [closeID].Position.Y > _Position.Y) {
				_Position.Y -= _Velocity.Y;
				_Facing = SpriteFacing.Up;
			}
		}

		#region implemented abstract members of Sprite

		public override void Update (GameTime gmt)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

