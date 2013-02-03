using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SuperMarioRecreation.Worlds
{
    class World1_1 : BaseWorld
    {
        Rectangle[] tubeBBoxes;
        Rectangle[] floorBBoxes;

        Texture2D[] backgrounds;
        Texture2D aboveGround;
        Texture2D currentBackground;

        Rectangle backgroundPosition;
        ContentManager content;

        public void initWorld()
        {
            content = Program.baseGame.Content;
            aboveGround = content.Load<Texture2D>("Worlds/World1-1/world 1-1");

            backgrounds = new Texture2D[] { aboveGround };
            currentBackground = backgrounds[0];
        }

        public void initBBoxes()
        {
            throw new NotImplementedException();
        }

        public Rectangle[] getTubeBBoxes()
        {
            return tubeBBoxes;
        }

        public Rectangle[] getFloorBBoxes()
        {
            throw new NotImplementedException();
        }

        public Texture2D getCurrentBackground()
        {
            return currentBackground;
        }

        public Rectangle getCurrentBackgroundPos()
        {
            return backgroundPosition;
        }

    }
}
