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

namespace SlopeTesting
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball, player;
        SpriteFont myFont;
        Vector2 pos;
        Rectangle playerPos;
        float slope;
        bool movingRight;
        int yIntercept;

        int monitorWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int monitorHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        Viewport viewport;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = monitorHeight;
            graphics.PreferredBackBufferWidth = monitorWidth;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            
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
            movingRight = true;
            slope = -.5f;
            viewport = graphics.GraphicsDevice.Viewport;
            pos = new Vector2(0, 0);
            yIntercept = viewport.Height / 2;
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
            myFont = Content.Load<SpriteFont>("font");
            ball = Content.Load<Texture2D>("ball");
            player = Content.Load<Texture2D>("player_bar");

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            playerPos = new Rectangle(30, Mouse.GetState().Y, 20, 120);

            if (movingRight)
                pos.X += 5;
            else
                pos.X -= 5;
            pos.Y = (pos.X * slope) + yIntercept;

            if (pos.Y < 0)                                  //Hits the roof
            {
                yIntercept = -(int)(pos.Y - (pos.X * slope));
                slope *= -1;
            }

            if ((pos.Y + ball.Height) > viewport.Height)    //Hits the floor
            {

                yIntercept = (int)(pos.Y + (pos.X * slope));
                slope *= -1;
            }

            if (pos.X < 0)                                  //Hits the left wall
            {
                movingRight = true;
                yIntercept = (int)(pos.Y + (pos.X * slope));
                slope *= -1;
            }

            if ((pos.X + ball.Width) > viewport.Width)      //Hits the right wall
            {
                movingRight = false;
                yIntercept = (int)(pos.Y + (pos.X * slope));
                slope *= -1;
            }

            if (playerPos.Contains(new Point((int)pos.X, (int)pos.Y)))
            {
                movingRight = true;
                yIntercept = (int)(pos.Y + (pos.X * slope));
                slope *= -1;
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(ball, pos, Color.White);
            spriteBatch.Draw(player, playerPos, Color.White);
            spriteBatch.DrawString(myFont, "slope: " + slope, new Vector2(20, 20), Color.White);
            spriteBatch.DrawString(myFont, "X: " + pos.X + "  Y: " + pos.Y, new Vector2(20, 42), Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
