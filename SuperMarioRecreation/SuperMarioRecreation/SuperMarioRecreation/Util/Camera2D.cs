using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SuperMarioRecreation.Util
{
    class Camera2D : ICamera
    {
        Point cameraPos;

        public Camera2D()
            : this(new Point(0, 0)){ }

        public Camera2D(Point position)
        {
            this.cameraPos = position;
        }

        public Point getCameraPosition()
        {
            return cameraPos;
        }

        public void setCameraPosition(Point position)
        {
            this.cameraPos = position;
        }
    }
}
