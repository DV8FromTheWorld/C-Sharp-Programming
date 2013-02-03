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
        void initWorld();
        void initBBoxes();

        Rectangle[] getTubeBBoxes();
        Rectangle[] getFloorBBoxes();


        Texture2D getCurrentBackground();
        Rectangle getCurrentBackgroundPos();
    }
}
