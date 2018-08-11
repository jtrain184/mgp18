
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

        int currentMenuItem;
        int numItems;

        // Constructor for Main Menu:
        public S_MainMenu(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            this.currentMenuItem = (int)MenuItem.MainMenu.PIRATE;

            items = new List<MenuItem>();


            // Game: Pirate Bay
            items.Add(new MenuItem(this.xPos + 300, this.yPos + 200, "Pirate Bay", (int)MenuItem.MainMenu.PIRATE));
            numItems++;

            // Game: Lonely Mountain
            items.Add(new MenuItem(this.xPos + 1000, this.yPos + 200, "Lonely" + System.Environment.NewLine + "Mountain", (int)MenuItem.MainMenu.MOUNTAIN));
            numItems++;

            // About
            items.Add(new MenuItem(this.xPos + 300, this.yPos + 500, "About", (int)MenuItem.MainMenu.ABOUT));
            numItems++;

            // Exit
            items.Add(new MenuItem(this.xPos + 1000, this.yPos + 500, "Exit", (int)MenuItem.MainMenu.EXIT));
            numItems++;

            // Menu Description
            items.Add(new MenuItem(this.xPos + 650, this.yPos + 650,
                "Use [W-A-S-D Keys] to navigate the menu" + System.Environment.NewLine +
                "Press [Enter] to confirm your selection", -1));

            // Map buttons to each other
            foreach (MenuItem item in items) {
                //set above and below values
                if (item.activeValue > 1)
                {
                    item.above = items.Find(x => x.activeValue == item.activeValue - 2);
                    item.below = item;  // Item is already at the bottom
                }

                else
                {
                    item.above = item;  // Item is already at the top
                    item.below = items.Find(x => x.activeValue == item.activeValue + 2);
                }


                // set left and right values
                if (item.activeValue % 2 == 0)
                {
                    item.left = item;   // Item is already on the left
                    item.right = items.Find(x => x.activeValue == item.activeValue + 1);
                }
                else
                {
                    item.left = items.Find(x => x.activeValue == item.activeValue - 1);
                    item.right = item;  // Item is already on the right
                }



            }



        }


        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks) {
            base.Update(gameTime, ks);

            // Move Menu Selection Up:
            if (km.ActionPressed(KeyboardManager.action.up, KeyboardManager.playerIndex.one)) {
                currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).above.activeValue;

            }

            // Move Menu Selection Down:
            if (km.ActionPressed(KeyboardManager.action.down, KeyboardManager.playerIndex.one)) {
                currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).below.activeValue;
            }

            // Move Menu Selection Left:
            if (km.ActionPressed(KeyboardManager.action.left, KeyboardManager.playerIndex.one)) {
                currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).left.activeValue;
            }

            // Move Menu Selection Right:
            if (km.ActionPressed(KeyboardManager.action.right, KeyboardManager.playerIndex.one)) {
                currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).right.activeValue;
            }

            //DEBUG:  GO TO MINIGAME2
            if (km.KeyPressed(Keys.D9))
            {
                // DEBUG: Go straight to the mini game
                // Create player entitities and add to game options
         
                 // add as a human player
                 parentManager.gameOptions.players.Add(new Player(parentManager, Player.Type.FRANK, true));
                 // add a comp player
                 parentManager.gameOptions.players.Add(new Player(parentManager, Player.Type.MANFORD, true));
                 parentManager.gameOptions.players.Add(new Player(parentManager, Player.Type.SUE, false));
                 parentManager.gameOptions.players.Add(new Player(parentManager, Player.Type.WILBER, false));


                parentManager.gameOptions.numPlayers = 2;

                S_Minigame2 minigame = new S_Minigame2(parentManager, 0, 0, true);
                parentManager.AddStateQueue(minigame);

                parentManager.gameOptions.difficulty = MenuItem.Difficulty.HARD;

                this.flagForDeletion = true;

            }

            // Press ENTER while some menu item is highlighted:
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one)) {

                // Map: Pirate Bay
                if (currentMenuItem == (int)MenuItem.MainMenu.PIRATE) {
                    parentManager.gameOptions.mapName = MenuItem.MainMenu.MOUNTAIN;
                    S_PlayerCountMenu playerCountMenu = new S_PlayerCountMenu(parentManager, 0, 0);
                    parentManager.AddStateQueue(playerCountMenu);
                    this.flagForDeletion = true;
                }

                // Map: Lonely Moutain
                if (currentMenuItem == (int)MenuItem.MainMenu.MOUNTAIN) {
                    // DEBUG: Go straight to the mini game
                    // Create player entitities and add to game options
                    for (int i = 0; i < 4; i++)
                    {
                        // add as a human player
                        if (i < 1)
                            parentManager.gameOptions.players.Add(new Player(parentManager, Player.Type.FRANK, true));
                        // add a comp player
                        else
                            parentManager.gameOptions.players.Add(new Player(parentManager, Player.Type.LOUIE, false));

                    }
                    parentManager.gameOptions.numPlayers = 1;

                    parentManager.gameOptions.difficulty = MenuItem.Difficulty.EASY;

                    S_Minigame1 minigame1 = new S_Minigame1(parentManager, 0, 0, true);
                    parentManager.AddStateQueue(minigame1);
                    this.flagForDeletion = true;


                } // end Lonely Mountain choice



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

            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;
            sb.Begin();

            sb.Draw(this.parentManager.game.bg_titleScreen, new Vector2(xPos, yPos), Color.White);

            // Draw Buttons -----------------------

            // Hate hard coding...but just do it...
            int SPRITE_WIDTH = 320;
            int SPRITE_HEIGHT = 160;

            Color tColor;
            int i = 0;
            foreach (MenuItem item in items)
            {
                if (item.activeValue != -1)
                {
                    Vector2 pos = new Vector2(item.xPos, item.yPos);
                    Vector2 cloudPos = new Vector2(item.xPos - SPRITE_WIDTH / 2, item.yPos - SPRITE_HEIGHT / 2);
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
            }

            // Draw the Menu description cloud wider
            Vector2 menuItemPos = new Vector2(items[numItems].xPos, items[numItems].yPos);
            Vector2 menuTextPos = CenterString.getCenterStringVector(menuItemPos, items[numItems].text, this.parentManager.game.ft_menuDescriptionFont);
            sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)items[numItems].xPos - 600 / 2, (int)items[numItems].yPos - 140 / 2, 600, 140), Color.White);
            sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, items[numItems].text, menuTextPos, Color.Black);

            // End drawing:
            sb.End();
        }




    } // end class definition
}
