using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;


namespace Hoover
{
	/// <summary>
	/// Abstract class for all sprites
	/// </summary>
	public abstract class Sprite
	{
		protected enum Direction { Up, Down, Left, Right };

		/// <summary>
		/// Direction the sprite is facing
		/// </summary>
		protected Direction _Facing;

		/// <summary>
		/// Position of the sprite
		/// </summary>
		protected Vector2 _Position;

		/// <summary>
		/// Velocity of the sprite
		/// </summary>
		protected Vector2 _Velocity;

		/// <summary>
		/// Texture
		/// </summary>
		protected Texture2D _texture;

		/// <summary>
		/// Boarders of the sprite
		/// </summary>
		protected Rectangle _boarders;

		/// <summary>
		/// Name of the image for the sprite
		/// </summary>
		protected string _assetName;

		/// <summary>
		/// The size of the _texture for our boarders
		/// </summary>
		protected Vector2 _textureSize;

		#region Properties

		/// <summary>
		/// Position of the Sprite
		/// </summary>
		/// <value>The position.</value>
		public Vector2 Position
		{
			get {
				return new Vector2 (_Position.X, _Position.Y);
			}
		}

		/// <summary>
		/// Gets the boarders.
		/// </summary>
		/// <value>The boarders.</value>
		public Rectangle Boarders
		{
			get {
				return new Rectangle (this._boarders.X, this._boarders.Y, this._boarders.Width, this._boarders.Height);
			}
		}

		#endregion

		#region MonoGame Logic
		/// <summary>
		/// Loads the content.
		/// </summary>
		/// <param name="contentManager">Content manager.</param>
		public void LoadContent(ContentManager contentManager)
		{
			_texture = contentManager.Load<Texture2D> (_assetName);
			// Create a rectangle based off the size of the textures for collision detection
			_boarders = new Rectangle(((int)_Position.X), (int)(_Position.Y), (int)_textureSize.X, (int)_textureSize.Y);
		}

		/// <summary>
		/// Draw the specified sprite.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (_texture, _Position, Color.White);
		}

		/// <summary>
		/// Updates the sprite's position e.t.c
		/// </summary>
		/// <param name="gmt">Gmt.</param>
		public abstract void Update (GameTime gmt);

		#endregion

		/// <summary>
		/// Updates the boarders.
		/// </summary>
		protected void UpdateBoarders()
		{
			_boarders.X = (int)_Position.X;
			_boarders.Y = (int)_Position.Y;
		}

		/// <summary>
		/// Detects collisions with other sprites in the specified direction
		/// </summary>
		/// <returns>The collision.</returns>
		/// <param name="boarders">Boarders.</param>
		/// <param name="d">Direction to check</param>
		protected bool DetectCollision(List<Rectangle> boarders, Direction d)
		{
			Rectangle r = new Rectangle();
			for (int i = 0; i < boarders.Count; i++) {
				// We add the velocity to the current position to see if a collision will occur, 
				if (d == Direction.Left) {
					r = new Rectangle ((int)(_Position.X - _Velocity.X), (int)_Position.Y, Boarders.Width, Boarders.Height);
					//Debug.Write ("Left intersection");

				} else if (d == Direction.Right) {
					r = new Rectangle ((int)(_Position.X + _Velocity.X), (int)_Position.Y, Boarders.Width, Boarders.Height);
					//Debug.Write ("Right intersection");
				} else if (d == Direction.Up) {
					r = new Rectangle ((int)_Position.X, (int)(_Position.Y - _Velocity.Y), Boarders.Width, Boarders.Height); 
					//Debug.Write ("Up intersection");
				} else if (d == Direction.Down) {
					r = new Rectangle ((int)_Position.X, (int)(_Position.Y + _Velocity.Y), Boarders.Width, Boarders.Height);
					//Debug.Write ("Down intersection");
				}

				if (r.Intersects (boarders [i])) {
					return true;
				}
			}

			return false;
		}
	}
}

