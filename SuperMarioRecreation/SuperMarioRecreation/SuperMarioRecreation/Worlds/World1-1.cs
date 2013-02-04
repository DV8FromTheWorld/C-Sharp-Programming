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
        Rectangle[] tubeBBoxes;         //TODO:  Tube BBoxes (will be used for level and world changes)
        Rectangle[] floorBBoxes;        //TODO:  Floor BBoxes (needs to include tubeBBoxes)

        Texture2D[] backgrounds;        //Array of backgrounds for current world
        Texture2D aboveground;          
        Texture2D underground;
        Texture2D currentBackground;

        Point backgroundPosition;
        ContentManager content;

        public void initWorld()
        {
            content = Program.baseGame.Content;
            aboveground = content.Load<Texture2D>("Worlds/World1-1/overworld");
            underground = content.Load<Texture2D>("Worlds/World1-1/underground");
            backgroundPosition = new Point(0, 0);

            backgrounds = new Texture2D[] { aboveground, underground };
            currentBackground = backgrounds[0];
        }

        public void initBBoxes()
        {

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

        public void tubeLevelChange(int tubeIndex)
        {
            switch (tubeIndex)
            {
                case 0:
                    currentBackground = underground;
                    backgroundPosition = new Point(0, 0);
                    break;
                case 1:
                    currentBackground = aboveground;
                    backgroundPosition = new Point(0, 0);
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }


        public void worldChange(int worldIndex)
        {
            if (worldIndex == 0)
            {
                //TODO: World change to next world (World 1-2)
            }
            else
            {
                throw new IndexOutOfRangeException("World index provided to worldChange() for world 1-1 was in in the known worlds that could be changed too");

            }
        }
    }
}