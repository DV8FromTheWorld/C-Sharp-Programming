﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PongRemake
{
    class Options
    {
        //File manipulation (loading, writing, etc)
        private static string optionsFilePath;
        private static string currentDir;
        private static StreamReader sr;
        private static StreamWriter sw;

        //Options
        public static string playerName;
        public static Color playerColor;
        public static bool controllerEnabled;
        public static bool rainbowBallEnabled;

        //Temp Options (player hasn't pressed "save options" yet)
        private static string tempPlayerName;
        private static Color tempPlayerColor;
        private static bool tempControllerEnabled;
        private static bool tempRainbowBallEnabled;

        private static Vector2[] controllerTextPos;
        private static Vector2[] ballTextPos;

        private static float fontSizeIncrease;

        private static Rectangle saveRec;
        private static Rectangle cancelRec;
        private static Rectangle resetRec;

        private static Rectangle[] redControlsRec;
        private static Rectangle[] greenControlsRec;
        private static Rectangle[] blueControlsRec;

        public static void Load()
        {
            currentDir = Directory.GetCurrentDirectory();
            optionsFilePath = currentDir + "/Pong-Remake/Options.txt";
            if (Directory.Exists(currentDir + "/Pong-Remake/"))
                CreateOrLoadOptions();
            else
            {
                Directory.CreateDirectory(currentDir + "/Pong-Remake/");
                CreateOrLoadOptions();
            }
            Initalize();
        }

        private static void Initalize()
        {
            fontSizeIncrease = 1.5f;
            controllerTextPos = new Vector2[] { new Vector2(20, 100), new Vector2(300, 100), new Vector2(700, 100) };
            ballTextPos = new Vector2[] { new Vector2(20, 150), new Vector2(300, 150), new Vector2(700, 150) };
            saveRec = new Rectangle((int)(GameBase.monitorWidth / 4 - Drawing.myFont.MeasureString("Save Options").X / 2), GameBase.monitorHeight / 5 * 4, (int)Drawing.myFont.MeasureString("Save Options").X, (int)Drawing.myFont.MeasureString("Save Options").Y);
            cancelRec = new Rectangle((int)(GameBase.monitorWidth / 4*2 - Drawing.myFont.MeasureString("Cancel").X / 2), GameBase.monitorHeight / 5 * 4, (int)Drawing.myFont.MeasureString("Cancel").X, (int)Drawing.myFont.MeasureString("Cancel Options").Y);
            resetRec = new Rectangle((int)(GameBase.monitorWidth / 4*3 - Drawing.myFont.MeasureString("Reset Options").X / 2), GameBase.monitorHeight / 5 * 4, (int)Drawing.myFont.MeasureString("Reset Options").X, (int)Drawing.myFont.MeasureString("Reset Options").Y);
            redControlsRec = new Rectangle[2];
            redControlsRec[0] = new Rectangle(GameBase.monitorWidth / 16 * 8, GameBase.monitorHeight / 16 * 7, (int)(Drawing.myFont.MeasureString("+").X * fontSizeIncrease), (int)(Drawing.myFont.MeasureString("+").Y * fontSizeIncrease));
            redControlsRec[1] = new Rectangle(GameBase.monitorWidth / 16 * 8 + 40, GameBase.monitorHeight / 16 * 7, (int)(Drawing.myFont.MeasureString("-").X * fontSizeIncrease), (int)(Drawing.myFont.MeasureString("-").Y * fontSizeIncrease));
            greenControlsRec = new Rectangle[2];
            greenControlsRec[0] = new Rectangle(GameBase.monitorWidth / 16 * 8, GameBase.monitorHeight / 16 * 8, (int)(Drawing.myFont.MeasureString("+").X * fontSizeIncrease), (int)(Drawing.myFont.MeasureString("+").Y * fontSizeIncrease));
            greenControlsRec[1] = new Rectangle(GameBase.monitorWidth / 16 * 8 + 40, GameBase.monitorHeight / 16 * 8, (int)(Drawing.myFont.MeasureString("-").X * fontSizeIncrease), (int)(Drawing.myFont.MeasureString("-").Y * fontSizeIncrease));
            blueControlsRec = new Rectangle[2];
            blueControlsRec[0] = new Rectangle(GameBase.monitorWidth / 16 * 8, GameBase.monitorHeight / 16 * 9, (int)(Drawing.myFont.MeasureString("+").X * fontSizeIncrease), (int)(Drawing.myFont.MeasureString("+").Y * fontSizeIncrease));
            blueControlsRec[1] = new Rectangle(GameBase.monitorWidth / 16 * 8 + 40, GameBase.monitorHeight / 16 * 9, (int)(Drawing.myFont.MeasureString("-").X * fontSizeIncrease), (int)(Drawing.myFont.MeasureString("-").Y * fontSizeIncrease));
            ResetTempVariables();
        }

        public static void Update()
        {
            Vector2 temp;       //Reused multiple times to save ram
            string[] tempText;  //Reused multiple times to save ram

            #region Input Options
            //Controls enabling and disabling of the different input types
            if (tempControllerEnabled)
                tempText = new string[]{ "[ Xbox 360 Controller ]", "  Mouse and Keyboard  "};
            else
                tempText = new string[]{ "  Xbox 360 Controller ", "[ Mouse and Keyboard ]"};

            temp = Drawing.myFont.MeasureString(tempText[0]);
            if (new Rectangle((int)controllerTextPos[1].X, (int)controllerTextPos[1].Y, (int)temp.X, (int)temp.Y).Contains(Updater.mousePos) && (Updater.mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
                tempControllerEnabled = true;

            temp = Drawing.myFont.MeasureString(tempText[1]);
            if (new Rectangle((int)controllerTextPos[2].X, (int)controllerTextPos[2].Y, (int)temp.X, (int)temp.Y).Contains(Updater.mousePos) && (Updater.mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
                tempControllerEnabled = false;

            #endregion

            #region Rainbow Ball Option
            //allows for enabling of a "rainbow" effect displayed by the Pong ball
            if (tempRainbowBallEnabled)
                tempText = new string[] { "[ Enabled ]", "  Disabled  " };
            else
                tempText = new string[] { "  Enabled  ", "[ Disabled ]" };

            temp = Drawing.myFont.MeasureString(tempText[0]);
            if (new Rectangle((int)ballTextPos[1].X, (int)ballTextPos[1].Y, (int)temp.X, (int)temp.Y).Contains(Updater.mousePos) && (Updater.mouse.LeftButton == ButtonState.Pressed))
                tempRainbowBallEnabled = true;

            temp = Drawing.myFont.MeasureString(tempText[1]);
            if (new Rectangle((int)ballTextPos[2].X, (int)ballTextPos[2].Y, (int)temp.X, (int)temp.Y).Contains(Updater.mousePos) && (Updater.mouse.LeftButton == ButtonState.Pressed))
                tempRainbowBallEnabled = false;

            #endregion

            #region Pong Paddle Color Controls



            #endregion

            #region save, cancel and reset buttons

            if (saveRec.Contains(Updater.mousePos))
            {
                Colors.optionsScreenColors[0] = Color.Red;
                if (Updater.mouse.LeftButton == ButtonState.Pressed)
                {
                    SaveCurrentOptions();
                    Updater.SwitchScreen(Updater.oldRendering);
                }
            }
            else
                Colors.optionsScreenColors[0] = Color.White;

            if (cancelRec.Contains(Updater.mousePos))
            {
                Colors.optionsScreenColors[1] = Color.Red;
                if (Updater.mouse.LeftButton == ButtonState.Pressed)
                {
                    ResetTempVariables();
                    Updater.SwitchScreen(Updater.oldRendering);
                }
            }
            else
                Colors.optionsScreenColors[1] = Color.White;

            if (resetRec.Contains(Updater.mousePos))
            {
                Colors.optionsScreenColors[2] = Color.Red;
                if (Updater.mouse.LeftButton == ButtonState.Pressed)
                    ResetOptions();
            }
            else
                Colors.optionsScreenColors[2] = Color.White;

            #endregion
        }

        public static void Draw(SpriteBatch spriteBatch, float transparency)
        {
            //spriteBatch.DrawString(Drawing.myFont, "Name", new Vector2(20, 20), Color.White * transparency);

            #region Input Controls
            //Draws the controls to change input
            spriteBatch.DrawString(Drawing.myFont, "Input", controllerTextPos[0], Color.White * transparency);
            if (tempControllerEnabled)
            {
                spriteBatch.DrawString(Drawing.myFont, "[ Xbox 360 Controller ]", controllerTextPos[1], Color.Red * transparency);
                spriteBatch.DrawString(Drawing.myFont, "  Mouse and Keyboard  ", controllerTextPos[2], Color.White * transparency);
            }
            else
            {
                spriteBatch.DrawString(Drawing.myFont, "  Xbox 360 Controller ", controllerTextPos[1], Color.White * transparency);
                spriteBatch.DrawString(Drawing.myFont, "[ Mouse and Keyboard ]", controllerTextPos[2], Color.Red * transparency);
            }

            #endregion

            #region Rainbow Ball Controls
            //Draws the controls to enable or disable the "Rainbow ball" effect
            spriteBatch.DrawString(Drawing.myFont, "Rainbow Pong Ball", ballTextPos[0], Color.White * transparency);
            if (tempRainbowBallEnabled)
            {
                spriteBatch.DrawString(Drawing.myFont, "[ Enabled ]", ballTextPos[1], Color.Red * transparency);
                spriteBatch.DrawString(Drawing.myFont, "  Disabled  ", ballTextPos[2], Color.White * transparency);
            }
            else
            {
                spriteBatch.DrawString(Drawing.myFont, "  Enabled  ", ballTextPos[1], Color.White * transparency);
                spriteBatch.DrawString(Drawing.myFont, "[ Disabled ]", ballTextPos[2], Color.Red * transparency);
            }

            #endregion

            #region Pong Paddle Color Settings
            //Draws the controls for changing the colors of the Pong Paddle
            spriteBatch.DrawString(Drawing.myFont, "Pong paddle color", new Vector2(GameBase.monitorWidth/2, GameBase.monitorHeight/8*3), Color.White * transparency);
            spriteBatch.DrawString(Drawing.myFont, "RED:", new Vector2(GameBase.monitorWidth/16*9, GameBase.monitorHeight/16 * 7), Color.Red * transparency);
            spriteBatch.DrawString(Drawing.myFont, "GREEN:", new Vector2(GameBase.monitorWidth / 16 * 9, GameBase.monitorHeight / 16 * 8), Color.Green * transparency);
            spriteBatch.DrawString(Drawing.myFont, "BLUE:", new Vector2(GameBase.monitorWidth / 16 * 9, GameBase.monitorHeight / 16 * 9), Color.Blue * transparency);

            spriteBatch.DrawString(Drawing.myFont, tempPlayerColor.R.ToString(), new Vector2((GameBase.monitorWidth / 16 * 10) + (GameBase.monitorWidth / 32), GameBase.monitorHeight / 16 * 7), Color.White * transparency);
            spriteBatch.DrawString(Drawing.myFont, tempPlayerColor.G.ToString(), new Vector2((GameBase.monitorWidth / 16 * 10) + (GameBase.monitorWidth / 32), GameBase.monitorHeight / 16 * 8), Color.White * transparency);
            spriteBatch.DrawString(Drawing.myFont, tempPlayerColor.B.ToString(), new Vector2((GameBase.monitorWidth / 16 * 10) + (GameBase.monitorWidth / 32), GameBase.monitorHeight / 16 * 9), Color.White * transparency);

            spriteBatch.DrawString(Drawing.myFont, "+", new Vector2(redControlsRec[0].X, redControlsRec[0].Y), Color.White * transparency, 0.0f, new Vector2(0, 0), fontSizeIncrease, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(Drawing.myFont, "-", new Vector2(redControlsRec[1].X, redControlsRec[1].Y), Color.White * transparency, 0.0f, new Vector2(0, 0), fontSizeIncrease, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(Drawing.myFont, "+", new Vector2(greenControlsRec[0].X, greenControlsRec[0].Y), Color.White * transparency, 0.0f, new Vector2(0, 0), fontSizeIncrease, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(Drawing.myFont, "-", new Vector2(greenControlsRec[1].X, greenControlsRec[1].Y), Color.White * transparency, 0.0f, new Vector2(0, 0), fontSizeIncrease, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(Drawing.myFont, "+", new Vector2(blueControlsRec[0].X, blueControlsRec[0].Y), Color.White * transparency, 0.0f, new Vector2(0, 0), fontSizeIncrease, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(Drawing.myFont, "-", new Vector2(blueControlsRec[1].X, blueControlsRec[1].Y), Color.White * transparency, 0.0f, new Vector2(0, 0), fontSizeIncrease, SpriteEffects.None, 1.0f);

            spriteBatch.Draw(GameBase.playerBar, new Rectangle(GameBase.monitorWidth / 16 * 12, GameBase.monitorHeight / 16 * 7, 26, GameBase.monitorHeight / 5), tempPlayerColor * transparency);            
            #endregion

            #region Save, Cancel and Reset choices
            //Draws the Save, Cancel and Reset buttons
            spriteBatch.DrawString(Drawing.myFont, "Save Options", new Vector2(saveRec.X, saveRec.Y), Colors.optionsScreenColors[0] * transparency);
            spriteBatch.DrawString(Drawing.myFont, "Cancel", new Vector2(cancelRec.X, cancelRec.Y), Colors.optionsScreenColors[1] * transparency);
            spriteBatch.DrawString(Drawing.myFont, "Reset Options", new Vector2(resetRec.X, resetRec.Y), Colors.optionsScreenColors[2] * transparency);

            #endregion
        }

        private static void CreateOrLoadOptions()
        {
            if (!File.Exists(optionsFilePath))
            {
                ResetOptions();
                WriteToOptionsFile(false);
            }
            else
            {
                try
                {
                    sr = new StreamReader(optionsFilePath);
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if ((line.IndexOf("[") == -1))
                            continue;
                        string type;
                        string value;
                        int index;

                        index = line.IndexOf("=") + 2;
                        line = line.Substring(index);
                        index = line.IndexOf("\"");
                        type = line.Substring(0, index);

                        index = line.IndexOf("=") + 2;
                        line = line.Substring(index);
                        index = line.IndexOf("\"");
                        value = line.Substring(0, index);

                        switch (type)
                        {
                            case "playerName":
                                playerName = value;
                                break;
                            case "playerColor":
                                string temp;
                                int i;
                                int sizeTesting;
                                byte[] colorParts = new byte[3];   //red, green, blue (parts of Color)
                                for (int j = 0; j < 3; j++)
                                {
                                    i = value.IndexOf(':');
                                    temp = value.Substring(0, i);
                                    if (Int32.TryParse(temp, out sizeTesting))  //Due to the "out", it sets the value of sizeTesting
                                    {
                                        if (sizeTesting > 255)
                                            colorParts[j] = 255;
                                        else if (sizeTesting < 0)
                                            colorParts[j] = 0;
                                        else
                                            colorParts[j] = (byte)sizeTesting;
                                    }
                                    else
                                        colorParts[j] = 255;
                                    value = value.Substring(i + 1);
                                }
                                playerColor = new Color(colorParts[0], colorParts[1], colorParts[2]);
                                break;
                            case "controllerEnabled":
                                if (!Boolean.TryParse(value, out controllerEnabled)) //Due to the "out", it sets the value of controllerEnabled
                                    throw new Exception("Error loading options: could not parse controllerEnabled value");
                                break;
                            case "rainbowBallEnabled":
                                if (!Boolean.TryParse(value, out rainbowBallEnabled))      //Due to the "out", it sets the value of rainbowBall
                                    throw new Exception("Error loading options: could not parse rainbowBall value");
                                break;
                            default:
                                //TODO:  write to log
                                break;
                        }
                    }
                    sr.Close();
                }
                catch (Exception e)
                {
                    sr.Close();
                    File.Delete(optionsFilePath);
                    //report to log (log.write(e.ToString);)
                    CreateOrLoadOptions();
                }  
            }
        }

        private static void WriteToOptionsFile(bool exists)
        {
            if(exists)
                File.Delete(optionsFilePath);
            FileStream fs = new FileStream(optionsFilePath, FileMode.CreateNew);
            fs.Close();

            sw = new StreamWriter(optionsFilePath, true);
            sw.WriteLine("PLEASE DO NOT MANUALLY EDIT THIS FILE");
            sw.WriteLine("DOING SO COULD CAUSE DELETION OF SETTINGS");
            sw.WriteLine("----------------------------------------");
            sw.WriteLine("[type=\"playerName\";value=\""+playerName+"\";]");
            sw.WriteLine("[type=\"playerColor\";value=\""+playerColor.R+":"+playerColor.G+":"+playerColor.B+":\";]");
            sw.WriteLine("[type=\"controllerEnabled\";value=\""+controllerEnabled+"\";]");
            sw.WriteLine("[type=\"rainbowBallEnabled\";value=\""+rainbowBallEnabled+"\";]");
            sw.Close();
        }

        private static void ResetOptions()
        {
            playerName = "Player One";
            playerColor = Color.White;
            controllerEnabled = false;
            rainbowBallEnabled = false;
            ResetTempVariables();
        }

        private static void ResetTempVariables()
        {
            tempPlayerName = playerName;
            tempPlayerColor = playerColor;
            tempControllerEnabled = controllerEnabled;
            tempRainbowBallEnabled = rainbowBallEnabled;
        }

        private static void SaveCurrentOptions()
        {
            playerName = tempPlayerName;
            playerColor = tempPlayerColor;
            controllerEnabled = tempControllerEnabled;
            rainbowBallEnabled = tempRainbowBallEnabled;
            WriteToOptionsFile(true);
        }
    }
}
