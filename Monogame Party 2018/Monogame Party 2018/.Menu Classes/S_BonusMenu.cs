
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace Monogame_Party_2018
{
    public class S_BonusMenu : State
    {
        public List<MenuItem> items;
        public const int cloudHeight = 80;
        public const int cloudWidth = 250;
        public const string description = "Finally, please choose a Bonus setting.\nBonuses are announced at the end of the game?";

        public int currentMenuItem;
        public int numItems;
        public Vector2 glovePos;
        public bool moveGlove = false;


        // Constructor for Bonus Menu:
        public S_BonusMenu(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currentMenuItem = 0;

            items = new List<MenuItem>() {
            new MenuItem(MGP_Constants.SCREEN_MID_X, 200, "Bonus", 0),
            new MenuItem(MGP_Constants.SCREEN_MID_X, 300, "No Bonus", 1)
            };

            numItems = items.Count;

            glovePos = new Vector2(items[0].xPos - (cloudWidth / 2 + 60), items[0].yPos - 35);
        }


        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // Move Menu Selection Up:
            if (km.ActionPressed(KeyboardManager.action.up, KeyboardManager.playerIndex.all))
            {
                if (currentMenuItem == 1) { currentMenuItem = 0; }
                moveGlove = true;
            }

            // Move Menu Selection Down:
            if (km.ActionPressed(KeyboardManager.action.down, KeyboardManager.playerIndex.all))
            {
                if (currentMenuItem == 0) { currentMenuItem = 1; }
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
                // Allow Bonuses
                if (currentMenuItem == 0)
                    parentManager.gameOptions.allowBonus = true;

                // No Bonuses
                if (currentMenuItem == 1)
                    parentManager.gameOptions.allowBonus = false;


                // DEBUG: PRINT GAME OPTIONS	
                Console.WriteLine("Map: " + parentManager.gameOptions.mapName +
                    "\nPlayer Count: " + parentManager.gameOptions.numPlayers);
                int x = 1;
                foreach (Player player in parentManager.gameOptions.players)
                {
                    Console.WriteLine("Character " + x + ": " + player.type);
                    x++;
                }
                Console.WriteLine("Difficulty: " + parentManager.gameOptions.difficulty +
                   "\nRound Count: " + parentManager.gameOptions.numRounds +
                   "\nAllow Bonuses: " + parentManager.gameOptions.allowBonus + "\n");





                // Start game based on game options from here.
                S_Board board = new B_PirateBay(parentManager, 0, 0);
                parentManager.AddStateQueue(board);

                // Add UI to game:
                board.playerUI = new S_PlayerUI(parentManager, 0, 0);
                parentManager.AddStateQueue(board.playerUI);

                this.flagForDeletion = true;
            }

            // Press Cancel Key: Goes back one menu:
            if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.all))
            {
                S_NumRoundsMenu numRoundsMenu = new S_NumRoundsMenu(parentManager, 0, 0);
                parentManager.AddStateQueue(numRoundsMenu);
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
            Vector2 backgroundBox = new Vector2(MGP_Constants.SCREEN_MID_X - 550, MGP_Constants.SCREEN_MID_Y + 150);
            sb.Draw(this.parentManager.game.spr_messageBox, new Rectangle((int)backgroundBox.X, (int)backgroundBox.Y, 1100, 150), new Color(0, 0, 128, 200));

            // Text
            Vector2 textDesPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X + 550, backgroundBox.Y + 75), description, this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, description, textDesPos, Color.White);

            // Draw current selection hand
            sb.Draw(parentManager.game.spr_glove, glovePos, Color.White);

            // End drawing:
            sb.End();
        }
    }
}
