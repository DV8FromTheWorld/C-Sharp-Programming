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

namespace Shooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState key;
        KeyboardState keyOld;

        GamePadState pad;
        GamePadState padOld;

        float playerMoveSpeed;

        Player player;

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
            player = new Player();
            playerMoveSpeed = 8.0f;
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
           
            // Load the player resources
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("shipAnimation");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 30, Color.White, 1f, true);
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerAnimation, playerPosition);
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
            pad = GamePad.GetState(PlayerIndex.One);
            key = Keyboard.GetState();
            UpdatePlayer(gameTime, player, pad);



            padOld = pad;
            keyOld = key;
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
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime, Player tempPlayer, GamePadState tempPad)
        {
            player.Update(gameTime);
            // Get Thumbstick Controls
            tempPlayer.Position.X += tempPad.ThumbSticks.Left.X * playerMoveSpeed;
            tempPlayer.Position.Y -= tempPad.ThumbSticks.Left.Y * playerMoveSpeed;

            // Use the Keyboard / Dpad
            if (key.IsKeyDown(Keys.Left) ||
            tempPad.DPad.Left == ButtonState.Pressed)
            {
                tempPlayer.Position.X -= playerMoveSpeed;
            }
            if (key.IsKeyDown(Keys.Right) ||
            tempPad.DPad.Right == ButtonState.Pressed)
            {
                tempPlayer.Position.X += playerMoveSpeed;
            }
            if (key.IsKeyDown(Keys.Up) ||
            tempPad.DPad.Up == ButtonState.Pressed)
            {
                tempPlayer.Position.Y -= playerMoveSpeed;
            }
            if (key.IsKeyDown(Keys.Down) ||
            tempPad.DPad.Down == ButtonState.Pressed)
            {
                tempPlayer.Position.Y += playerMoveSpeed;
            }

            // Make sure that the player does not go out of bounds
            tempPlayer.Position.X = MathHelper.Clamp(tempPlayer.Position.X, 0, GraphicsDevice.Viewport.Width - tempPlayer.Width);
            tempPlayer.Position.Y = MathHelper.Clamp(tempPlayer.Position.Y, 0, GraphicsDevice.Viewport.Height - tempPlayer.Height);
        }
    }
}
