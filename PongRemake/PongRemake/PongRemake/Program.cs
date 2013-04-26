using System;

namespace PongRemake
{
#if WINDOWS || XBOX
    static class Program
    {
        public static GameBase instance;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameBase game = new GameBase())
            {
                instance = game;
                game.Run();
            }
        }
    }
#endif
}

