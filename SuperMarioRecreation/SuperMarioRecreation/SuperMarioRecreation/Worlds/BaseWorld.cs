using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SuperMarioRecreation.Worlds
{
    public interface BaseWorld
    {
        void update(GameTime gameTime);
        void draw(GameTime gameTime, SpriteBatch spriteBatch);
        void setBackPos(Point point);
        Point getBackPos();

        void initWorld();
        void initBBoxes();
        void tubeLevelChange(int tubeIndex);
        void worldChange(int worldIndex);

        Rectangle[] getRoofBBoxes();
        Rectangle[] getWallBBoxes();
        Rectangle[] getTubeBBoxes();
        Rectangle[] getFloorBBoxes();

        Texture2D getCurrentBackground();
    }
}
