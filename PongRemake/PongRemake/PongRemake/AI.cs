using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PongRemake
{
    class AI
    {
        public AI()
        { }

        public void MovePlayer(Player player, PongBall ball)
        {
            int speed = ball.speed/2+3;
            int playerTopPoint = player.position.Y;
            int playerBottomPoint = player.position.Y + player.position.Height;
            if (!((ball.position.Y > playerTopPoint) && (ball.position.Y < playerBottomPoint)))
            {
                if (ball.position.Y > playerBottomPoint)
                    player.position.Y += speed;
                if (ball.position.Y < playerTopPoint)
                    player.position.Y -= speed;
            }
        }
    }
}
