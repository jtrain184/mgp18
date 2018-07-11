
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018.Menu_Classes
{
    public class S_DifficultyMenu : State
    {
        public enum Buttons
        {
            EASY = 0,
            MEDIUM,
            HARD,
            BACK
        }

        public List<mainMenuItem> items;

        int currentMenuItem;
        int numItems;

        // Constructor for Main Menu:
        public S_DifficultyMenu(GameStateManager creator, EntityCounter ec, float xPos, float yPos) : base(creator, ec, xPos, yPos)
        {
            currentMenuItem = (int)Buttons.EASY;

            items = new List<mainMenuItem>();

           

            // Difficulty: Easy
            items.Add(new mainMenuItem(this.xPos + 300, this.yPos + 200, "EASY", (int)Buttons.EASY));
            numItems++;

            // Difficulty: Medium
            items.Add(new mainMenuItem(this.xPos + 1000, this.yPos + 200, "Medium", (int)Buttons.MEDIUM));
            numItems++;

            // Difficulty: Hard
            items.Add(new mainMenuItem(this.xPos + 300, this.yPos + 500, "Hard", (int)Buttons.HARD));
            numItems++;

            // Back Button
            items.Add(new mainMenuItem(this.xPos + 1000, this.yPos + 500, "Back", (int)Buttons.BACK));
            numItems++;

            // Menu Description
            items.Add(new mainMenuItem(this.xPos + 650, this.yPos + 650, "Choose the difficulty:", -1));
            

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




                    // Back: Goes back to player count:
                    if (currentMenuItem == (int)Buttons.BACK)
                    {
                        S_PlayerCountMenu playerCountMenu = new S_PlayerCountMenu(parentManager, parentManager.eCounter, 0, 0);
                        parentManager.AddStateQueue(playerCountMenu);
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
           
            for (int i = 0; i < 4; i++)
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
            Vector2 menuItemPos = new Vector2(items[4].xPos, items[4].yPos);
            Vector2 menuTextPos = CenterString.getCenterStringVector(menuItemPos, items[4].text, this.parentManager.game.ft_mainMenuFont);
            sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)items[4].xPos - 550 / 2, (int)items[4].yPos - 110 / 2, 550, 110), Color.White);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, items[4].text, menuTextPos, Color.Black);

            // End drawing:
            sb.End();
        }
    }
}
