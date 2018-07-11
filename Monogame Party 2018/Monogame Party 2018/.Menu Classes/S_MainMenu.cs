
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{

    public class S_MainMenu : State {

        public enum Buttons {
            PIRATE = 0,
            MOUNTAIN,
            ABOUT,
            EXIT
        }

        public List<mainMenuItem> items;

        int currentMenuItem;
        int numItems;

        // Constructor for Main Menu:
        public S_MainMenu(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currentMenuItem = (int)Buttons.PIRATE;

            items = new List<mainMenuItem>();


            // Game: Pirate Bay
            items.Add(new mainMenuItem(this.xPos + 300, this.yPos + 200, "Pirate Bay", (int)Buttons.PIRATE));
            numItems++;

            // Game: Lonely Mountain
            items.Add(new mainMenuItem(this.xPos + 1000, this.yPos + 200, "Lonely" + System.Environment.NewLine + "Mountain", (int)Buttons.MOUNTAIN));
            numItems++;

            // About
            items.Add(new mainMenuItem(this.xPos + 300, this.yPos + 500, "About", (int)Buttons.ABOUT));
            numItems++;

            // Exit
            items.Add(new mainMenuItem(this.xPos + 1000, this.yPos + 500, "Exit", (int)Buttons.EXIT));
            numItems++;

            // Map buttons to each other
            foreach(mainMenuItem item in items) {
                //set above and below values
                if (item.activeValue > 1) 
                   item.below = item.above = items.Find(x => x.activeValue == item.activeValue - 2);
                else
                    item.below = item.above = items.Find(x => x.activeValue == item.activeValue + 2);

                // set left and right values
                if (item.activeValue % 2 == 0)
                    item.left = item.right = items.Find(x => x.activeValue == item.activeValue + 1);
                else
                    item.left = item.right = items.Find(x => x.activeValue == item.activeValue - 1);



            }
           



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
                    currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).above.activeValue;
                  
                }

                // Move Menu Selection Down:
                if (parentManager.km.KeyPressed(Keymap.Down))
                {
                    currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).below.activeValue;
                }

                // Move Menu Selection Left:
                if (parentManager.km.KeyPressed(Keymap.Left))
                {
                    currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).left.activeValue;
                }

                // Move Menu Selection Right:
                if (parentManager.km.KeyPressed(Keymap.Right))
                {
                    currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).right.activeValue;
                }


                // Press ENTER while some menu item is highlighted:
                if (parentManager.km.KeyPressed(Keymap.Select)) {

                    // Map: Pirte Bay
                    if (currentMenuItem == (int)Buttons.PIRATE) {
                        parentManager.gameOptions.mapName = "Pirate Bay";
                        S_PlayerCountMenu playerCountMenu = new S_PlayerCountMenu(parentManager, 0, 0);
                        parentManager.AddStateQueue(playerCountMenu);
                        this.flagForDeletion = true;
                    }

                    if (currentMenuItem == (int)Buttons.MOUNTAIN)
                    {
                        parentManager.gameOptions.mapName = "Lonely Mountain";
                        S_PlayerCountMenu playerCountMenu = new S_PlayerCountMenu(parentManager, 0, 0);
                        parentManager.AddStateQueue(playerCountMenu);
                    }

                    // choosing exit actually exits the game:
                    if (currentMenuItem == (int)Buttons.EXIT) {
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
                sb.Draw(this.parentManager.game.spr_cloudIcon, cloudPos, Color.White);

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




    } // end class definition
}
