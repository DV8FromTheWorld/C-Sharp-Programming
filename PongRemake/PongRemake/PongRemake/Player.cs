using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongRemake
{
    class Player
    {

        public String name;
        public Color color;
        public int score;
        public Rectangle position;
        public PlayerIndex playerIndex;
        public bool isAI;
        public Texture2D texture
        {
            get { return GameBase.playerBar; }
        }
        public Rectangle[] collisionBoxes;

        public Player(String name, PlayerIndex playerIndex, bool isAI, Color color)
        {
            this.name = name;
            this.score = 0;
            this.position = new Rectangle(100, 100, 26, Updater.viewport.Height/5);
            this.playerIndex = playerIndex;
            this.isAI = isAI;
            this.color = color;
            this.collisionBoxes = new Rectangle[6];
            for (int i = 0; i < 6; i++)
            {
                this.collisionBoxes[i] = new Rectangle(position.X, (position.Y/6) * (i+1), position.Width, position.Height / 6);
            }
        }

        public void Update()
        {
            if (!isAI)
            {
                position.Y = Updater.mouse.Y - (position.Height / 2);   //Currently will be used for player 1
                //MULTPLAYER TODO: Send packet to other player(s) / Server
                UpdateCollisionBoxes();
                
            }
            else
            { 
                //TODO: Create AI
            }
        }

        private void UpdateCollisionBoxes()
        {
            for (int i = 0; i < 6; i++)
            {
                this.collisionBoxes[i] = new Rectangle(position.X + position.Width / 2, position.Y + ((position.Height / 6) * i), position.Width / 2, position.Height / 6);
            }
        }
    }
}
