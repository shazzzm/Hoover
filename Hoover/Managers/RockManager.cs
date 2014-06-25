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
		/// Number of rocks left
		/// </summary>
		private int _noRocks;

		/// <summary>
		/// Accesses the rock array
		/// </summary>
		/// <param name="index">Index.</param>
		public Rock this[int index]
		{
			get {
				if (_noRocks != 0)
				{
					return rocks[index];
				}

				return null;
			}
		}

		/// <summary>
		/// Number of rocks
		/// </summary>
		/// <value>The no rocks.</value>
		public int noRocks {
			get {
				return this._noRocks;
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

			_noRocks = rocks.Length;
		}


		/// <summary>
		/// Removes the specified rock
		/// </summary>
		/// <param name="index">Index.</param>
		public void removeRock(int index)
		{
			rocks [index] = null;
			_noRocks--;
		}

		/// <summary>
		/// Gets the rock boarders
		/// </summary>
		/// <returns>The boarders.</returns>
		public Rectangle[] getBoarders()
		{
			Rectangle[] boarders = new Rectangle[rocks.Length];

			for (int i = 0; i < rocks.Length; i++) {
				if (rocks [i] != null) {
					boarders [i] = rocks [i].Boarders;
				}
			}

			return boarders;
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

