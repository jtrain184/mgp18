
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{

    public class S_MainMenu : State
    {

        public enum Buttons
        {
            CASTLE = 0,
            PIRATE,
            ABOUT,
            EXIT
        }

        public List<mainMenuItem> items;

        int currentMenuItem;
        int numItems;

        // Constructor for Main Menu:
        public S_MainMenu(GameStateManager creator, EntityCounter ec, float xPos, float yPos) : base(creator, ec, xPos, yPos)
        {
            currentMenuItem = (int)Buttons.CASTLE;

            items = new List<mainMenuItem>();


            // Game: Castle Land
            items.Add(new mainMenuItem(this.xPos + 100, this.yPos + 200, "Castle Land", (int)Buttons.CASTLE));
            numItems++;

            // Game: Pirate Bay
            items.Add(new mainMenuItem(this.xPos + 500, this.yPos + 300, "Pirate Bay", (int)Buttons.PIRATE));
            numItems++;

            // Settings
            //items.Add(new mainMenuItem(100, 500, "Settings", (int)buttons.SETTINGS));
            //numItems++;

            // About
            items.Add(new mainMenuItem(this.xPos + 900, this.yPos + 200, "About", (int)Buttons.ABOUT));
            numItems++;

            // Exit
            items.Add(new mainMenuItem(this.xPos + 900, this.yPos + 500, "Exit", (int)Buttons.EXIT));
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

                if (parentManager.km.KeyPressed(Keymap.Select))
                {

                    // Go to whatever menu item you chose:
                    if (currentMenuItem == (int)Buttons.CASTLE)
                    {
                        S_MainMenu newMenu = new S_MainMenu(parentManager, parentManager.eCounter, this.xPos + 200, this.yPos + 200);
                        parentManager.AddStateQueue(newMenu);
                    }


                    // choosing exit actually exits the game:
                    if (currentMenuItem == (int)Buttons.EXIT)
                    {
                        parentManager.game.Exit();
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
            Color tColor;
            int i = 0;
            foreach (mainMenuItem item in items)
            {
                Vector2 loc = new Vector2(item.xPos, item.yPos);

                // Cloud Background:
                sb.Draw(this.parentManager.game.spr_cloudIcon, loc, Color.White);

                // Draw Text:
                if (i == currentMenuItem)
                    tColor = Color.Blue;
                else
                    tColor = Color.Red;
                sb.DrawString(this.parentManager.game.ft_mainMenuFont, item.text, loc, tColor);

                i++;
            }


            // End drawing:
            sb.End();
        }




    } // end class definition
}
