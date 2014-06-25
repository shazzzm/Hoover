using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
		List<Rock> rocks;

		/// <summary>
		/// Accesses the rock array
		/// </summary>
		/// <param name="index">Index.</param>
		public Rock this[int index]
		{
			get {
				return rocks[index];
			}
		}

		/// <summary>
		/// Number of rocks
		/// </summary>
		/// <value>The no rocks.</value>
		public int noRocks {
			get {
				return rocks.Count;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Hoover.RockManager"/> class.
		/// </summary>
		public RockManager ()
		{
			rocks = new List<Rock> ();
			Random r = new Random ();
			for (int i = 0; i < 10; i++) {
				Rock temp = new Rock (new Vector2 (r.Next (300) * 2, r.Next(250) * 2));
				rocks.Add (temp);
			}
		}


		/// <summary>
		/// Removes the specified rock
		/// </summary>
		/// <param name="index">Index.</param>
		public void removeRock(int index)
		{
			rocks.RemoveAt (index);
		}

		/// <summary>
		/// Gets the rock boarders
		/// </summary>
		/// <returns>The boarders.</returns>
		public List<Rectangle> getBoarders()
		{
			List<Rectangle> boarders = new List<Rectangle> ();

			for (int i = 0; i < rocks.Count; i++) {
				boarders.Add (rocks [i].Boarders);
			}

			return boarders;
		}

		/// <summary>
		/// Loads the content.
		/// </summary>
		/// <param name="content">Content.</param>
		public void LoadContent(ContentManager content)
		{
			for (int i = 0; i < rocks.Count; i++) {
				rocks [i].LoadContent (content);
			}
		}

		/// <summary>
		/// Draws the sprite
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < rocks.Count; i++) {
				rocks [i].Draw (spriteBatch);
			}
		}
	}
}

