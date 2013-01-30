using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace MoodLight
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //color shift (mood light)
        Color backgroundColor;
        int redIntensity, greenIntensity, blueIntensity;
        Boolean redIncrease, greenIncrease, blueIncrease;
        
        //strobe stuff
        int black;
        int strobeSpeed;
        Boolean noStrobe;  

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            redIntensity = 0;       //Should not all start at 0 because it will just go from black to white due to color balance.
            greenIntensity = 127;
            blueIntensity = 255;
            black = 1;
            noStrobe = true;        //Disables strobe effect by default.  Set to false to enable strobe effect.
            strobeSpeed = 3;        //Controls Strobe Speed.  Increase variable to slow down the strobe.
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

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Back))
                this.Exit();
            if (black != 0 | noStrobe)
            {
                if (redIntensity == 0) redIncrease = true;
                if (redIntensity == 255) redIncrease = false;
                if (redIncrease) redIntensity++; else redIntensity--;

                if (blueIntensity == 0) blueIncrease = true;
                if (blueIntensity == 255) blueIncrease = false;
                if (blueIncrease) blueIntensity++; else blueIntensity--;

                if (greenIntensity == 0) greenIncrease = true;
                if (greenIntensity == 255) greenIncrease = false;
                if (greenIncrease) greenIntensity++; else greenIntensity--;

                backgroundColor = new Color(redIntensity, greenIntensity, blueIntensity);
                if (black == strobeSpeed)
                {
                    black = 0;
                }
                else
                {
                    black += 1;
                }
            }
            else
            {
                backgroundColor = new Color(0, 0, 0);
                black += 1;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
