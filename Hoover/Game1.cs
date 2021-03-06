#region Using Statements
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Hoover
{
	/// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;		
		Hoover hoover;
		Miner miner;
		RockManager rocks;
		Wall wall;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = false;		

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
			hoover = new Hoover ();
			miner = new Miner ();
			rocks = new RockManager ();
			wall = new Wall (new Vector2 (40, 10));
            base.Initialize();
				
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here 
			hoover.LoadContent (this.Content);
			miner.LoadContent (this.Content);
			rocks.LoadContent (this.Content);
			wall.LoadContent (this.Content);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			{
				Exit();
			}
            // TODO: Add your update logic here		
			List<Rectangle> boarders = rocks.getBoarders ();
			List<Rectangle> hooverBoarder = new List<Rectangle> ();
			hooverBoarder.Add (hoover.Boarders);
			boarders.Add (miner.Boarders);
			hoover.Update (gameTime, boarders);
			miner.Update (gameTime, rocks, hooverBoarder);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           	//graphics.GraphicsDevice.Clear(new Color(0, 138, 118));
			graphics.GraphicsDevice.Clear(Color.Wheat);
            //TODO: Add your drawing code here
			spriteBatch.Begin ();
			hoover.Draw (spriteBatch);
			miner.Draw (spriteBatch);
			rocks.Draw (spriteBatch);
			wall.Draw (spriteBatch);

			spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

