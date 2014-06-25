using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hoover
{
	/// <summary>
	/// Miner.
	/// </summary>
	public class Miner : Sprite
	{
		int collisionRight = 0;
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

		public void Update (GameTime gmt, RockManager rocks, List<Rectangle> boarders)
		{
			// If we're out of rocks, just chill
			if (rocks.noRocks == 0) {
				return;
			}

			float close = 100000f;
			int closeID = -1;
			// Find the closest rock
			for (int i = 0; i < rocks.noRocks; i++) {
				if (Vector2.Distance (_Position, rocks [i].Position) < close) {
					closeID = i;
					close = Vector2.Distance (_Position, rocks [i].Position);
				}
			}

			// Move towards the nearest rock
			if ((rocks [closeID].Position.Y - _Position.Y) > 2) {
				move (Direction.Down, boarders);
			} else if ((rocks [closeID].Position.X - _Position.X) < 2) {
				move (Direction.Left, boarders);
			} else if ((rocks [closeID].Position.Y - _Position.Y) < 2) {
				move (Direction.Up, boarders);
			} else if ((rocks [closeID].Position.X - _Position.X) > 2) {
				move (Direction.Right, boarders);
			} 
			// If we're on a rock, mine it!
			else {
				noRocksMined++;
				rocks.removeRock (closeID);
			}

			UpdateBoarders ();
		}

		/// <summary>
		/// Moves the sprite in the specified direction. We also do edge detection e.t.c here
		/// </summary>
		/// <param name="d">Direction we're moving</param>
		/// <param name="boarders">Boarders to avoid</param>
		private void move(Direction d, List<Rectangle> boarders)
		{
			_Facing = d;
			// If there's no collision, move!
			if (!DetectCollision (boarders, d)) {
				if (d == Direction.Down) {
					_Position.Y += _Velocity.Y;
				} else if (d == Direction.Left) {
					_Position.X -= _Velocity.X;
				} else if (d == Direction.Up) {
					_Position.Y -= _Velocity.Y;
				} else if (d == Direction.Right) {
					_Position.X += _Velocity.X;
				} 
			} else {
				// If the collision is one way, try going another!
				if (d==Direction.Down) {
					Debug.Write ("Collision Down: Trying Right");
					move (Direction.Right, boarders);
				} else if (d == Direction.Left) {
					Debug.Write ("Collision Left: Trying Down");
					move (Direction.Down, boarders);
				} else if (d == Direction.Up) {
					Debug.Write ("Collision Up: Trying Left");
					move (Direction.Left, boarders);
				} else if (d == Direction.Right) {
					Debug.Write ("Collision Right: Trying Down");
					move (Direction.Down, boarders);
					collisionRight++;
				} 
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

