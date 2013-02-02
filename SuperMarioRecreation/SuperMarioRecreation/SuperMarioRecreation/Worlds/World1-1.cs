using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SuperMarioRecreation.Worlds
{
    public class World1_1 : BaseWorld
    {

        Texture2D worldBackground;


        public World1_1(Texture2D worldTex, Rectangle dementions)
        {
            worldBackground = Program.baseGame.Content.Load<Texture2D>("world 1-1");
            this.worldBackground = worldTex;
            
        }
    }
}
