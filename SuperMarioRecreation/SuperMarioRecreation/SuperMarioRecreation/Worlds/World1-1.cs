﻿using System;
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


        Rectangle[] roofBBoxes;
        Rectangle[] wallBBoxes;
        Rectangle[] tubeBBoxes;         //TODO:  Tube BBoxes (will be used for level and world changes)
        Rectangle[] floorBBoxes;        //TODO:  Floor BBoxes (needs to include tubeBBoxes)

        Texture2D[] backgrounds;        //Array of backgrounds for current world
        Texture2D abovePart1;
        Texture2D abovePart2;
        Texture2D underground;
        Texture2D currentBackground;

        //Magic Numbers
        Rectangle abovePos1;
        Rectangle abovePos2;

        Point backPos;
        ContentManager content;

        public void initWorld()
        {
            content = Program.baseGame.Content;
            abovePart1 = content.Load<Texture2D>("Worlds/World1-1/overworld_1");
            abovePart2 = content.Load<Texture2D>("Worlds/World1-1/overworld_2");
            underground = content.Load<Texture2D>("Worlds/World1-1/underground");

            //Magic Numbers
            abovePos1 = new Rectangle(0, 0, abovePart1.Width*3, abovePart2.Height*3);
            abovePos2 = new Rectangle(abovePart1.Width * 3, 0, abovePart2.Width * 3, abovePart2.Height * 3);

            backgrounds = new Texture2D[] { abovePart1, abovePart2, underground };
            currentBackground = backgrounds[0];
        }

        public void initBBoxes()
        {
            
        }

        public void update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(abovePart1, abovePos1, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.Draw(abovePart2, abovePos2, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }

        public void tubeLevelChange(int tubeIndex)
        {
            switch (tubeIndex)
            {
                case 0:
                    currentBackground = underground;
                    //backgroundPosition = new Point(0, 0);
                    break;
                case 1:
                    currentBackground = abovePart1;
                    //backgroundPosition = new Point(0, 0);
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

        public void setBackPos(Point point)
        {
            backPos = point;
        }

        public Point getBackPos()
        {
            return backPos;
        }

        public Rectangle[] getRoofBBoxes()
        {
            throw new NotImplementedException();
        }

        public Rectangle[] getWallBBoxes()
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
    }
}
