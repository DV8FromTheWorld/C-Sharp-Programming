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

namespace TitleScreen
{

    //  Title Screen
    //  Austin Keener
    //  2/26/2013

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont myFont;

        Texture2D titleScreen;
        Texture2D start;
        Texture2D instructions;

        Texture2D selected;
        Texture2D unselected;

        MouseState mouse;

        Rectangle startRec;
        Rectangle instructionRec;
        Rectangle titleRec;
        
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
            IsMouseVisible = true;
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

            titleScreen = Content.Load<Texture2D>("titleScreen");
            selected = Content.Load<Texture2D>("selected");
            unselected = Content.Load<Texture2D>("unselected");
            myFont = Content.Load<SpriteFont>("myFont");

            titleRec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            startRec = new Rectangle(55, 430, 80, 35);
            instructionRec = new Rectangle(555, 430, 150, 35);

            start = unselected;
            instructions = unselected;

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
            mouse = Mouse.GetState();

            if (startRec.Contains(new Point(mouse.X, mouse.Y)))
                start = selected;
            else
                start = unselected;

            if (instructionRec.Contains(new Point(mouse.X, mouse.Y)))
                instructions = selected;
            else
                instructions = unselected;

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
            spriteBatch.Draw(titleScreen, titleRec,Color.White);
            spriteBatch.Draw(start, startRec , Color.White);
            spriteBatch.Draw(instructions, instructionRec , Color.White);
            spriteBatch.DrawString(myFont, "Start!", new Vector2(60, 432), Color.White);
            spriteBatch.DrawString(myFont, "Instructions", new Vector2(560, 432), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
