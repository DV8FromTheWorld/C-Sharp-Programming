using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongRemake
{
    class PongBall
    {
        public Rectangle position;
        public int speed;
        public bool isAlive;
        public bool drawBall;
        public Color color;

        public int collisionCooldown;

        public Texture2D texture
        {
            get { return GameBase.ball; }
        }
        int redIntensity, greenIntensity, blueIntensity;
        Boolean redIncrease, greenIncrease, blueIncrease;

        bool movingRight;
        float slope;
        int yIntercept;

        public PongBall()
        {
            Random rand  = new Random();
            position = new Rectangle(30, GameBase.monitorHeight, ((GameBase.monitorHeight / 5) / 6) - 12, ((GameBase.monitorHeight / 5) / 6) - 12);
            redIntensity = 0;       //Should not all start at 0 because it will just go from black to white due to color balance.
            greenIntensity = 127;
            blueIntensity = 255;
            Reset();
            speed = 12;
            isAlive = true;
            collisionCooldown = 0;
            if(Options.rainbowBallEnabled)
                color = GetRainbowColor();
            else
                color = Color.White;
        }

        public void Update()
        {
            if (isAlive)
            {
                if (!drawBall)
                    drawBall = true;
                if (movingRight)
                    position.X += speed;
                else
                    position.X -= speed;
                position.Y = (int)(position.X * slope) + yIntercept;
                CheckWallCollision();
                if (Options.rainbowBallEnabled)
                    color = GetRainbowColor();
                else
                    color = Color.White;
            }
            else
                drawBall = false;
        }

        private void CheckWallCollision()
        {
            if (position.Y < 0)                                  //Hits the roof
            {
                yIntercept = -(int)(position.Y - (position.X * slope));
                slope *= -1;
            }

            if ((position.Y + position.Height) > Updater.viewport.Height)    //Hits the floor
            {

                yIntercept = (int)(position.Y + (position.X * slope));
                slope *= -1;
            }

            //if (position.X < 0)                                  //Hits the left wall
            //{
            //    SwitchDirection();
            //    yIntercept = (int)(position.Y + (position.X * slope));
            //    slope *= -1;
            //}

            //if ((position.X + position.Width) > Updater.viewport.Width)      //Hits the right wall
            //{
            //    SwitchDirection();
            //    yIntercept = (int)(position.Y + (position.X * slope));
            //    slope *= -1;
            //}
        }

        public void CheckPlayerCollision(Player player, PongBall ball)
        {
            Random rand = new Random();
            float[] deflectionSlopes = new float[] { -.8f, -.5f, -.15f, .15f, .5f, .8f }; 
            for (int i = 0; i < 6; i++)
            {
                if (collisionCooldown == 0)
                {
                    if (player.collisionBoxes[i].Intersects(ball.position))
                    {
                        //player.PlayCollisionSound();
                        SwitchDirection();
                        if (player.leftSidePlayer)
                            slope = deflectionSlopes[i];
                        else
                            slope = -deflectionSlopes[i];

                        yIntercept = (int)(position.Y - (slope * position.X));
                        collisionCooldown++;
                        break;
                    }
                }
                else
                {
                    collisionCooldown++;
                    if (collisionCooldown >= 60)
                        collisionCooldown = 0;
                }
                    
            }
        }

        public void CheckPastPlayer(Player leftSide, Player rightSide)
        {
            if ((position.X + position.Width + 5 ) < 0)     //Goes beyond leftSide player (playerOne)                             
            {
                rightSide.score += 1;
                isAlive = false;
                drawBall = false;
                //TODO: play sound for player scoring
            }

            if ((position.X + 5) > GameBase.monitorWidth)   //Goes beyond rightSide player (playerTwo)
            {
                leftSide.score += 1;
                isAlive = false;
                drawBall = false;
                //TODO: play sound for player scoring
            }
        }

        public void Reset()
        {
            float[] randomSlopes = new float[] { -.8f, -.5f, -.15f, .15f, .5f, .8f }; 
            Random rand = new Random();
            position.X = rand.Next((GameBase.monitorWidth / 2) - ((GameBase.monitorWidth / 4) / 2), (GameBase.monitorWidth / 2) + ((GameBase.monitorWidth / 4) / 4));
            position.Y = rand.Next((GameBase.monitorHeight / 2) - ((GameBase.monitorHeight / 4) / 2), (GameBase.monitorHeight / 2) + ((GameBase.monitorHeight / 4) / 4));
            slope = randomSlopes[rand.Next(0, 6)];
            yIntercept = (int)(position.Y - (position.X * slope));
            if (rand.NextDouble() > 0.5)
                movingRight = true;
            else
                movingRight = false;
            isAlive = true;
        }

        private void SwitchDirection()
        {
            if (movingRight)
                movingRight = false;
            else
                movingRight = true;
        }

        private Color GetRainbowColor()
        {
            if (redIntensity == 0) redIncrease = true;
            if (redIntensity == 255) redIncrease = false;
            if (redIncrease) redIntensity++; else redIntensity--;

            if (blueIntensity == 0) blueIncrease = true;
            if (blueIntensity == 255) blueIncrease = false;
            if (blueIncrease) blueIntensity++; else blueIntensity--;

            if (greenIntensity == 0) greenIncrease = true;
            if (greenIntensity == 255) greenIncrease = false;
            if (greenIncrease) greenIntensity++; else greenIntensity--;

            return new Color(redIntensity, greenIntensity, blueIntensity);
        }
    }
}
