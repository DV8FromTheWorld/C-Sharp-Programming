using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

namespace PongRemake
{
    class Updater
    {
        public static KeyboardState kbCurrent;
        public static KeyboardState kbOld;

        public static GamePadState pad1;
        public static GamePadState pad2;
        public static GamePadState padOld1;
        public static GamePadState padOld2;

        public static MouseState mouse;

        public static bool fadeChange;

        public static Game gameInstance;

        public static GameEngine gameEngine;

        public static Viewport viewport;

        //Start screen positions for text drawing
        public static Vector2[] titleScreenTextPos = new Vector2[3]; // 0: Text.TITLE.Start   1: Text.TITLE.Options  2: Text.TITLE.Quit
        public static Vector2[] quitScreenTextPos = new Vector2[3];  // 0: Text.QUIT.Yes      1: Text.QUIT.No        2: Text.QUIT.QuitMessage  
        public static Vector2[] pauseScreenTextPos = new Vector2[4]; // 0: Text.PAUSE.Resume  1: Text.PAUSE.Restart  2: Text.PAUSE.Options     3: Text.PAUSE.Quit


        //Start screen rectangles for menu selections
        public static Rectangle[] titleScreenTextRec = new Rectangle[3];// 0: Text.TITLE.Start  1: Text.TITLE.Options  2: Text.TITLE.Quit
        public static Rectangle[] quitScreenTextRec = new Rectangle[2]; // 0: Text.Quit.Yes     1: Text.Quit.No
        public static Rectangle[] pauseScreenTextRec = new Rectangle[4];// 0: Text.PAUSE.Resume 1: Text.PAUSE.Restart  2: Text.PAUSE.Options     3: Text.PAUSE.EndGame

        //Stores the transparencies for the current screen and the screen that is being switched to
        //Used for fadeChange.
        public static float currentTransparency = 1.0f;
        public static float nextTransparency = 0.0f;

        public static String oldRendering;      //Contains the previous state of rendering
        public static String currentRendering;  //Stores the current screen state that is being rendered
        public static String nextRendering;     //Contains the next state that will be rendered (used for fade change)

        public static void InitializeVariables()
        {
            //Text positions for Start Screen text
            titleScreenTextPos[0] = new Vector2(Text.AlignCenter(Text.TITLE.Start), GameBase.monitorHeight / 2 - 100);
            titleScreenTextPos[1] = new Vector2(Text.AlignCenter(Text.TITLE.Options), GameBase.monitorHeight / 2);
            titleScreenTextPos[2] = new Vector2(Text.AlignCenter(Text.TITLE.Quit), GameBase.monitorHeight / 2 + 100);

            //Text positions for the Quit Screen text
            quitScreenTextPos[0] = new Vector2(Text.AlignLeft(Text.QUIT.Yes), GameBase.monitorHeight / 2);
            quitScreenTextPos[1] = new Vector2(Text.AlignRight(Text.QUIT.No), GameBase.monitorHeight / 2);
            quitScreenTextPos[2] = new Vector2(Text.AlignCenter(Text.QUIT.QuitMessage), GameBase.monitorHeight / 4);

            //Text positions for the Pause Screen text
            pauseScreenTextPos[0] = new Vector2(Text.AlignCenter(Text.PAUSE.Resume), GameBase.monitorHeight / 2 - 75);
            pauseScreenTextPos[1] = new Vector2(Text.AlignCenter(Text.PAUSE.Restart), GameBase.monitorHeight / 2 - 25);
            pauseScreenTextPos[2] = new Vector2(Text.AlignCenter(Text.PAUSE.Options), GameBase.monitorHeight / 2 + 25);
            pauseScreenTextPos[3] = new Vector2(Text.AlignCenter(Text.PAUSE.EndGame), GameBase.monitorHeight / 2 + 75);

            //Rectangles for Start Screen Text to check for mouse clicks for menu items
            titleScreenTextRec[0] = new Rectangle((int)titleScreenTextPos[0].X, (int)titleScreenTextPos[0].Y, (int)Drawing.myFont.MeasureString(Text.TITLE.Start).X, (int)Drawing.myFont.MeasureString(Text.TITLE.Start).Y);
            titleScreenTextRec[1] = new Rectangle((int)titleScreenTextPos[1].X, (int)titleScreenTextPos[1].Y, (int)Drawing.myFont.MeasureString(Text.TITLE.Options).X, (int)Drawing.myFont.MeasureString(Text.TITLE.Options).Y);
            titleScreenTextRec[2] = new Rectangle((int)titleScreenTextPos[2].X, (int)titleScreenTextPos[2].Y, (int)Drawing.myFont.MeasureString(Text.TITLE.Quit).X, (int)Drawing.myFont.MeasureString(Text.TITLE.Quit).Y);

            //Rectangles for Quit Screen text to check for mouse clicks for menu items
            quitScreenTextRec[0] = new Rectangle((int)quitScreenTextPos[0].X, (int)quitScreenTextPos[0].Y, (int)Drawing.myFont.MeasureString(Text.QUIT.Yes).X, (int)Drawing.myFont.MeasureString(Text.QUIT.Yes).Y);
            quitScreenTextRec[1] = new Rectangle((int)quitScreenTextPos[1].X, (int)quitScreenTextPos[1].Y, (int)Drawing.myFont.MeasureString(Text.QUIT.No).X, (int)Drawing.myFont.MeasureString(Text.QUIT.No).Y);
            
            //Rectangles for Pause Screen text to check for mouse clicks for menu items
            pauseScreenTextRec[0] = new Rectangle((int)pauseScreenTextPos[0].X, (int)pauseScreenTextPos[0].Y, (int)Drawing.myFont.MeasureString(Text.PAUSE.Resume).X, (int)Drawing.myFont.MeasureString(Text.PAUSE.Resume).Y);
            pauseScreenTextRec[1] = new Rectangle((int)pauseScreenTextPos[1].X, (int)pauseScreenTextPos[1].Y, (int)Drawing.myFont.MeasureString(Text.PAUSE.Restart).X, (int)Drawing.myFont.MeasureString(Text.PAUSE.Restart).Y);
            pauseScreenTextRec[2] = new Rectangle((int)pauseScreenTextPos[2].X, (int)pauseScreenTextPos[2].Y, (int)Drawing.myFont.MeasureString(Text.PAUSE.Options).X, (int)Drawing.myFont.MeasureString(Text.PAUSE.Options).Y);
            pauseScreenTextRec[3] = new Rectangle((int)pauseScreenTextPos[3].X, (int)pauseScreenTextPos[3].Y, (int)Drawing.myFont.MeasureString(Text.PAUSE.EndGame).X, (int)Drawing.myFont.MeasureString(Text.PAUSE.EndGame).Y);

        }

        public static void TitleScreen()
        {
            for (int i = 0; i < 3; i++)
                Colors.titleScreenColors[i] = Color.White;
            if (titleScreenTextRec[0].Contains(mouse.X, mouse.Y))  //Text.TITLE.Start
            {
                Colors.titleScreenColors[0] = Color.Red;
                if (mouse.LeftButton == ButtonState.Pressed)
                    StartNewGame();
            }
            else if (titleScreenTextRec[1].Contains(mouse.X, mouse.Y))  //Text.TITLE.Options
            {
                Colors.titleScreenColors[1] = Color.Red;
                if (mouse.LeftButton == ButtonState.Pressed)
                    SwitchScreen(GameScreen.OPTIONS);
            }
            else if (titleScreenTextRec[2].Contains(mouse.X, mouse.Y))  //Text.TITLE.Quit
            {
                Colors.titleScreenColors[2] = Color.Red;
                if (mouse.LeftButton == ButtonState.Pressed)
                    SwitchScreen(GameScreen.QUIT);
            }

        }

        public static void PauseScreen()
        {
            for (int i = 0; i < 4; i++)
                Colors.pauseScreenColors[i] = Color.White;
            if (pauseScreenTextRec[0].Contains(mouse.X, mouse.Y))        //Text.PAUSE.Resume
            {
                Colors.pauseScreenColors[0] = Color.Red;
                if (mouse.LeftButton == ButtonState.Pressed)
                    SwitchScreenNoFade(GameScreen.PLAYING);
            }
            else if (pauseScreenTextRec[1].Contains(mouse.X, mouse.Y))   //Text.PAUSE.Restart
            {
                Colors.pauseScreenColors[1] = Color.Red;
                //if (mouse.LeftButton == ButtonState.Pressed)
                //TODO : Restart 
            }
            else if (pauseScreenTextRec[2].Contains(mouse.X, mouse.Y))   //Text.PAUSE.Options
            {
                Colors.pauseScreenColors[2] = Color.Red;
                if (mouse.LeftButton == ButtonState.Pressed)
                    SwitchScreen(GameScreen.OPTIONS);
            }
            else if (pauseScreenTextRec[3].Contains(mouse.X, mouse.Y))  //Text.Pause.EndGame
            {
                Colors.pauseScreenColors[3] = Color.Red;
               // if (mouse.LeftButton == ButtonState.Pressed)
                //TODO : End game
            }

        }

        public static void OptionsScreen()
        {

        }

        public static void QuitScreen()
        {
            for (int i = 0; i < 3; i++)
                Colors.quitScreenColors[i] = Color.White;
            if (quitScreenTextRec[0].Contains(mouse.X, mouse.Y))  //Text.QUIT.Yes
            {
                Colors.quitScreenColors[0] = Color.Red;
                if (mouse.LeftButton == ButtonState.Pressed)
                    Updater.gameInstance.Exit();
            }
            else if (quitScreenTextRec[1].Contains(mouse.X, mouse.Y))  //Text.QUIT.No
            {
                Colors.quitScreenColors[1] = Color.Red;
                if (mouse.LeftButton == ButtonState.Pressed)
                    SwitchScreen(GameScreen.TITLE);
            }
        }

        public static void FadeChangeUpdate()
        {
            currentTransparency -= 0.02f;
            nextTransparency += 0.02f;
            if (nextTransparency > 1.0f)
            {
                oldRendering = currentRendering;
                currentRendering = nextRendering;
                currentTransparency = 1.0f;
                nextTransparency = 0.0f;
                fadeChange = false;
            }
        }

        public static void SwitchScreenNoFade(string nextScreen)
        {
            oldRendering = currentRendering;
            currentRendering = nextScreen;
        }

        public static void SwitchScreen(string nextScreen)
        {
            if (currentRendering != GameScreen.PLAYING) 
                Colors.ResetScreenColors(currentRendering);             
            nextRendering = nextScreen;
            fadeChange = true;
        }

        public static void StartNewGame()
        {
            SwitchScreenNoFade(GameScreen.PLAYING);
            gameEngine = new GameEngine();            
        }
    }
}
