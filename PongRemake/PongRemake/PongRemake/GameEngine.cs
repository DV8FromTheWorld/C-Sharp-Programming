using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PongRemake
{
    class GameEngine
    {
        public PongBall ball;
        public Player playerOne;
        public Player playerTwo;

        public GameEngine()
        {
            playerOne = new Player("Player 1", PlayerIndex.One, false, Color.White);         //Change to get name and color from options/preferences
            playerTwo = new Player("Player 2", PlayerIndex.Two, true, Color.White);          //Change to get name and color from options/preferences
            ball = new PongBall();
        }

        public void Update()
        {
            playerOne.Update();
            playerTwo.Update();
            ball.CheckPlayerCollision(playerOne);
            ball.CheckPlayerCollision(playerTwo);
            ball.Update();
            if (!ball.isAlive)
            { 
                
            }    
        }
    }
}
