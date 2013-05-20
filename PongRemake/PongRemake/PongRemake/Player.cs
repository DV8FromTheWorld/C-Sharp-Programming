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
        private AI computer;
        public String name;
        public Color color;
        public Rectangle position;
        public PlayerIndex playerIndex;
        public bool isAI;
        public bool leftSidePlayer;
        public Vector2 scorePos;
        public int score;
        public Rectangle[] collisionBoxes;
        public Texture2D texture
        {
            get { return GameBase.playerBar; }
        }

        public Player(String name, PlayerIndex playerIndex, bool isAI, Color color, bool leftSidePlayer)
        {
            this.collisionBoxes = new Rectangle[6];
            this.leftSidePlayer = leftSidePlayer;
            ResetPosition();
            this.name = name;
            this.score = 0;
            this.playerIndex = playerIndex;
            this.isAI = isAI;
            if (isAI)
                computer = new AI();
            this.color = color;
            if (leftSidePlayer)
                scorePos = new Vector2((GameBase.monitorWidth / 4), GameBase.monitorHeight / 20);
            else
                scorePos = new Vector2((GameBase.monitorWidth / 4)*3, GameBase.monitorHeight / 20);
        }

        public void Update(PongBall ball)
        {
            if (!isAI)
            {
                position.Y = Updater.mouse.Y - (position.Height / 2);   //Currently will be used for player 1
                //MULTPLAYER TODO: Send packet to other player(s) / Server
                if (!Updater.gameEngine.multiplayer)
                {
                    if (Updater.kbCurrent.IsKeyDown(Keys.Escape) && Updater.kbOld.IsKeyUp(Keys.Escape))
                    {
                        Updater.SwitchScreenNoFade(GameScreen.PAUSED);
                    }
                }
            }
            else
            {
                computer.MovePlayer(this, ball);
            }
            UpdateCollisionBoxes();
        }

        private void UpdateCollisionBoxes()
        {
            for (int i = 0; i < 6; i++)
            {
                if (leftSidePlayer)
                    this.collisionBoxes[i] = new Rectangle(position.X + ((position.Width / 4) * 3), position.Y + ((position.Height / 6) * i), position.Width / 4, position.Height / 6);
                else
                    this.collisionBoxes[i] = new Rectangle(position.X + (position.Width / 4), position.Y + ((position.Height / 6) * i), position.Width / 4, position.Height / 6);
            }
        }

        public void PlayCollisionSound()
        { 
            //TODO: Play sound
        }

        public void ResetPosition()
        {
            int width = 26;
            int height = Updater.viewport.Height / 5;
            if (leftSidePlayer)
                this.position = new Rectangle(GameBase.monitorWidth / 16, GameBase.monitorHeight / 2 - height / 2, width, height);
            else
                this.position = new Rectangle((GameBase.monitorWidth / 16) * 15, GameBase.monitorHeight / 2 - height / 2, width, height);
            UpdateCollisionBoxes();
        }
    }
}
