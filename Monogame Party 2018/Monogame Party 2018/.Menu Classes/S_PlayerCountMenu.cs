

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogame_Party_2018
{
    public class S_PlayerCountMenu : State
    {


        public List<MenuItem> items;
        public const int cloudHeight = 160;
        public const int cloudWidth = 275;
        public int currentMenuItem;
        public int numItems;
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
                new MenuItem(MGP_Constants.SCREEN_MID_X, 200, "1 Human\n3 COMS", 1),
                new MenuItem(MGP_Constants.SCREEN_MID_X, 400, "2 Humans\n2 COMS", 2)
            };

            numItems = items.Count;

            // Set glove position
            glovePos = new Vector2(items[0].xPos - (cloudWidth / 2 + 60), items[0].yPos - 35);
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
            if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.all))
            {
                S_MainMenu mainMenu = new S_MainMenu(parentManager, 0, 0);
                parentManager.AddStateQueue(mainMenu);
                this.flagForDeletion = true;
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

    }//end of class
}
