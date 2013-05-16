using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongRemake
{
    class GameEngine
    {
        public PongBall ball;
        public Player playerOne;
        public Player playerTwo;
        public bool multiplayer;

        public GameEngine()
        {
            playerOne = new Player("Player 1", PlayerIndex.One, false, Color.White);         //Change to get name and color from options/preferences
            playerTwo = new Player("Player 2", PlayerIndex.Two, true, Color.White);          //Change to get name and color from options/preferences
            ball = new PongBall();
            multiplayer = false;
        }

        public int timer = 0;
        public void Update()
        {
            if (timer != 60)
                timer++;
            else
            {
                if (ball.isAlive)
                {
                    ball.Update();
                    playerOne.Update();
                    //playerTwo.Update();
                    ball.CheckPlayerCollision(playerOne, ball);
                    //ball.CheckPlayerCollision(playerTwo, ball);
                    ball.CheckPastPlayer(playerOne, playerTwo);
                }
                else
                { 
                    //if (
                    ////TODO: Play sound for scoring a point
                    //playerOne.Reset();
                    //playerTwo.Reset();
                    ball.Reset();

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ball.isAlive)
                spriteBatch.Draw(ball.texture, ball.position, Color.White);
            
            spriteBatch.Draw(playerOne.texture, playerOne.position, playerOne.color);
            Color[] color = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Yellow, Color.Purple };
            for (int i = 0; i < 6; i++)
                spriteBatch.Draw(playerOne.texture, playerOne.collisionBoxes[i], color[i]);
        }
    }
}
