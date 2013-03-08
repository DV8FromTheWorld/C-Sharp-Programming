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

namespace ClockAnalog
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont myFont;

        Texture2D clockBackground;
        Texture2D hourHand;
        Texture2D minuteHand;
        Texture2D secondHand;

        Vector2 clockCenter;

        float secondRotation;
        float minuteRotation;
        float hourRotation;

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
            graphics.PreferredBackBufferHeight = 700;       //Sets the Height of the window
            graphics.PreferredBackBufferWidth = 700;        //Sets the Width of the window
            graphics.ApplyChanges();                        //Applies the changes set above
            IsMouseVisible = true;

            //hourAmount = (MathHelper.Pi * 2) / 12;

            clockCenter = new Vector2(350, 350);
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

            clockBackground = Content.Load<Texture2D>("clock");
            secondHand = Content.Load <Texture2D>("secondHand");
            minuteHand = Content.Load<Texture2D>("minuteHand");
            hourHand = Content.Load<Texture2D>("hourHand");
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

            calcSecondHandPos(DateTime.Now.Second);
            calcMinuteHandPos(DateTime.Now.Minute, DateTime.Now.Second);
            calcHourHandPos(DateTime.Now.Hour, DateTime.Now.Minute);

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
            spriteBatch.Draw(clockBackground, new Rectangle(0,0,700,700), Color.White);
            spriteBatch.Draw(hourHand, clockCenter, null, Color.White, hourRotation, new Vector2(6, 16), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(minuteHand, clockCenter, null, Color.White, minuteRotation, new Vector2(6, 16), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(secondHand, clockCenter, null, Color.White, secondRotation, new Vector2(6, 16), 1.0f, SpriteEffects.None, 0.0f);
            debug();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void calcSecondHandPos(int seconds)
        {
            //MathHelper.Pi * 2 gets the full circle.  We divide it into 60 parts (seconds).
            float secondSection = (MathHelper.Pi * 2) / 60;

            //We then find where the hand should be with the amount of seconds.
            secondRotation = (secondSection * seconds);

            //Lastly, we offset the hand back 90 degrees, (-= MathHelper.Pi / 2),  because on a circle, the 0 degree starts at the 3 instead of the 12 on a clock.
            secondRotation -= MathHelper.Pi / 2;
        }

        private void calcMinuteHandPos(int minutes, int seconds)
        {
            //MathHelper.Pi * 2 gets the full circle.  We divide it into 60 parts (minutes).
            float minuteSection = (MathHelper.Pi * 2) / 60;

            //We then find where the hand should be with the amount of minutes.
            minuteRotation = (minuteSection * minutes);

            //In addition, we cut the minuteSection into 60 parts, which represent the 60 seconds of a minute.
            //Then we add an offset to the rotation of the minute hand to account for the number of seconds that have passed.
            minuteRotation += (minuteSection / 60) * seconds;

            //Lastly, we offset the hand back 90 degrees, (-= MathHelper.Pi / 2),  because on a circle, the 0 degree starts at the 3 instead of the 12 on a clock.
            minuteRotation -= MathHelper.Pi / 2;
        }

        private void calcHourHandPos(int hours, int minutes)
        {
            //MathHelper.Pi * 2 gets the full circle.  We divide it into 12 parts (hours).
            float hourSection = (MathHelper.Pi * 2) / 12;
            
            //We then find where the hand should be with the amount of hours.
            hourRotation = hourSection * hours;

            //In addition, we cut the hourSection into 60 parts, which represent the 60 minutes of an hour.
            //Then we add an offset to the rotation of the hour hand to account for the number of minutes that have passed.
            hourRotation += (hourSection / 60) * minutes; 

            //Lastly, we offset the hand back 90 degrees, (-= MathHelper.Pi / 2),  because on a circle, the 0 degree starts at the 3 instead of the 12 on a clock.
            hourRotation -= MathHelper.Pi / 2;
        }

        private void debug()
        {
            MouseState mouse = Mouse.GetState();
            spriteBatch.DrawString(myFont, "X: " + mouse.X + "  Y: " + mouse.Y, new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(myFont, "H: " +DateTime.Now.Hour, new Vector2(0, 20), Color.Black);
            spriteBatch.DrawString(myFont, "M: " + DateTime.Now.Minute, new Vector2(0, 42), Color.Black);
            spriteBatch.DrawString(myFont, "S: " + DateTime.Now.Second, new Vector2(0, 64), Color.Black);
        }
    }
}
