using System;

namespace SuperMarioRecreation
{
#if WINDOWS || XBOX
    static class Program
    {

        public static Game1 baseGame;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                baseGame = game;
                game.Run();
            }
        }
    }
#endif
}

