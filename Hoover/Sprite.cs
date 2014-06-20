using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Hoover
{
	/// <summary>
	/// Abstract class for all sprites
	/// </summary>
	public abstract class Sprite
	{
		protected enum SpriteFacing { Up, Down, Left, Right };

		/// <summary>
		/// Direction the sprite is facing
		/// </summary>
		protected SpriteFacing _Facing;

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
		/// Name of the image for the sprite
		/// </summary>
		protected string _assetName;

		#region Properties

		public Vector2 Position
		{
			get {
				return new Vector2 (_Position.X, _Position.Y);
			}
		}

		#endregion

		/// <summary>
		/// Loads the content.
		/// </summary>
		/// <param name="contentManager">Content manager.</param>
		public void LoadContent(ContentManager contentManager)
		{
			_texture = contentManager.Load<Texture2D> (_assetName);
		}

		/// <summary>
		/// Draw the specified sprite.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (_texture, _Position, Color.White);
		}

		abstract public void Update (GameTime gmt);

		                   	
	}
}

