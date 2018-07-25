
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_DifficultyMenu : State
    {

        public List<MenuItem> items;

        int currentMenuItem;
        int numItems;

        // Constructor for Main Menu:
        public S_DifficultyMenu(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currentMenuItem = (int)MenuItem.Difficulty.EASY;

            items = new List<MenuItem>();

            // Difficulty: Easy
            items.Add(new MenuItem(this.xPos + 300, this.yPos + 200, "EASY", (int)MenuItem.Difficulty.EASY));
            numItems++;

            // Difficulty: Medium
            items.Add(new MenuItem(this.xPos + 1000, this.yPos + 200, "Medium", (int)MenuItem.Difficulty.MEDIUM));
            numItems++;

            // Difficulty: Hard
            items.Add(new MenuItem(this.xPos + 650, this.yPos + 500, "Hard", (int)MenuItem.Difficulty.HARD));
            numItems++;

            // Map buttons
            foreach(MenuItem item in items)
            {
                // Set above and below values
                if(item.activeValue < 2)
                {
                    item.above = item;  // Item is already at the top
                    item.below = items[2];  // Set below value to the only button below

                    // Set left and right values
                    if (item.activeValue == 0)
                    {
                        item.left = item;   // Item is already on the left
                        item.right = items[1];  // Set right value to button on the right
                    }
                    else
                    {
                        item.left = items[0];   // Set left value to button on the left
                        item.right = item;  // Item is already on the right
                    }
                }
                else
                {
                    item.above = items[0];  // Set above value to first button at the top
                    item.below = item;  // Item is already at the bottom
                    item.left = item;   // Item has no left buttons
                    item.right = item;  // Item has no right buttons
                }




            }



            // Menu Description
            items.Add(new MenuItem(this.xPos + 650, this.yPos + 650,
                "Use [W-A-S-D Keys] to select the difficulty of the game" + System.Environment.NewLine +
                "Press [Enter] to confirm selection" + System.Environment.NewLine +
                "Press [Backspace] to return to the previous menu", -1));


        }


        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
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


            // Press ENTER while some menu item is highlighted:
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one)) {

                // Difficulty: Easy
                if(currentMenuItem == (int)MenuItem.Difficulty.EASY)
                {
                    parentManager.gameOptions.difficulty = MenuItem.Difficulty.EASY;
                    S_NumRoundsMenu numRoundsMenu= new S_NumRoundsMenu(parentManager, 0, 0);
                    parentManager.AddStateQueue(numRoundsMenu);
                    this.flagForDeletion = true;
                }

                // Difficulty: Medium
                if (currentMenuItem == (int)MenuItem.Difficulty.MEDIUM)
                {
                    parentManager.gameOptions.difficulty = MenuItem.Difficulty.MEDIUM;
                    S_NumRoundsMenu numRoundsMenu = new S_NumRoundsMenu(parentManager, 0, 0);
                    parentManager.AddStateQueue(numRoundsMenu);
                    this.flagForDeletion = true;
                }

                // Difficulty: Medium
                if (currentMenuItem == (int)MenuItem.Difficulty.HARD)
                {
                    parentManager.gameOptions.difficulty = MenuItem.Difficulty.HARD;
                    S_NumRoundsMenu numRoundsMenu = new S_NumRoundsMenu(parentManager, 0, 0);
                    parentManager.AddStateQueue(numRoundsMenu);
                    this.flagForDeletion = true;
                }





            }

            // Press Cancel Key: Goes back to Player Count menu:
            if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.one)) {
                S_PlayerCountMenu playerCountMenu = new S_PlayerCountMenu(parentManager, 0, 0);
                parentManager.AddStateQueue(playerCountMenu);
                this.flagForDeletion = true;
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

            for (int i = 0; i < numItems; i++)
            {


                Vector2 pos = new Vector2(items[i].xPos, items[i].yPos);
                Vector2 cloudPos = new Vector2(items[i].xPos - SPRITE_WIDTH / 2, items[i].yPos - SPRITE_HEIGHT / 2);
                Vector2 textPos = CenterString.getCenterStringVector(pos, items[i].text, this.parentManager.game.ft_mainMenuFont);

                // Cloud Background:
                sb.Draw(this.parentManager.game.spr_cloudIcon, cloudPos, Color.White);

                // Draw Text:
                if (i == currentMenuItem)
                    tColor = Color.Blue;
                else
                    tColor = Color.Red;
                sb.DrawString(this.parentManager.game.ft_mainMenuFont, items[i].text, textPos, tColor);

                //i++;
            }

            // Draw the Menu description cloud wider
            Vector2 menuItemPos = new Vector2(items[numItems].xPos, items[numItems].yPos);
            Vector2 menuTextPos = CenterString.getCenterStringVector(menuItemPos, items[numItems].text, this.parentManager.game.ft_menuDescriptionFont);
            sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)items[numItems].xPos - 600 / 2, (int)items[numItems].yPos - 140 / 2, 600, 140), Color.White);
            sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, items[numItems].text, menuTextPos, Color.Black);

            // End drawing:
            sb.End();
        }
    }
}
