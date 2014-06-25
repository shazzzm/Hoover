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
			_Facing = Direction.Down;
			_textureSize = new Vector2 (25, 20);
		}

		/// <summary>
		/// Draw the specified sprite.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		new public void Draw(SpriteBatch spriteBatch)
		{	
			// We only want to draw the hoover that's facing in the direction we're moving
			Rectangle rect = new Rectangle ();
			if (_Facing == Direction.Right) {
				rect = new Rectangle (240, 0, 25, 20);
			} else if (_Facing == Direction.Down) {
				rect = new Rectangle (0, 40, 25, 20);
			} else if (_Facing == Direction.Left) {
				rect = new Rectangle (80, 0, 25, 20);
			} else if (_Facing == Direction.Up) {
				rect = new Rectangle (160, 20, 25, 20);
			}
			spriteBatch.Draw (_texture, _Position, rect, Color.White);

		}

		public void Update (GameTime gmt, RockManager rocks)
		{
			// If we're out of rocks, just chill
			if (rocks.noRocks == 0) {
				return;
			}

			float close = 100000f;
			int closeID = -1;
			// Find the closest rock
			for (int i = 0; i < rocks.noRocks; i++) {
				if (rocks [i] != null) {
					if (Vector2.Distance (_Position, rocks [i].Position) < close) {
						closeID = i;
						close = Vector2.Distance (_Position, rocks [i].Position);
					}
				}
			}

			// Quick check that there hasn't been a fuck up
			if (closeID == -1) {
				int i = 0;
				while (rocks[i] == null) {
					i++;
					closeID = i;
				}
			} 



			// Move towards the nearest rock
			if ((rocks [closeID].Position.X - _Position.X) > 2) {
				_Position.X += _Velocity.X;
				_Facing = Direction.Right;
			} else if ((rocks [closeID].Position.X - _Position.X) < 2) {
				_Position.X -= _Velocity.X;
				_Facing = Direction.Left;
			} else if ((rocks [closeID].Position.Y - _Position.Y) > 2) {
				_Position.Y += _Velocity.Y;
				_Facing = Direction.Down;
			} else if ((rocks [closeID].Position.Y - _Position.Y) < 2) {
				_Position.Y -= _Velocity.Y;
				_Facing = Direction.Up;
			}
			// If we're on a rock, mine it!
			else {
				noRocksMined++;
				rocks.removeRock (closeID);
			}

			UpdateBoarders ();
		}

		#region implemented abstract members of Sprite

		public override void Update (GameTime gmt)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

