
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


        public List<MenuItem> items;

        int currentMenuItem;
        int numItems;
        public const string description = "First we need to choose some settings.\nHow many people will play this time?";
        public Vector2 glovePos;
        public bool moveGlove = false;

        // Constructor for Player Count Menu:
        public S_PlayerCountMenu(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currentMenuItem = 0;

            items = new List<MenuItem>
            {
                // Player Count: One
                new MenuItem(MGP_Constants.SCREEN_MID_X - 275, 50, "1 Player and" + System.Environment.NewLine + "3 Computer Characters", 1),
                new MenuItem(MGP_Constants.SCREEN_MID_X - 275, 250, "2 Players and" + System.Environment.NewLine + "2 Computer Characters", 2)
            };

            numItems = items.Count;

            // Set glove position
            glovePos = new Vector2(items[0].xPos - 60, items[0].yPos + 40);
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // Move Menu Selection Up:
            if (km.ActionPressed(KeyboardManager.action.up, KeyboardManager.playerIndex.all))
            {
                if (currentMenuItem == 1)
                {
                    currentMenuItem = 0;
                    moveGlove = true;
                }

            }

            // Move Menu Selection Down:
            if (km.ActionPressed(KeyboardManager.action.down, KeyboardManager.playerIndex.all))
            {
                if (currentMenuItem == 0)
                {
                    currentMenuItem = 1;
                    moveGlove = true;
                }
            }

            // Press ENTER while some menu item is highlighted:
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.all))
            {
                // One Player
                if (currentMenuItem == 0)
                {
                    parentManager.gameOptions.numPlayers = 1;
                    S_CharacterMenu characterMenu = new S_CharacterMenu(parentManager, 0, 0);
                    parentManager.AddStateQueue(characterMenu);
                    this.flagForDeletion = true;
                }

                //Two Players
                if (currentMenuItem == 1)
                {
                    parentManager.gameOptions.numPlayers = 2;
                    S_CharacterMenu characterMenu = new S_CharacterMenu(parentManager, 0, 0);
                    parentManager.AddStateQueue(characterMenu);
                    this.flagForDeletion = true;
                }
            }

            // Press Cancel Key: Goes back to main menu:
            if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.one))
            {
                S_MainMenu mainMenu = new S_MainMenu(parentManager, 0, 0);
                parentManager.AddStateQueue(mainMenu);
                this.flagForDeletion = true;
            }

            // Move glove
            if (moveGlove)
            {
                if (Vector2.Distance(glovePos, new Vector2(items[currentMenuItem].xPos - 60, items[currentMenuItem].yPos + 40)) < 1.0f)
                {
                    moveGlove = false;
                }
                else
                {
                    glovePos.Y = MGP_Tools.Ease(glovePos.Y, items[currentMenuItem].yPos + 40, 0.5f);
                }
            }
        } // End update



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
            for (int i = 0; i < numItems; i++)
            {
                // Cloud Background:
                sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)items[i].xPos, (int)items[i].yPos, 550, 160), Color.White);

                // Draw Text:
                if (i == currentMenuItem)
                    tColor = Color.Blue;
                else
                    tColor = Color.Red;
                Vector2 textPos = CenterString.getCenterStringVector(new Vector2(items[i].xPos + 275, items[i].yPos + 80), items[i].text, this.parentManager.game.ft_mainMenuFont);
                sb.DrawString(this.parentManager.game.ft_mainMenuFont, items[i].text, textPos, tColor);
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

    }//end of class
}
