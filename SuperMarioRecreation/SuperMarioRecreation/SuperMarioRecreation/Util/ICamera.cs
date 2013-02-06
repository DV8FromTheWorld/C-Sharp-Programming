using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SuperMarioRecreation.Util
{
    interface ICamera
    {
        Point getCameraPosition();
        void setCameraPosition(Point position);
    }
}
