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

namespace InstanceTesting
{
    /// <summary>
    /// Conclusion:
    ///     When you create an instance of an object(child) within an instance(parent), and the parent instance is destroyed or nullified
    ///     the child instances within it are also destroyed, either immediately or through the Garbage Collector.
    /// Example:
    ///     When an instance of the GameEngine class is created, it creates 2 instances of the player class.  When it is recreated
    ///     below, the instances of the player class are unreachable.  I can only assume that they are either destroyed or overwritten.
    ///     If one were to nullify the parent class instead of assigning it a new value through contruction, the path to the child instances
    ///     is lost (creates a null reference/pointer exception.  One can only assume that they are destroyed due to being a part of the
    ///     parent instance.  Even if they aren't immediately destroyed, they should be destroyed by the Garbage Collector because they
    ///     are no longer referenced by anything, so they will evenentally be flushed.
    /// </summary>
    /// 

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont myFont;

        KeyboardState kb;
        KeyboardState kbOld;

        GameEngine gameEngine;
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
            gameEngine = new GameEngine();
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
            myFont = Content.Load<SpriteFont>("myFont");

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
            kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Back) && kbOld.IsKeyUp(Keys.Back))
                gameEngine = new GameEngine();

            if (kb.IsKeyDown(Keys.Enter) && kbOld.IsKeyUp(Keys.Enter))
                gameEngine = new GameEngine("dog", "muffin");

            if (kb.IsKeyDown(Keys.Space) && kbOld.IsKeyUp(Keys.Space))      //Will cause NullReferenceException in Draw method 
                gameEngine = null;

            kbOld = kb;
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
            spriteBatch.DrawString(myFont, gameEngine.playerOne.name, new Vector2(20, 20), Color.White);
            spriteBatch.DrawString(myFont, gameEngine.playerTwo.name, new Vector2(20, 42), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
