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

namespace MovingWithSpeed
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D faceLeft, faceRight, faceUp, faceDown, background;
        Texture2D currentTexture;
        SpriteFont myFont;

        int currentGear;
        int temp;

        Point currentPosition;
        const int MAXSPEED = 30;
        const int MINSPEED = 0;
        int currentSpeed;

        float rightTrigger;
        int currentDirection;
        int tempDirection;

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
            currentSpeed = 0;
            currentGear = 1;
            currentPosition = new Point(305, 365);
            tempDirection = 3;
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
            faceLeft = Content.Load<Texture2D>("AmbulanceLeft");
            faceRight = Content.Load<Texture2D>("AmbulanceRight");
            faceUp = Content.Load<Texture2D>("AmbulanceForward");
            faceDown = Content.Load<Texture2D>("AmbulanceBackward");
            background = Content.Load<Texture2D>("Hospital Parking Lot");
            myFont = Content.Load<SpriteFont>("myFont");

            currentTexture = faceRight;
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
            checkInput();
            moveCar(currentDirection, calcSpeed());

            // TODO: Add your update logic here

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
            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.Draw(currentTexture, new Rectangle(currentPosition.X, currentPosition.Y, currentTexture.Width/5, currentTexture.Height/5), Color.White);
            spriteBatch.DrawString(myFont, "Current Direction: " + currentDirection + "  speed: " + currentSpeed + " position  X: " + currentPosition.X + "  Y: " + currentPosition.Y + "  Trigger: " + rightTrigger, new Vector2(20, 20), Color.White);
            spriteBatch.DrawString(myFont, " temp: " + temp, new Vector2(20, 44), Color.Black);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void checkInput()
        {
            GamePadState pad1 = GamePad.GetState(PlayerIndex.One);
            rightTrigger = pad1.Triggers.Right;
            if (pad1.ThumbSticks.Left.Y > 0.85f)
                tempDirection = 0;
            if (pad1.ThumbSticks.Left.Y < -0.85f)
                tempDirection = 1;
            if (pad1.ThumbSticks.Left.X < -0.85f)
                tempDirection = 2;
            if (pad1.ThumbSticks.Left.X > 0.85f)
                tempDirection = 3;
            changeDirection(tempDirection);
            currentDirection = tempDirection;       //4 total possibilites.  0, 1, 2, 3.  represents up, down, left, right.
        }

        private void changeDirection(int tempDirection)
        {
            if (tempDirection == currentDirection)
            {
                return;
            }
            else
            {
                switch (tempDirection)
                { 
                    case 0:
                        currentTexture = faceUp;
                        break;
                    case 1:
                        currentTexture = faceDown;
                        break;
                    case 2:
                        currentTexture = faceLeft;
                        break;
                    case 3:
                        currentTexture = faceRight;
                        break;
                    default:
                        break;
                }
            }
        }

        private int calcSpeed()
        {
            temp = getAcceleration(currentSpeed, rightTrigger);
            currentSpeed += temp;
            if (currentSpeed < MINSPEED)
                currentSpeed = MINSPEED;
           
            if (currentSpeed > MAXSPEED)
                currentSpeed = MAXSPEED;
            
            return currentSpeed;
        }

        private int getAcceleration(int speed, float trigger)
        {
            
            if (trigger <= 0.09f)
            {
                return -1;
            }
            else
            {
                if (trigger > 0.7f)
                    return 3;
                if(trigger > 0.5f && trigger <= 0.7f)
                    return 2;
                if ((trigger > 0.1f && trigger <= 0.5f) && speed > MAXSPEED / 2)
                    return -1;
                if ((trigger > 0.1f && trigger <= 0.5f) && speed < MAXSPEED / 2)
                    return 1;
                else
                    return 0;                    
            }
            
        }

        private void moveCar(int direction, int speed)
        {
            int moveDistance = calcMoveDistance(speed);
            switch (direction)
            { 
                case 0:             //up
                    currentPosition.Y -= moveDistance;
                    break;
                case 1:             //down
                    currentPosition.Y += moveDistance;
                    break;
                case 2:             //left
                    currentPosition.X -= moveDistance;
                    break;
                case 3:             //right
                    currentPosition.X += moveDistance;
                    break;
                default:
                    break;
            }
        }
        private int calcMoveDistance(int speed)
        {
            if(speed == 0)
                return 0;
            if(speed > 0 && speed <= 5)
                return 1;
            if(speed > 5 && speed <= 10)
                return 2;
            if(speed > 10 && speed <= 15)
                return 3;
            if(speed > 15 && speed <= 20)
                return 4;
            if(speed > 20 && speed <= 25)
                return 5;
            if(speed >25 && speed <= MAXSPEED)
                return 6;
            if (true)
                return 0;
        }
        
    }   
}
