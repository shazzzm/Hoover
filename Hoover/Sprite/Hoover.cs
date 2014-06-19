using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Hoover
{
	public class Hoover : Sprite
	{
		/// <summary>
		/// The direction the sprite is facing - i.e. which sprite to show
		/// </summary>
		enum SpriteFacing { Up, Down, Left, Right };
		SpriteFacing _Facing = SpriteFacing.Up;
		public Hoover ()
		{
			_assetName = "Transprt";
			_Position = new Vector2 (50, 50);
			_Velocity = new Vector2 (5, 5);
		}

		public override void Update(GameTime gmt)
		{
			KeyboardState state = Keyboard.GetState ();

			if (state.IsKeyDown (Keys.Right)) {
				_Facing = SpriteFacing.Right;
				_Position.X += _Velocity.X;
			} else if (state.IsKeyDown (Keys.Left)) {
				_Facing = SpriteFacing.Left;
				_Position.X -= _Velocity.X;
			} else if (state.IsKeyDown (Keys.Up)) {
				_Facing = SpriteFacing.Up;
				_Position.Y -= _Velocity.Y;
			} else if (state.IsKeyDown (Keys.Down)) {
				_Facing = SpriteFacing.Down;
				_Position.Y += _Velocity.Y;
			}




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
				rect = new Rectangle (80, 45, 40, 35);
			} else if (_Facing == SpriteFacing.Down) {
				rect = new Rectangle (40, 85, 40, 35);
			} else if (_Facing == SpriteFacing.Left) {
				rect = new Rectangle (0, 45, 40, 35);
			} else if (_Facing == SpriteFacing.Up) {
				rect = new Rectangle (40, 3, 40, 35);
			}
			spriteBatch.Draw (_texture, _Position, rect, Color.White);
		}
	}
}

