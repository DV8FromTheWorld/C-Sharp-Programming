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

namespace BeadRolling
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont myFont;

        int[] currentRoll;
        int[] rollsAmount;

        bool gameOver;

        String[] names;
        String winner;
        
        Vector2[] rollPos;

        KeyboardState kb;
        KeyboardState oldkb;

        Texture2D[] rolls;
        Texture2D Background;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();
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
            rolls = new Texture2D[10];
            currentRoll = new int[] { 0, 0 };
            rollsAmount = new int[] { 0, 0 };
            rollPos = new Vector2[2];
            names = new String[] {"Fatuma", "Sanyu"};
            gameOver = false;
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
            String[] s = new String[] { "roll1", "roll2", "roll3", "roll4", "roll5", "roll6", "roll7", "roll8", "roll9", "roll10" };
            for (int i = 0; i < 10; i++)
            {
                rolls[i] = Content.Load<Texture2D>(s[i]);
            }
            Background = Content.Load<Texture2D>("background");
            myFont = Content.Load<SpriteFont>("myFont");

            Viewport v = GraphicsDevice.Viewport;
            int widthQuarter = (v.Width / 2) / 2;
            rollPos[0] = new Vector2(widthQuarter - rolls[0].Width/2, 150);
            rollPos[1] = new Vector2(widthQuarter*3 - rolls[0].Width/2, 150);
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

            if (!gameOver)
            {
                kb = Keyboard.GetState();
                if (oldkb.IsKeyUp(Keys.Left) && kb.IsKeyDown(Keys.Left))
                    currentRoll[0]++;
                if (currentRoll[0] >= 10)
                {
                    rollsAmount[0]++;
                    currentRoll[0] = 0;
                }

                if (oldkb.IsKeyUp(Keys.Right) && kb.IsKeyDown(Keys.Right))
                    currentRoll[1]++;
                if (currentRoll[1] >= 10)
                {
                    rollsAmount[1]++;
                    currentRoll[1] = 0;
                }

                oldkb = kb;

                for (int i = 0; i < 2; i++)
                {
                    if (rollsAmount[i] >= 5)
                    {
                        gameOver = true;
                        winner = names[i];
                    }
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Viewport v = GraphicsDevice.Viewport;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(rolls[currentRoll[0]], rollPos[0], Color.White);
            spriteBatch.Draw(rolls[currentRoll[1]], rollPos[1], Color.White);
            spriteBatch.DrawString(myFont, names[0]+": " + rollsAmount[0], new Vector2((v.Width / 4) - (myFont.MeasureString(names[0]+": " + rollsAmount[0]).X /2), 50), Color.White);
            spriteBatch.DrawString(myFont, names[1]+": " + rollsAmount[1], new Vector2((v.Width / 4)*3 - (myFont.MeasureString(names[0]+": " + rollsAmount[0]).X /2), 50), Color.White);
            if (gameOver)
                spriteBatch.DrawString(myFont, winner + " has won!", new Vector2((v.Width / 2) - (myFont.MeasureString(winner + " has won!").X /2), 100), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
