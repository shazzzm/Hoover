using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hoover
{
	/// <summary>
	/// Miner.
	/// </summary>
	public class Miner : Sprite
	{
		//int collisionRight = 0;
		int noRocksMined = 0;
		bool checkPath = false;
		int pathNo = 0;

		List<Vector2> vectorPath = new List<Vector2>();
		List<Direction> path = new List<Direction>();
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
			foreach (Vector2 pos in vectorPath) {
				spriteBatch.Draw (_texture, pos, rect, Color.White);
			}
		}

		public void Update (GameTime gmt, RockManager rocks, List<Rectangle> boarders)
		{
			KeyboardState state = Keyboard.GetState ();
			if (state.IsKeyDown (Keys.A)) {
				checkPath = true;
			} else {
				checkPath = false;
			}
			// If we're out of rocks, just chill
			if (rocks.noRocks == 0) {
				return;
			}

			// If we're on a rock, mine it!
			int j = 0;
			foreach (Rectangle r in rocks.getBoarders ()) {
				if (r.Intersects (_boarders)) {
					rocks.removeRock (j);

					// Create a new path
					path = new List<Direction> ();
					pathNo = 0;
					return;
				}
				j++;
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

			// If there's no path, or if there's a collision create it so it moves towards the nearest rock
			if (path.Count == 0 || checkPathCollision(boarders)) {
				path = findPath (rocks [closeID].Position, boarders);
				pathNo = 0;
			}

			// TODO: Have an error here
			if (pathNo == 500 || pathNo >= path.Count) {
				path = new List<Direction>();
				pathNo = 0;
				return;
			}

			move (path [pathNo]);
			pathNo++;

			UpdateBoarders ();
		}

		/// <summary>
		/// Check if there's a collision on our path
		/// </summary>
		/// <returns><c>true</c>, if path collision was checked, <c>false</c> otherwise.</returns>
		private bool checkPathCollision(List<Rectangle> boarders)
		{
			// Move along the path
			foreach (Vector2 pos in vectorPath) {

				if (DetectCollision (boarders, Direction.None, pos)) {
					Debug.Write ("Collision going at " + pos.ToString());
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Moves without checking for collisions
		/// </summary>
		/// <param name="d">D.</param>
		private void move(Direction d)
		{
			_Facing = d;
			_Position = updateVectorMovement (_Position, _Velocity, d);
		}

		/// <summary>
		/// Implementation of an A* search algorithm to find rocks
		/// </summary>
		private List<Direction> findPath(Vector2 goalPos, List<Rectangle> boarders)
		{
			List<Direction> path = new List<Direction> (); // Our path so far
			List<Vector2> checkedSquares = new List<Vector2> (); // Square's we've checked so far
			Vector2 curPos = new Vector2 (_Position.X, _Position.Y);

			while (Math.Abs((curPos - goalPos).Length()) > 1)
			{
				Direction bestMove = Direction.Down;
				Vector2 newLocation = curPos;
				float bestCost = 500000f;
				// Check all neighbours of current tile for the best
				for (int i = 0; i < 4; i++)
				{
					float heuristic;
					Direction d = (Direction)i;
					Vector2 posToCheck = new Vector2(curPos.X, curPos.Y);
					if (d == Direction.Down) {
						posToCheck.Y += _Velocity.Y;
					} else if (d == Direction.Up) {
						posToCheck.Y -= _Velocity.Y;
					} else if (d == Direction.Left) {
						posToCheck.X -= _Velocity.X;
					} else if (d == Direction.Right) {
						posToCheck.X += _Velocity.X;
					}

					// If we've already checked the square, we don't want to go over it again
					if (checkedSquares.Contains (posToCheck)) {
						heuristic = 500000f;
						//Debug.Write ("Checking square that's already done");
					} else {
						heuristic = getHeuristic (posToCheck, goalPos, boarders, new Rectangle ((int)posToCheck.X, (int)posToCheck.Y, _boarders.Width, _boarders.Height));
					}
					//Debug.Write ("Current Best: " + bestCost.ToString());
					//Debug.Write ("Checking: " + d.ToString ());
					//Debug.Write("Heuristic: " + heuristic.ToString()); 
					if (heuristic < bestCost) {
						bestMove = d;
						newLocation = posToCheck;
						bestCost = heuristic;
					}
					//Debug.Write ("Best Move: " + bestMove.ToString());
				}

				if (checkPath) {
					foreach (Direction d in path) {
						Debug.Write (d);
					}
				}
				checkedSquares.Add (newLocation);
				path.Add (bestMove);
				vectorPath = checkedSquares;
				// Update our position
				curPos = newLocation;
				//Debug.Write (curPos);

				// If this gets a bit big, run it again
				if (path.Count > 500) {
					return path;
				}
			}


			return path;
		}

		private float getHeuristic(Vector2 pos, Vector2 goal, List<Rectangle> boarders, Rectangle myBoarders)
		{
			// If there's an object in the way, return a very high cost
			foreach (Rectangle r in boarders) {
				if (r.Intersects (myBoarders)) {
					return 50000f;
				}
			}

			return (Math.Abs (pos.X - goal.X) + Math.Abs (pos.Y - goal.Y));
		}

		#region implemented abstract members of Sprite

		public override void Update (GameTime gmt)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

