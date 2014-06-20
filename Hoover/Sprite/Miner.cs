using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hoover
{
	/// <summary>
	/// Miner.
	/// </summary>
	public class Miner : Sprite
	{
		int noRocksMined = 0;
		public Miner ()
		{
			_assetName = "Miner";
			_Position = new Vector2 (20, 20);
			_Velocity = new Vector2 (2, 2);
			_Facing = SpriteFacing.Down;
		}

		/// <summary>
		/// Draw the specified sprite.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
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

		public void Update (GameTime gmt, RockManager rocks)
		{
			float close = 10000f;
			int closeID = -1;
			// Find the closest rock
			for (int i = 0; i < rocks.noRocks; i++) {
				if (rocks [i] != null) {
					if (Vector2.Distance (_Position, rocks [i].Position) < close) {
						closeID = i;
					}
				}
			}

			// Quick check that there hasn't been a fuck up
			if (closeID == -1) {
				int i = 0;
				while (rocks[i] != null) {
					closeID = i;
					i++;
				}
			}

			// Move towards the nearest rock
			if ((rocks [closeID].Position.X - _Position.X) > 2) {
				_Position.X += _Velocity.X;
				_Facing = SpriteFacing.Right;
			} else if ((rocks [closeID].Position.X - _Position.X) < 2) {
				_Position.X -= _Velocity.X;
				_Facing = SpriteFacing.Left;
			} else if ((rocks [closeID].Position.Y - _Position.Y) > 2) {
				_Position.Y += _Velocity.Y;
				_Facing = SpriteFacing.Down;
			} else if ((rocks [closeID].Position.Y - _Position.Y) < 2) {
				_Position.Y -= _Velocity.Y;
				_Facing = SpriteFacing.Up;
			}
			// If we're on a rock, mine it!
			else {
				noRocksMined++;
				rocks.removeRock (closeID);
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

