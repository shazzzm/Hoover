using System;

namespace Hoover
{
	/// <summary>
	/// Manages the rocks in the game, when to create them e.t.c
	/// </summary>
	public class RockManager
	{
		private Rock[] rocks;

		public RockManager ()
		{
			rocks = new Rock[10];
			Random r = new Random ();
			for (int i = 0; i < rocks.Length; i++) {
				rocks [i] = new Rock (new Vector2 (r.Next (300) * 2, r.Next(250) * 2));
				rocks [i].LoadContent (this.Content);
			}
		}
	}
}

