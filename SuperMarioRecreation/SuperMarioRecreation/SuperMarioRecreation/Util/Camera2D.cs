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

        //Creates an instance of the camera at the default 0,0 position
        public Camera2D()
            : this(new Point(0, 0)){ }

        //Creates an instance of the camera at the point specified.
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
