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

namespace Bearded_Robot
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont reactionFont;
        SpriteFont bigFont;

        Texture2D[] buttonPics;

                                    //Timers for triggering events and measuring reaction time.
        int[] timers;               //0: A button, 1: B button, 2: X button,  3: Y button,  4: Sound,  5:  Rumble
        int[] reactionTimeTotals;
        int monitorWidth;
        int monitorHeight;

        SoundEffect ding;

        GamePadState pad1;
        GamePadState padOld1;
        GamePadState pad2;
        GamePadState padOld2;

        String[,] reactionTimes;    // 2 players, 5 reaction time strings  0: A But.,  1: B But.,  2: X But.,  3:  Y But.,  4: Sound  5: Rumble

        Vector2[,] reactionTextPos;

        Rectangle[] picRecs;

        MouseState mouse;

        bool[,] hasBeenPressed;
        bool[] enabledReactions;    //0: A button, 1: B button, 2: X button,  3: Y button,  4: Sound,  5:  Rumble
        bool[] displayPic;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            monitorHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            monitorWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = monitorHeight;
            graphics.PreferredBackBufferWidth = monitorWidth;
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
            buttonPics = new Texture2D[6];
            picRecs = new Rectangle[4];
            reactionTextPos = new Vector2[2, 6];
            hasBeenPressed = new bool[2, 6];
            enabledReactions = new bool[6];
            displayPic = new bool[6];
            reactionTimes = new String[2, 6];
            timers = new int[6];
            reactionTimeTotals = new int[2];

            Random rand = new Random();
            timers[0] = -(rand.Next(20, 80) + rand.Next(35, 65));
            timers[1] = -(rand.Next(10, 45) + rand.Next(25, 80) + rand.Next(30, 45) + 100);
            timers[2] = -(rand.Next(180, 200) + rand.Next(10, 25));
            timers[3] = -(rand.Next(0, 80) + rand.Next(20, 50));
            timers[4] = -(rand.Next(30, 120) + rand.Next(60, 140) + rand.Next(100, 200));
            timers[5] = -(rand.Next(90, 110) + rand.Next(145, 195));
                
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            String[] picNames = new String[] { "A_Button", "B_Button", "X_Button", "Y_Button", "note", "rumble"};
            for (int i = 0; i < 6; i++)
                buttonPics[i] = Content.Load<Texture2D>(picNames[i]);
            reactionFont = Content.Load<SpriteFont>("reactionFont");
            ding = Content.Load<SoundEffect>("ding");
            PopulateArrays();
            
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
            pad1 = GamePad.GetState(PlayerIndex.One);
            pad2 = GamePad.GetState(PlayerIndex.Two);
            mouse = Mouse.GetState();
            UpdateTimers();
            if (pad1.IsConnected)
                CheckGamePadInput(pad1, padOld1, 0);
            if (pad2.IsConnected)
                CheckGamePadInput(pad2, padOld2, 1);
            padOld1 = pad1;
            padOld2 = pad2;
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
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++) 
                {
                    spriteBatch.DrawString(reactionFont, reactionTimes[i, j], reactionTextPos[i, j], Color.White);
                    if (j == 5)
                        spriteBatch.Draw(buttonPics[j], new Rectangle((int)reactionTextPos[i, j].X - 110, (int)reactionTextPos[i, j].Y + 3, 108, 50), Color.White);
                    else
                        spriteBatch.Draw(buttonPics[j], new Rectangle((int)reactionTextPos[i, j].X - 79, (int)reactionTextPos[i, j].Y + 6, 50, 50), Color.White);
                }     
            }
            for (int k = 0; k < 4; k++)
            {
                if (displayPic[k])
                    spriteBatch.Draw(buttonPics[k], picRecs[k], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void UpdateTimers()
        {
            for (int i = 0; i < 4; i++)     //A, B, X, and Y buttons
            {
                if (enabledReactions[i])    
                {
                    timers[i]++;
                    if (timers[i] == 0)
                        displayPic[i] = true;
                } 
            }
            if (enabledReactions[4])    //Sound
            {
                timers[4]++;
                if (timers[4] == 0)
                {
                    ding.Play();
                }
            }
            if (enabledReactions[5])    //Rumble
            {
                timers[5]++;
                if (timers[5] == 0)
                {
                    GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
                }
                else if (timers[5] == 45)
                {
                    GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (enabledReactions[j] && timers[j] == 120 && !hasBeenPressed[i, j])
                    {
                            reactionTimes[i, j] = "Time: " + timers[j] / 60 + Math.Abs((timers[j] % 60) / 60.0).ToString(".###");
                            hasBeenPressed[i, j] = true;
                            reactionTimeTotals[i] += timers[j];
                    }
                }
            }
            
        }

        public void CheckGamePadInput(GamePadState pad, GamePadState padOld, int player)
        {
            Buttons[] buttons = new Buttons[] { Buttons.A, Buttons.B, Buttons.X, Buttons.Y, Buttons.LeftShoulder, Buttons.RightShoulder };
            for (int i = 0; i < 6; i++)
            {
                if (enabledReactions[i] && pad.IsButtonDown(buttons[i]) && padOld.IsButtonUp(buttons[i]) && !hasBeenPressed[player, i])
                {
                    reactionTimes[player, i] = "Time: " + timers[i] / 60 + Math.Abs((timers[i] % 60) / 60.0).ToString(".###");
                    hasBeenPressed[player, i] = true;
                    if (timers[i] >= 0)
                        reactionTimeTotals[player] += timers[i];
                    else if (-timers[i] >= 60)
                        reactionTimeTotals[player] += -timers[i];
                    else
                        reactionTimeTotals[player] += 60;
                }
            }
        }

        public void PopulateArrays()
        { 
            int coordX = monitorWidth / 4;
            int coordY = monitorHeight / 4 - 120;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    reactionTimes[i, j] = "Time: Uncalculated";
                    hasBeenPressed[i, j] = false;
                    reactionTextPos[i, j] = new Vector2(coordX - (reactionFont.MeasureString(reactionTimes[i, j]).X / 2), coordY);
                    enabledReactions[j] = true;
                    coordY += 65;
                }
                coordX = monitorWidth / 4 * 3;
                coordY = monitorHeight / 4 - 120;
            }
            coordY = monitorHeight / 4 * 3 - 100;
            coordX = monitorWidth / 5 - 100;
            for (int k = 0; k < 4; k++)
            {
                displayPic[k] = false;
                picRecs[k] = new Rectangle(coordX, coordY, 200, 200);
                coordX += monitorWidth / 5;
            }
                
        }
    }
}
