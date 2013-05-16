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

        public Texture2D texture
        {
            get { return GameBase.ball; }
        }

        bool movingRight;
        float slope;
        int yIntercept;

        public PongBall()
        {
            position = new Rectangle(30, GameBase.monitorHeight, ((GameBase.monitorHeight / 5) / 6) - 12, ((GameBase.monitorHeight / 5) / 6) - 12);
            Reset();
            speed = 12;
            isAlive = true;
        }

        public void Update()
        {
            if (isAlive)
            {
                if (movingRight)
                    position.X += speed;
                else
                    position.X -= speed;
                position.Y = (int)(position.X * slope) + yIntercept;
                CheckWallCollision();
            }
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

            if ((position.X + position.Width) > Updater.viewport.Width)      //Hits the right wall
            {
                SwitchDirection();
                yIntercept = (int)(position.Y + (position.X * slope));
                slope *= -1;
            }
        }

        public void CheckPlayerCollision(Player player, PongBall ball)
        {
            Random rand = new Random();
            float[] deflectionSlopes = new float[] { -.8f, -.5f, -.15f, .15f, .5f, .8f }; 
            for (int i = 0; i < 6; i++)
            { 
                if (player.collisionBoxes[i].Intersects(ball.position))
                {
                        //player.PlayCollisionSound();
                        SwitchDirection();
                        slope = deflectionSlopes[i];
                        yIntercept = (int)(position.Y - (slope * position.X));
                        break;
                }
            }
        }

        public void CheckPastPlayer(Player leftSide, Player rightSide)
        {
            if ((position.X + position.Width + 5 ) < 0)     //Goes beyond leftSide player (playerOne)                             
            {
                rightSide.score += 1;
                isAlive = false;
                //TODO: play sound for player scoring
            }

            if ((position.X + 5) > GameBase.monitorWidth)   //Goes beyond rightSide player (playerTwo)
            {
                leftSide.score += 1;
                isAlive = false;
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
        }

        public void SwitchDirection()
        {
            if (movingRight)
                movingRight = false;
            else
                movingRight = true;
        }
    }
}
