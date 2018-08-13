﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{

    public class S_MainMenu : State {


        public List<MenuItem> items;

        public int currentMenuItem;
        public int numItems;
        public const int cloudWidth = 300;
        public const int cloudHeight = 80;

        public Vector2 glovePos;
        public bool moveGlove = false;

        public bool showControls = false;

        bool loadedSong = false;

        // Constructor for Main Menu:
        public S_MainMenu(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            this.currentMenuItem = (int)MenuItem.MainMenu.PIRATE;

            items = new List<MenuItem>
            {
                new MenuItem(MGP_Constants.SCREEN_MID_X, 200, "Pirate Bay", (int)MenuItem.MainMenu.PIRATE),
                new MenuItem(MGP_Constants.SCREEN_MID_X, 300, "Controls", (int)MenuItem.MainMenu.MOUNTAIN),
                new MenuItem(MGP_Constants.SCREEN_MID_X, 400, "About", (int)MenuItem.MainMenu.ABOUT),
                new MenuItem(MGP_Constants.SCREEN_MID_X, 500, "Exit", (int)MenuItem.MainMenu.EXIT)
            };
            numItems = items.Count;

            // Map buttons to each other
            for (int i = 0; i < numItems; i++)
            {
                try { items[i].above = items[i - 1]; } catch (Exception) { items[i].above = items[i]; }
                try { items[i].below = items[i + 1]; } catch (Exception) { items[i].below = items[i]; }
            }

            glovePos = new Vector2(items[0].xPos - (cloudWidth / 2 + 60), items[0].yPos - 35);

        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks) {
            base.Update(gameTime, ks);

            if (!loadedSong) {
              parentManager.audioEngine.addSongQueue(MGP_Constants.music.mainMenu);
              Console.WriteLine("Added mainMenu to music queue");
              loadedSong = true;
            }


            // Move Menu Selection Up:
            if (km.ActionPressed(KeyboardManager.action.up, KeyboardManager.playerIndex.all)) {
                currentMenuItem = items[currentMenuItem].above.activeValue;
                moveGlove = true;

            }

            // Move Menu Selection Down:
            if (km.ActionPressed(KeyboardManager.action.down, KeyboardManager.playerIndex.all)) {
                currentMenuItem = items[currentMenuItem].below.activeValue;
                moveGlove = true;
            }

            // Move glove
            if (moveGlove)
            {
                if (Vector2.Distance(glovePos, new Vector2(items[currentMenuItem].xPos - 60, items[currentMenuItem].yPos - 35)) < 1.0f)
                {
                    moveGlove = false;
                }
                else
                {
                    glovePos.Y = MGP_Tools.Ease(glovePos.Y, items[currentMenuItem].yPos - 35, 0.5f);
                }
            }

            //DEBUG:  GO TO MINIGAME2
            if (km.KeyPressed(Keys.D9))
            {
                // DEBUG: Go straight to the mini game
                // Create player entitities and add to game options

                 // add as a human player
                 parentManager.gameOptions.players.Add(new Player(parentManager, Player.Type.FRANK, true, KeyboardManager.playerIndex.one));
                 // add a comp player
                 parentManager.gameOptions.players.Add(new Player(parentManager, Player.Type.MANFORD, true, KeyboardManager.playerIndex.two));
                 parentManager.gameOptions.players.Add(new Player(parentManager, Player.Type.SUE, false, KeyboardManager.playerIndex.none));
                 parentManager.gameOptions.players.Add(new Player(parentManager, Player.Type.WILBER, false, KeyboardManager.playerIndex.none));


                parentManager.gameOptions.numPlayers = 2;

                S_Minigame2 minigame = new S_Minigame2(parentManager, 0, 0, true);
                parentManager.AddStateQueue(minigame);

                parentManager.gameOptions.difficulty = MenuItem.Difficulty.HARD;

                this.flagForDeletion = true;

            }

            // DEBUG: Go to minigame instructions (LAST PARAMETER: 0 == minigame1, 1 == minigame2)
            if (km.KeyPressed(Keys.D5))
            {
                parentManager.gameOptions.numPlayers = 2;
                S_MinigameInstructions minigame = new S_MinigameInstructions(parentManager, 0, 0, 1);
                parentManager.AddStateQueue(minigame);
                this.flagForDeletion = true;

            }

            // Press ENTER while some menu item is highlighted:
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.all)) {

                // Map: Pirate Bay
                if (currentMenuItem == (int)MenuItem.MainMenu.PIRATE) {
                    parentManager.gameOptions.mapName = MenuItem.MainMenu.MOUNTAIN;
                    S_PlayerCountMenu playerCountMenu = new S_PlayerCountMenu(parentManager, 0, 0);
                    parentManager.AddStateQueue(playerCountMenu);
                    this.flagForDeletion = true;
                }

                // Option: Controls
                if (currentMenuItem == (int)MenuItem.MainMenu.MOUNTAIN) {
                    S_Controls controls = new S_Controls(parentManager, 0, 0);
                    parentManager.AddStateQueue(controls);
                    this.active = false;
                }



                // Option: About
                if(currentMenuItem == (int)MenuItem.MainMenu.ABOUT) {
                    S_About about = new S_About(parentManager, 0, 0);
                    parentManager.AddStateQueue(about);
                    this.flagForDeletion = true;
                }

                // Option: Exit
                if (currentMenuItem == (int)MenuItem.MainMenu.EXIT) {
                    parentManager.game.Exit();
                }


            } // end pressed 'select' button


        } // end update function

        // Draw:
        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            SpriteBatch sb = this.parentManager.game.spriteBatch;
            sb.Begin();

            // Draw Background:
            sb.Draw(this.parentManager.game.bg_titleScreen, new Vector2(xPos, yPos), Color.White);

            // Draw Buttons
            Color tColor;
            int i = 0;
            foreach (MenuItem item in items)
            {
                    Vector2 pos = new Vector2(item.xPos, item.yPos);
                    Rectangle cloudPos = new Rectangle((int)item.xPos - cloudWidth / 2, (int)item.yPos - cloudHeight / 2, cloudWidth, cloudHeight);
                    Vector2 textPos = CenterString.getCenterStringVector(pos, item.text, this.parentManager.game.ft_mainMenuFont);

                    // Cloud Background:
                    sb.Draw(this.parentManager.game.spr_cloudIcon, cloudPos, Color.White);

                    // Draw Text:
                    if (i == currentMenuItem)
                        tColor = Color.Blue;
                    else
                        tColor = Color.Red;
                    sb.DrawString(this.parentManager.game.ft_mainMenuFont, item.text, textPos, tColor);

                    i++;
            }

            // Draw current selection hand
            sb.Draw(parentManager.game.spr_glove, glovePos, Color.White);

            if (this.active)
            {
                string text = "Directional Buttons...Move selection\nSelect...Confirm selection";

                Vector2 smTextPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, 675), text, parentManager.game.ft_rollDice_lg);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X - 2, smTextPos.Y), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X + 2, smTextPos.Y), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y - 2), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y + 2), Color.Black);

                sb.DrawString(parentManager.game.ft_rollDice_lg, text, smTextPos, Color.White);
            }



            // End drawing:
            sb.End();
        }




    } // end class definition
}
