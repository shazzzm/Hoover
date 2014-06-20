using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Hoover
{
	/// <summary>
	/// User controlled Hoover sprite
	/// </summary>
	public class Hoover : Sprite
	{
		/// <summary>
		/// The direction the sprite is facing - i.e. which sprite to show
		/// </summary>

		public Hoover ()
		{
			_assetName = "Transprt";
			_Position = new Vector2 (50, 50);
			_Velocity = new Vector2 (5, 5);
		}

		public void Update(GameTime gmt, Rectangle[] boarders)
		{
			KeyboardState state = Keyboard.GetState ();

			if (state.IsKeyDown (Keys.Right)) {
				_Facing = Direction.Right;

				if (DetectCollision (boarders, _Facing)) {
					_Position.X += _Velocity.X;
				}
			} else if (state.IsKeyDown (Keys.Left)) {
				_Facing = Direction.Left;

				if (DetectCollision (boarders, _Facing)) {
					_Position.X -= _Velocity.X;
				}
			} else if (state.IsKeyDown (Keys.Up)) {
				_Facing = Direction.Up;

				if (DetectCollision (boarders, _Facing)) {
					_Position.Y -= _Velocity.Y;
				}
			} else if (state.IsKeyDown (Keys.Down)) {
				_Facing = Direction.Down;

				if (DetectCollision (boarders, _Facing)) {
					_Position.Y += _Velocity.Y;
				}
			}

			UpdateBoarders ();
		}

		#region implemented abstract members of Sprite

		public override void Update (GameTime gmt)
		{
			throw new NotImplementedException ();
		}

		#endregion

		/// <summary>
		/// Draw the specified sprite.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		new public void Draw(SpriteBatch spriteBatch)
		{	
			// We only want to draw the hoover that's facing in the direction we're moving
			Rectangle rect = new Rectangle ();
			if (_Facing == Direction.Right) {
				rect = new Rectangle (80, 45, 40, 35);
			} else if (_Facing == Direction.Down) {
				rect = new Rectangle (40, 85, 40, 35);
			} else if (_Facing == Direction.Left) {
				rect = new Rectangle (0, 45, 40, 35);
			} else if (_Facing == Direction.Up) {
				rect = new Rectangle (40, 3, 40, 35);
			}
			spriteBatch.Draw (_texture, _Position, rect, Color.White);
		}
	}
}

