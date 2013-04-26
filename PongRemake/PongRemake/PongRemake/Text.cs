using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PongRemake
{
    class Text
    {
        public static TextQuit QUIT = new TextQuit();
        public static TextTitle TITLE = new TextTitle();
        public static TextPause PAUSE = new TextPause();

        public static float AlignCenter(string text)
        {
            return (GameBase.monitorWidth / 2) - (Drawing.myFont.MeasureString(text).X / 2);
        }

        public static float AlignRight(string text)
        {
            return ((GameBase.monitorWidth / 4) * 3) - (Drawing.myFont.MeasureString(text).X / 2);
        }

        public static float AlignLeft(string text)
        {
            return (GameBase.monitorWidth / 4) - (Drawing.myFont.MeasureString(text).X / 2);
        }

        public class TextQuit
        {
            public string QuitMessage = "Are you sure you want quit?";
            public string Yes = "Yes";
            public string No = "No";           
        }

        public class TextTitle
        {
            public string Start = "Start Game";
            public string Options = "Options";
            public string Quit = "Quit Game";
        }

        public class TextPause
        {
            public string Resume = "Resume Game";
            public string Restart = "Restart Game";
            public string Options = "Options";
            public string EndGame = "End Game";
        }
    }
}
