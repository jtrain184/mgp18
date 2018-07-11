
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_PlayerCountMenu : State
    {
        public enum Buttons
        {
            ONE = 0,
            TWO,
        }

        public List<mainMenuItem> items;

        int currentMenuItem;
        int numItems;

        // Constructor for Player Count Menu:
        public S_PlayerCountMenu(GameStateManager creator,float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currentMenuItem = (int)Buttons.ONE;

            items = new List<mainMenuItem>();


            // Player Count: One
            items.Add(new mainMenuItem(this.xPos + 300, this.yPos + 200, "1 Player and" + System.Environment.NewLine + "3 Computer Characters", (int)Buttons.ONE));
            numItems++;

            // Player Count: Two
            items.Add(new mainMenuItem(this.xPos + 1000, this.yPos + 200, "2 Players and" + System.Environment.NewLine + "2 Computer Characters", (int)Buttons.TWO));
            numItems++;

            // Menu Description
            items.Add(new mainMenuItem(this.xPos + 650, this.yPos + 650,
                "Use the arrow keys to select the players for the game" + System.Environment.NewLine +
                "Confirm your selection by pressing Enter" + System.Environment.NewLine +
                "Press Back to return to the previous menu", -1));
        }


        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // If this is the top layer, allow moving active menu:
            if (this.isTopLayer)
            {


                // Move Menu Selection Left:
                if (km.ActionPressed(KeyboardManager.action.left, KeyboardManager.playerIndex.one)) {
                    if (currentMenuItem == 1) { currentMenuItem = 0; }
                }

                // Move Menu Selection Right:
                if (km.ActionPressed(KeyboardManager.action.right, KeyboardManager.playerIndex.one)) {
                    if (currentMenuItem == 0) { currentMenuItem = 1; }

                }


                // Press ENTER while some menu item is highlighted:
                if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one)) {
                    // One Player
                    if(currentMenuItem == (int)Buttons.ONE)
                    {
                        parentManager.gameOptions.numPlayers = 1;
                        S_DifficultyMenu diffMenu = new S_DifficultyMenu(parentManager, 0, 0);
                        parentManager.AddStateQueue(diffMenu);
                        this.flagForDeletion = true;
                    }


                    //Two Players
                    if (currentMenuItem == (int)Buttons.TWO)
                    {
                        parentManager.gameOptions.numPlayers = 2;
                        S_DifficultyMenu diffMenu = new S_DifficultyMenu(parentManager, 0, 0);
                        parentManager.AddStateQueue(diffMenu);
                        this.flagForDeletion = true;
                    }





                }
                // Press Cancel Key: Goes back to main menu:
                if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.one)) {
                    S_MainMenu mainMenu = new S_MainMenu(parentManager, 0, 0);
                    parentManager.AddStateQueue(mainMenu);
                    this.flagForDeletion = true;
                }
            }


        }

        // Draw:
        public override void Draw(GameTime gameTime)
        {
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
           for(int i = 0; i < numItems; i++)
            {


                Vector2 pos = new Vector2(items[i].xPos, items[i].yPos);
                Vector2 cloudPos = new Vector2(items[i].xPos - SPRITE_WIDTH / 2, items[i].yPos - SPRITE_HEIGHT / 2);
                Vector2 textPos = CenterString.getCenterStringVector(pos, items[i].text, this.parentManager.game.ft_mainMenuFont);

                // Cloud Background:
                sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)items[i].xPos - 550 / 2, (int)items[i].yPos - SPRITE_HEIGHT / 2, 550, 160), Color.White);

                // Draw Text:
                if (i == currentMenuItem)
                    tColor = Color.Blue;
                else
                    tColor = Color.Red;
                sb.DrawString(this.parentManager.game.ft_mainMenuFont, items[i].text, textPos, tColor);


            }

            // Draw the Menu description cloud wider
            Vector2 menuItemPos = new Vector2(items[numItems].xPos, items[numItems].yPos);
            Vector2 menuTextPos = CenterString.getCenterStringVector(menuItemPos, items[numItems].text, this.parentManager.game.ft_menuDescriptionFont);
            sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)items[numItems].xPos - 600 / 2, (int)items[numItems].yPos - 140 / 2, 600, 140), Color.White);
            sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, items[numItems].text, menuTextPos, Color.Black);


            // End drawing:
            sb.End();
        }

    }//end of class
}
