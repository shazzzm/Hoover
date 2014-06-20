using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hoover
{
	/// <summary>
	/// Manages the rocks in the game, when to create them e.t.c
	/// </summary>
	public class RockManager
	{
		/// <summary>
		/// The rocks.
		/// </summary>
		private Rock[] rocks;

		/// <summary>
		/// Accesses the rock array
		/// </summary>
		/// <param name="index">Index.</param>
		public Rock this[int index]
		{
			get {
				return rocks [index];
			}
		}

		/// <summary>
		/// Number of rocks
		/// </summary>
		/// <value>The no rocks.</value>
		public int noRocks {
			get {
				return this.rocks.Length;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Hoover.RockManager"/> class.
		/// </summary>
		public RockManager ()
		{
			rocks = new Rock[10];
			Random r = new Random ();
			for (int i = 0; i < rocks.Length; i++) {
				rocks [i] = new Rock (new Vector2 (r.Next (300) * 2, r.Next(250) * 2));
			}
		}

		/// <summary>
		/// Removes the specified rock
		/// </summary>
		/// <param name="index">Index.</param>
		public void removeRock(int index)
		{
			rocks [index] = null;
		}

		/// <summary>
		/// Loads the content.
		/// </summary>
		/// <param name="content">Content.</param>
		public void LoadContent(ContentManager content)
		{
			for (int i = 0; i < rocks.Length; i++) {
				rocks [i].LoadContent (content);
			}
		}

		/// <summary>
		/// Draws the sprite
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < rocks.Length; i++) {
				if (rocks [i] != null) {
					rocks [i].Draw (spriteBatch);
				}
			}
		}
	}
}

