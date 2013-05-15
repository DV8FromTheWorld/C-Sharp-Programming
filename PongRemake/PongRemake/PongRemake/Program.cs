using System;

namespace PongRemake
{
#if WINDOWS
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameBase game = new GameBase())
            {
                Updater.gameInstance = game;
                game.Run();
            }
        }
    }
#endif
}

