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

namespace SuperMarioRecreation
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Point backPos;
        Boolean underGround;
        Viewport undergroundScreenDisplay;
        Viewport abovegroundScreenDisplay;

        Texture2D background;
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
            backPos.X = 0;
            backPos.Y = 0;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            underGround = false;
            abovegroundScreenDisplay = graphics.GraphicsDevice.Viewport;
            undergroundScreenDisplay = abovegroundScreenDisplay;
           // undergroundScreenDisplay.Height = 100;
          //  undergroundScreenDisplay.Width = 30;
           // undergroundScreenDisplay;
            background = Content.Load<Texture2D>("world 1-1");
            graphics.PreferredBackBufferHeight = (background.Height+16);
            graphics.PreferredBackBufferWidth = 765;
            graphics.ApplyChanges();
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
            if (keyDown(Keys.Escape))
                this.Exit();
            if (keyDown(Keys.Right))
                backPos.X -= 4;
            if (keyDown(Keys.Left))
                backPos.X += 4;
            if (keyDown(Keys.Up))
                backPos.Y -= 4;
            if (keyDown(Keys.Down))
                backPos.Y += 4;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            drawBackground(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private Boolean keyDown(Keys key)
        { 
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void drawBackground(SpriteBatch sb)
        {
            if (underGround)
            {

            }
            spriteBatch.Draw(background, new Rectangle(backPos.X, backPos.Y, background.Width * 3, (background.Height * 2)), null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            
        }

    }
}
