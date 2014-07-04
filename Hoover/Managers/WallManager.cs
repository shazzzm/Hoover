using System;
using System.Collections.Generic;

namespace Hoover
{
	public class WallManager
	{
		/// <summary>
		/// List of the wall tiles
		/// </summary>
		List<Wall> walls;

		/// <summary>
		/// Accesses the wall array
		/// </summary>
		/// <param name="index">Index.</param>
		public Wall this[int index]
		{
			get {
				return walls[index];
			}
		}

		/// <summary>
		/// Number of rocks
		/// </summary>
		/// <value>The no rocks.</value>
		public int noWalls {
			get {
				return walls.Count;
			}
		}

		public WallManager ()
		{
			walls = new List<Wall> ();


		}
	}
}

