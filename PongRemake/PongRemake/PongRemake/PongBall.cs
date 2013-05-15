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
            position = new Rectangle(30, GameBase.monitorHeight / 2, 25, 25);
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
                movingRight = true;
                yIntercept = (int)(position.Y + (position.X * slope));
                slope *= -1;
            }

            if ((position.X + position.Width) > Updater.viewport.Width)      //Hits the right wall
            {
                movingRight = false;
                yIntercept = (int)(position.Y + (position.X * slope));
                slope *= -1;
            }
        }

        public void CheckPlayerCollision(Player player, PongBall ball)
        {

            for (int i = 0; i < 6; i++)
            { 
                if (player.collisionBoxes[i].Contains(ball.position))
                {

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
    }
}
