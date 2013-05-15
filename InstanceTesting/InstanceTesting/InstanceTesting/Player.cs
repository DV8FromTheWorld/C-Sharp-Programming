using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstanceTesting
{
    class Player
    {
        public String name;

        public Player(String name)
        {
            if (name != null)
                this.name = name;
        }

    }
}
