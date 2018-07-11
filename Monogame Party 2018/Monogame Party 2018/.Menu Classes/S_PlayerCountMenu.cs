
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018.Menu_Classes
{
    public class S_PlayerCountMenu : State
    {
        public enum Buttons
        {
            ONE = 0,
            TWO,
            BACK
        }

        public List<mainMenuItem> items;

        int currentMenuItem;
        int numItems;

        // Constructor for Player Count Menu:
        public S_PlayerCountMenu(GameStateManager creator, EntityCounter ec, float xPos, float yPos) : base(creator, ec, xPos, yPos)
        {
            currentMenuItem = (int)Buttons.ONE;

            items = new List<mainMenuItem>();


            // Player Count: One
            items.Add(new mainMenuItem(this.xPos + 300, this.yPos + 200, "1 Player and" + System.Environment.NewLine + "3 Computer Characters", (int)Buttons.ONE));
            numItems++;

            // Player Count: Two
            items.Add(new mainMenuItem(this.xPos + 1000, this.yPos + 200, "2 Players and" + System.Environment.NewLine + "2 Computer Characters", (int)Buttons.TWO));
            numItems++;

            // Back Button
            items.Add(new mainMenuItem(this.xPos + 650, this.yPos + 500, "Back", (int)Buttons.BACK));
            numItems++;

        }


        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // If this is the top layer, allow moving active menu:
            if (this.isTopLayer)
            {


                // Move Menu Selection Up:
                if (parentManager.km.KeyPressed(Keymap.Up))
                {
                    if (currentMenuItem == (numItems - 1)) { currentMenuItem = 0; }
                    else { currentMenuItem++; }
                }

                // Move Menu Selection Down:
                if (parentManager.km.KeyPressed(Keymap.Down))
                {
                    if (currentMenuItem == 0) { currentMenuItem = numItems - 1; }
                    else { currentMenuItem--; }
                }


                // Press ENTER while some menu item is highlighted:
                if (parentManager.km.KeyPressed(Keymap.Select))
                {
                    // One Player
                    if(currentMenuItem == (int)Buttons.ONE)
                    {
                        parentManager.gameOptions.numPlayers = 1;
                        S_DifficultyMenu diffMenu = new S_DifficultyMenu(parentManager, parentManager.eCounter, 0, 0);
                        parentManager.AddStateQueue(diffMenu);
                        this.flagForDeletion = true;
                    }


                    //Two Players
                    if (currentMenuItem == (int)Buttons.TWO)
                    {
                        parentManager.gameOptions.numPlayers = 2;
                        S_DifficultyMenu diffMenu = new S_DifficultyMenu(parentManager, parentManager.eCounter, 0, 0);
                        parentManager.AddStateQueue(diffMenu);
                        this.flagForDeletion = true;
                    }


                    // Back: Goes back to main menu:
                    if (currentMenuItem == (int)Buttons.BACK)
                    {
                        S_MainMenu mainMenu = new S_MainMenu(parentManager, parentManager.eCounter, 0, 0);
                        parentManager.AddStateQueue(mainMenu);
                        this.flagForDeletion = true;
                    }


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
            int i = 0;
            foreach (mainMenuItem item in items)
            {

               
                Vector2 pos = new Vector2(item.xPos, item.yPos);
                Vector2 cloudPos = new Vector2(item.xPos - SPRITE_WIDTH / 2, item.yPos - SPRITE_HEIGHT / 2);
                Vector2 textPos = CenterString.getCenterStringVector(pos, item.text, this.parentManager.game.ft_mainMenuFont);

                // Cloud Background:
                sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)item.xPos - 550 / 2, (int)item.yPos - SPRITE_HEIGHT / 2, 550, 160), Color.White);

                // Draw Text:
                if (i == currentMenuItem)
                    tColor = Color.Blue;
                else
                    tColor = Color.Red;
                sb.DrawString(this.parentManager.game.ft_mainMenuFont, item.text, textPos, tColor);

                i++;
            }


            // End drawing:
            sb.End();
        }

    }//end of class
}
