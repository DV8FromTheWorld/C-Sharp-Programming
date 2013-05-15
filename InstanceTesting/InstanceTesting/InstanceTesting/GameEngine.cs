using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstanceTesting
{
    class GameEngine
    {
        public Player playerOne;
        public Player playerTwo;

        public GameEngine() :
            this("cat", "apple") { }

        public GameEngine(String name1, String name2)
        {
            playerOne = new Player(name1);
            playerTwo = new Player(name2);
        }
    }
}
