
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
        public const int cloudHeight = 80;
        public const int cloudWidth = 200;
        public const string description = "What will the difficulty of\nthe computer players be?";

        public int currentMenuItem;
        public int numItems;
        public Vector2 glovePos;
        public bool moveGlove = false;

        // Constructor for Main Menu:
        public S_DifficultyMenu(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currentMenuItem = (int)MenuItem.Difficulty.EASY;

            items = new List<MenuItem>() {
            new MenuItem(MGP_Constants.SCREEN_MID_X, 200, "EASY", (int)MenuItem.Difficulty.EASY),
            new MenuItem(MGP_Constants.SCREEN_MID_X, 300, "Medium", (int)MenuItem.Difficulty.MEDIUM),
            new MenuItem(MGP_Constants.SCREEN_MID_X, 400, "Hard", (int)MenuItem.Difficulty.HARD)
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
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // Move Menu Selection Up:
            if (km.ActionPressed(KeyboardManager.action.up, KeyboardManager.playerIndex.all))
            {
                currentMenuItem = items[currentMenuItem].above.activeValue;
                moveGlove = true;

            }

            // Move Menu Selection Down:
            if (km.ActionPressed(KeyboardManager.action.down, KeyboardManager.playerIndex.all))
            {
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

            // Press ENTER while some menu item is highlighted:
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.all))
            {
                // Difficulty: Easy
                if (currentMenuItem == (int)MenuItem.Difficulty.EASY)
                    parentManager.gameOptions.difficulty = MenuItem.Difficulty.EASY;

                // Difficulty: Medium
                if (currentMenuItem == (int)MenuItem.Difficulty.MEDIUM)
                    parentManager.gameOptions.difficulty = MenuItem.Difficulty.MEDIUM;
 
                // Difficulty: Medium
                if (currentMenuItem == (int)MenuItem.Difficulty.HARD)
                    parentManager.gameOptions.difficulty = MenuItem.Difficulty.HARD;

                // Go to next menu
                S_NumRoundsMenu numRoundsMenu = new S_NumRoundsMenu(parentManager, 0, 0);
                parentManager.AddStateQueue(numRoundsMenu);
                this.flagForDeletion = true;
            }

            // Press Cancel Key: Goes back to Player Count menu:
            if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.all))
            {
                S_PlayerCountMenu playerCountMenu = new S_PlayerCountMenu(parentManager, 0, 0);
                parentManager.AddStateQueue(playerCountMenu);
                this.flagForDeletion = true;
            }
        }


        // Draw:
        public override void Draw(GameTime gameTime)
        {
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

            // Background Box
            Vector2 backgroundBox = new Vector2(MGP_Constants.SCREEN_MID_X - 450, MGP_Constants.SCREEN_MID_Y + 150);
            sb.Draw(this.parentManager.game.spr_messageBox, new Rectangle((int)backgroundBox.X, (int)backgroundBox.Y, 900, 150), new Color(0, 0, 128, 200));

            // Text
            Vector2 textDesPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X + 450, backgroundBox.Y + 75), description, this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, description, textDesPos, Color.White);

            // Draw current selection hand
            sb.Draw(parentManager.game.spr_glove, glovePos, Color.White);

            // End drawing:
            sb.End();
        }
    }
}
