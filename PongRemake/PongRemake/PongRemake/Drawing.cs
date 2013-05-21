using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PongRemake
{
    class Drawing
    {

        public static SpriteBatch spriteBatch;
        public static SpriteFont myFont;

        public static void TitleScreen() { TitleScreen(1.0f); }
        public static void TitleScreen(float transparency)
        {
            spriteBatch.DrawString(myFont, Text.TITLE.Start, Updater.titleScreenTextPos[0], Colors.titleScreenColors[0] * transparency);
            spriteBatch.DrawString(myFont, Text.TITLE.Options, Updater.titleScreenTextPos[1], Colors.titleScreenColors[1] * transparency);
            spriteBatch.DrawString(myFont, Text.TITLE.Quit, Updater.titleScreenTextPos[2], Colors.titleScreenColors[2] * transparency);
        }

        public static void PauseScreen() { PauseScreen(1.0f); } 
        public static void PauseScreen(float transparency)
        {
            spriteBatch.DrawString(myFont, Text.PAUSE.Resume, Updater.pauseScreenTextPos[0], Colors.pauseScreenColors[0] * transparency);
            spriteBatch.DrawString(myFont, Text.PAUSE.Restart, Updater.pauseScreenTextPos[1], Colors.pauseScreenColors[1] * transparency);
            spriteBatch.DrawString(myFont, Text.PAUSE.Options, Updater.pauseScreenTextPos[2], Colors.pauseScreenColors[2] * transparency);
            spriteBatch.DrawString(myFont, Text.PAUSE.EndGame, Updater.pauseScreenTextPos[3], Colors.pauseScreenColors[3] * transparency);
        }

        public static void GamePlayingScreen() { GamePlayingScreen(1.0f); }
        public static void GamePlayingScreen(float transparency) { Updater.gameEngine.Draw(spriteBatch); }

        public static void OptionsScreen() { OptionsScreen(1.0f); } 
        public static void OptionsScreen(float transparency)
        {
            Options.Draw(spriteBatch, transparency);   
        }

        public static void QuitScreen() { QuitScreen(1.0f); }
        public static void QuitScreen(float transparency)
        {
                spriteBatch.DrawString(myFont, Text.QUIT.Yes, Updater.quitScreenTextPos[0], Colors.quitScreenColors[0] * transparency);
                spriteBatch.DrawString(myFont, Text.QUIT.No, Updater.quitScreenTextPos[1], Colors.quitScreenColors[1] * transparency);
                spriteBatch.DrawString(myFont, Text.QUIT.QuitMessage, Updater.quitScreenTextPos[2], Colors.quitScreenColors[2] * transparency);
        }

        public static void Debug()
        {
            //spriteBatch.DrawString(myFont, "X: " + Updater.mouse.X + "  Y: " + Updater.mouse.Y, new Vector2(100, 100), Color.White);
            //spriteBatch.DrawString(myFont, "R : " + Color.Red.R + "  G: " + Color.Red.G + "  B: " + Color.Red.B + "  A: " + Color.Red.A, new Vector2(100, 150), Color.White);
        }

        public static void FadeChangeDraw()
        {
            switch (Updater.currentRendering)
            {
                case GameScreen.TITLE:
                    TitleScreen(Updater.currentTransparency);
                    break;
                case GameScreen.PAUSED:
                    PauseScreen(Updater.currentTransparency);
                    break;
                case GameScreen.OPTIONS:
                    OptionsScreen(Updater.currentTransparency);
                    break;
                case GameScreen.QUIT:
                    QuitScreen(Updater.currentTransparency);
                    break;
                case GameScreen.PLAYING:
                    GamePlayingScreen(Updater.currentTransparency);
                    break;
                default:
                    throw new Exception("fadeChange's Switch statement could understand which screen to draw as currentRendering.");
            }
            switch (Updater.nextRendering)
            {
                case GameScreen.TITLE:
                    TitleScreen(Updater.nextTransparency);
                    break;
                case GameScreen.PAUSED:
                    PauseScreen(Updater.nextTransparency);
                    break;
                case GameScreen.OPTIONS:
                    OptionsScreen(Updater.nextTransparency);
                    break;
                case GameScreen.QUIT:
                    QuitScreen(Updater.nextTransparency);
                    break;
                case GameScreen.PLAYING:
                    GamePlayingScreen(Updater.nextTransparency);
                    break;
                default:
                    throw new Exception("fadeChange's Switch statement could understand which screen to draw as nextRendering.");
            }
        }
    }
}
