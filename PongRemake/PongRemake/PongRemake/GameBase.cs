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

namespace PongRemake
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameBase : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ball;
        Texture2D paddle;

        public static int monitorWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int monitorHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        bool debug = true;

        public GameBase()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = monitorWidth;
            graphics.PreferredBackBufferHeight = monitorHeight;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;
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
            Drawing.myFont = Content.Load<SpriteFont>("myFont");
            Updater.InitializeVariables();
            Updater.currentRendering = GameScreen.TITLE;
            Colors.PopulateColors();
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
            
            ball = Content.Load<Texture2D>("ball");
            paddle = Content.Load<Texture2D>("paddle");
            Drawing.spriteBatch = spriteBatch;
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
            Updater.kbCurrent = Keyboard.GetState();
            Updater.mouse = Mouse.GetState();
            if (Updater.kbCurrent.IsKeyDown(Keys.Escape))
                this.Exit();
            if (Updater.fadeChange)
            {
                Updater.FadeChangeUpdate();
            }
            else
            {
                switch (Updater.currentRendering)
                {
                    case GameScreen.TITLE:
                        Updater.TitleScreen();
                        break;
                    case GameScreen.OPTIONS:
                        Updater.OptionsScreen();
                        break;
                    case GameScreen.PAUSED:
                        Updater.PauseScreen();
                        break;
                    case GameScreen.QUIT:
                        Updater.QuitScreen();
                        break;
                    case GameScreen.PLAYING:
                        Updater.GamePlayingScreen();
                        break;
                    default:
                        throw new Exception("Error in switch statment in UPDATE method.  Did not satisfy any of the cases (currentRendering).");
                }
            }
            Updater.kbOld = Updater.kbCurrent;
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
            if (Updater.fadeChange)
            {
                Drawing.FadeChangeDraw();
            }
            else 
            {
                switch (Updater.currentRendering)
                {
                    case GameScreen.TITLE:
                        Drawing.TitleScreen();
                        break;
                    case GameScreen.OPTIONS:
                        Drawing.OptionsScreen();
                        break;
                    case GameScreen.PAUSED:
                        Drawing.PauseScreen();
                        break;
                    case GameScreen.QUIT:
                        Drawing.QuitScreen();
                        break;
                    case GameScreen.PLAYING:
                        Drawing.GamePlayingScreen();
                        break;
                    default:
                        throw new Exception("Error in switch statment in DRAW method.  Did not satisfy any of the cases. (currentRendering)");
                }
            }
            
            if (debug)
                Drawing.Debug();
            spriteBatch.End();
            base.Draw(gameTime);
        }  
    }
}
