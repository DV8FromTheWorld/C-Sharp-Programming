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
            slope = 0.5f;
            speed = 12;
            movingRight = true;
            isAlive = true;
        }

        public void Update()
        {
            if (movingRight)
                position.X += speed;
            else
                position.X -= speed;
            position.Y = (int)(position.X * slope) + yIntercept;
            CheckWallCollision();
            //CheckPastPlayer();
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

            if (position.X < 0)                                  //Hits the left wall
            {
                SwitchDirection();
                yIntercept = (int)(position.Y + (position.X * slope));
                slope *= -1;
            }

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
                        SwitchDirection();
                        slope = deflectionSlopes[i];
                        yIntercept = (int)(position.Y - (slope * position.X));
                        break;
                }
            }
        }

        public void Destroy(bool completelyDestroy)
        {
            isAlive = false;
        }

        public void Reset()
        {

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
