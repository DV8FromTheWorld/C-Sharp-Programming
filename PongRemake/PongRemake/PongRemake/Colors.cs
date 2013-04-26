using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PongRemake
{
    class Colors
    {
        public static Color[] titleScreenColors; // 0: Text.TITLE.Start   1: Text.TITLE.Options  2: Text.TITLE.Quit
        public static Color[] quitScreenColors;  // 0: Text.QUIT.Yes      1: Text.QUIT.No        2: Text.QUIT.QuitMessage
        public static Color[] pauseScreenColors; // 0: Text.PAUSE.Resume  1: Text.PAUSE.Restart  2: Text.PAUSE.Options     3: Text.PAUSE.EndGame

        public static void PopulateColors()
        {
            titleScreenColors = new Color[] { Color.White, Color.White, Color.White };
            quitScreenColors = new Color[] { Color.White, Color.White, Color.White };
            pauseScreenColors = new Color[] { Color.White, Color.White, Color.White, Color.White };
        }

        public static void ResetScreenColors(string screen)
        {
            switch (screen)
            {
                case GameScreen.TITLE:
                    titleScreenColors = new Color[] { Color.White, Color.White, Color.White };
                    break;
                case GameScreen.QUIT:
                    quitScreenColors = new Color[] { Color.White, Color.White, Color.White };
                    break;
                case GameScreen.PAUSED:
                    pauseScreenColors = new Color[] { Color.White, Color.White, Color.White, Color.White };
                    break;
                default:
                    throw new Exception("Error in switch statement in the ResetScreenColors method.  Did not suit any of the screen types");

            }
        }
    }
}
