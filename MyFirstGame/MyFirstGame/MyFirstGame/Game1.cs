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

namespace MyFirstGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Debug Text Print Variables
        SpriteFont font;
        string mouseLoc;
        string imageTex;
        string bob;
        string temp;

        //Background Image Variables
        Texture2D backgroundImage;
        Vector2 backgroundPosition;
        
        //Mario Image Variables
        Texture2D marioImage;
        Rectangle marioPosition;
        SpriteEffects marioDirection;

        //Mario's "magic" numbers
        int marioHeight, marioWidth;
        int bottomFootXOffset;
        Point marioStartPoint;
        Point marioBottomFoot;

        //BoundBox "magic" numbers
        int topOfFloorBB;

        //BoundingBox Texture (used for BB debug)
        Texture2D BBTexture;

        //Bounding Boxes
        Rectangle groundBBPos;

        //Array of all known Bounding Boxes
        Rectangle[] BBArray;

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
            this.IsMouseVisible = true;

            //Mario Magic Numbers
            marioHeight = 85;           //resize height
            marioWidth = 85;            //resize width
            marioStartPoint.X = 0;      //Start point when first drawn (X value)
            marioStartPoint.Y = 354;    //Start point when first drawn (Y value)
            bottomFootXOffset = 22;     //X value offset for the tip of the bottom foot

            //Resize and set start position of mario
            marioPosition = new Rectangle(marioStartPoint.X, marioStartPoint.Y, marioWidth, marioHeight);

            marioDirection = SpriteEffects.None;
            backgroundPosition = Vector2.Zero;

            //BoundBox Magic Numbers
            topOfFloorBB = 41;  

            //Initialize the positions of the bounding boxes (for collision)
            getBBPositions();

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
            backgroundImage = Content.Load<Texture2D>("Background");
            marioImage = Content.Load<Texture2D>("mario");
            BBTexture = Content.Load<Texture2D>("boundingBoxLimit");
            font = Content.Load<SpriteFont>("myFont");

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (KeyDown(Keys.Escape))
                this.Exit();
            marioBottomFoot = new Point(marioPosition.X + bottomFootXOffset, marioPosition.Y + marioHeight);
            //if (isAgainstBB()) { } else { CheckMovement(); }
            CheckMovement();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.Draw(backgroundImage, backgroundPosition, Color.White);
            //                texture    image pos/size         color    rotation  origin(ignore)  sprite effects  layer
            spriteBatch.Draw(marioImage, marioPosition, null, Color.White, 0.0f, new Vector2(0, 0), marioDirection, 0.0f);
            spriteBatch.Draw(BBTexture, groundBBPos, Color.White);
            printStoof();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private Boolean KeyDown(Keys key){
            if(Keyboard.GetState(PlayerIndex.One).IsKeyDown(key))
            {
                return true;
            }else
            {
                return false;
            }
        }

        private void CheckMovement(){
            if (KeyDown(Keys.Right))
            {
                //if (isAgainstBB( marioPosition.X + 4, 
                marioPosition.X += 4;
                marioDirection = SpriteEffects.None;
            }
            if (KeyDown(Keys.Left))
            {
                marioPosition.X -= 4;
                marioDirection = SpriteEffects.FlipHorizontally;
            }
            if (KeyDown(Keys.Down))
            {
                marioPosition.Y += 4;
            }
            if (KeyDown(Keys.Up))
            {
                marioPosition.Y -= 4;
            }
        }

        private void getBBPositions()
        {
                                    //     gets the bottom, removes topOfFloorBB to get top of BB       gets the far right edge         thickness of BB
            groundBBPos = new Rectangle(0, graphics.GraphicsDevice.Viewport.Height - topOfFloorBB, graphics.GraphicsDevice.Viewport.Width, 30);

            BBArray = new Rectangle[] { groundBBPos };
        }

        private Boolean isAgainstBB(int X, int Y)
        {
            foreach(Rectangle bBox in BBArray){
                if (bBox.Contains(new Point(X, Y)))
                {
                    return true;
                }
            }
            return false;
        }

        private void printStoof()
        {
            mouseLoc = "X: " + Mouse.GetState().X + " Y: " + Mouse.GetState().Y;
            imageTex = "X: " + marioPosition.X + " Y: " + marioPosition.Y;
            bob = "X: " + marioBottomFoot.X + " Y: " + marioBottomFoot.Y;
            temp = (graphics.GraphicsDevice.Viewport.Height - topOfFloorBB - marioHeight).ToString();
            spriteBatch.DrawString(font, mouseLoc, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, imageTex, new Vector2(10, 22), Color.White);
            spriteBatch.DrawString(font, bob, new Vector2(10, 34), Color.White);
            spriteBatch.DrawString(font, temp, new Vector2(10, 46), Color.White);
        }
    }
}
