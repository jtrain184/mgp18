
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_BonusMenu : State
    {
        public List<MenuItem> items;

        int currentMenuItem;
        int numItems;

        // Constructor for Bonus Menu:
        public S_BonusMenu(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currentMenuItem = 0;

            items = new List<MenuItem>();


            // Option: Allow Bonuses
            items.Add(new MenuItem(this.xPos + 300, this.yPos + 200, "Allow Bonuses", 0));
            numItems++;

            // Option: No bonuses
            items.Add(new MenuItem(this.xPos + 1000, this.yPos + 200, "No Bonuses", 1));
            numItems++;

            // Menu Description
            items.Add(new MenuItem(this.xPos + 650, this.yPos + 650,
                "Bonuses will be annouced at the end of the game." + System.Environment.NewLine +
                "Use [W-A-S-D Keys] to navigate selections" + System.Environment.NewLine +
                "Press [Enter] to confirm selection" + System.Environment.NewLine +
                "Press [Backspace] to return to the previous menu", -1));
        }


        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // Move Menu Selection Left:
            if (km.ActionPressed(KeyboardManager.action.left, KeyboardManager.playerIndex.one))
            {
                if (currentMenuItem == 1) { currentMenuItem = 0; }
            }

            // Move Menu Selection Right:
            if (km.ActionPressed(KeyboardManager.action.right, KeyboardManager.playerIndex.one))
            {
                if (currentMenuItem == 0) { currentMenuItem = 1; }
            }


            // Press ENTER while some menu item is highlighted:
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one))
            {
                // Allow Bonuses
                if (currentMenuItem == 0)
                {
                    parentManager.gameOptions.allowBonus = true;

                }

                // No Bonuses
                if (currentMenuItem == 1)
                {
                    parentManager.gameOptions.allowBonus = false;
                }

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

                S_Board board = new B_PirateBay(parentManager, 0, 0);       // add code to create correct board
                parentManager.AddStateQueue(board);

                // Add UI to game:
                board.playerUI = new S_PlayerUI(parentManager, 0, 0);
                parentManager.AddStateQueue(board.playerUI);



                this.flagForDeletion = true;


            }
            // Press Cancel Key: Goes back to main menu:
            if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.one))
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
    }
}
