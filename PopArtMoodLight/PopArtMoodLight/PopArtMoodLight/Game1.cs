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

namespace PopArtMoodLight
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D backImage;
        Rectangle quad1, quad2, quad3, quad4;

        byte[] redIntensity;
        byte[] greenIntensity;
        byte[] blueIntensity;
        byte[][] intensities;

        bool[] redCountingUp;
        bool[] greenCountingUp;
        bool[] blueCountingUp;
        bool[][] countingUp;

        Color[] colors;

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
            int width = GraphicsDevice.Viewport.Width / 2;
            int height = GraphicsDevice.Viewport.Height / 2;

            quad1 = new Rectangle(0, 0, width, height);
            quad2 = new Rectangle(width, 0, width, height);
            quad3 = new Rectangle(0, height, width, height);
            quad4 = new Rectangle(width, height, width, height);

            redIntensity = new byte[] { 0, 0, 0, 0 }; 
            greenIntensity = new byte[] { 0, 0, 0, 0 };
            blueIntensity = new byte[] { 0, 0, 0, 0 };
            intensities = new byte[][] { redIntensity, greenIntensity, blueIntensity };

            redCountingUp = new bool[] { false, false, false, false };
            greenCountingUp = new bool[] { false, false, false, false };
            blueCountingUp = new bool[] { false, false, false, false };
            countingUp = new bool[][] { redCountingUp, greenCountingUp, blueCountingUp };

            colors = new Color[] { Color.White, Color.White, Color.White, Color.White };

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

            backImage = Content.Load<Texture2D>("Jakbert");



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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (intensities[j][i] == 0) countingUp[j][i] = true;
                    if (intensities[j][i] == 255) countingUp[j][i] = false;
                    if (countingUp[j][i]) intensities[j][i]++; else intensities[j][i]--;
                }

                colors[i] = new Color(intensities[0][i], intensities[1][i], intensities[2][i]);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(backImage, quad1, colors[0]);
            spriteBatch.Draw(backImage, quad2, colors[1]);
            spriteBatch.Draw(backImage, quad3, colors[2]);
            spriteBatch.Draw(backImage, quad4, colors[3]);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
