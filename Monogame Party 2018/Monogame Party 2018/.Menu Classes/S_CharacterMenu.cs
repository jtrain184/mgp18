
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_CharacterMenu : State
    {

        public List<MenuItem> items;
        public List<MenuItem.Characters> characters;

        int currentMenuItem;
        int numItems;
        int numOfPlayers;

        // Constructor for Main Menu:
        public S_CharacterMenu(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currentMenuItem = (int)MenuItem.Characters.Princess;
            items = new List<MenuItem>();
            characters = new List<MenuItem.Characters>();
            numOfPlayers = parentManager.gameOptions.numPlayers;


            // Character: Princess Peach
            items.Add(new MenuItem(this.xPos + 300, this.yPos + 200, "Princess" + System.Environment.NewLine + "Peach", (int)MenuItem.Characters.Princess));
            numItems++;

            // Character: Prince of Persia
            items.Add(new MenuItem(this.xPos + 1000, this.yPos + 200, "Prince" + System.Environment.NewLine + "of Persia", (int)MenuItem.Characters.Prince));
            numItems++;

            // Character: Queen of Hearts
            items.Add(new MenuItem(this.xPos + 300, this.yPos + 500, "Queen" + System.Environment.NewLine + "of Hearts", (int)MenuItem.Characters.Queen));
            numItems++;

            // Character: King Kong
            items.Add(new MenuItem(this.xPos + 1000, this.yPos + 500, "King" + System.Environment.NewLine + "Kong", (int)MenuItem.Characters.King));
            numItems++;

            // Menu Description
            items.Add(new MenuItem(this.xPos + 650, this.yPos + 650,
                "Use [Arrow Keys] to select your character" + System.Environment.NewLine +
                "Use [Decimal] to go back" + System.Environment.NewLine +
                "Press [Enter] to confirm your selection", -1));


            // Map buttons to each other
            foreach (MenuItem item in items)
            {
                //set above and below values
                if (item.activeValue > 1)
                {
                    item.above = items.Find(x => x.activeValue == item.activeValue - 2);
                    item.below = item;  // Item is already at the bottom
                }

                else
                {
                    item.above = item;  // Item is already at the top
                    item.below = items.Find(x => x.activeValue == item.activeValue + 2);
                }


                // set left and right values
                if (item.activeValue % 2 == 0)
                {
                    item.left = item;   // Item is already on the left
                    item.right = items.Find(x => x.activeValue == item.activeValue + 1);
                }
                else
                {
                    item.left = items.Find(x => x.activeValue == item.activeValue - 1);
                    item.right = item;  // Item is already on the right
                }



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
                if (km.ActionPressed(KeyboardManager.action.up, KeyboardManager.playerIndex.one))
                {
                    // Check if player 1 has made a selection
                    if (characters.Count == 1)
                    {
                        // Only allow movement if player 1 has not chosen that character
                        if (items.Find(x => x.activeValue == currentMenuItem).above.activeValue != (int)characters[0])
                            currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).above.activeValue;
                    }
                    // Player 1 is selecting
                    else
                        currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).above.activeValue;

                }

                // Move Menu Selection Down:
                if (km.ActionPressed(KeyboardManager.action.down, KeyboardManager.playerIndex.one))
                {
                    // Check if player 1 has made a selection
                    if (characters.Count == 1)
                    {
                        // Only allow movement if player 1 has not chosen that character
                        if (items.Find(x => x.activeValue == currentMenuItem).below.activeValue != (int)characters[0])
                            currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).below.activeValue;
                    }
                    // Player 1 is selecting
                    else
                        currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).below.activeValue;
                }

                // Move Menu Selection Left:
                if (km.ActionPressed(KeyboardManager.action.left, KeyboardManager.playerIndex.one))
                {
                    // Check if player 1 has made a selection
                    if (characters.Count == 1)
                    {
                        // Only allow movement if player 1 has not chosen that character
                        if (items.Find(x => x.activeValue == currentMenuItem).left.activeValue != (int)characters[0])
                            currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).left.activeValue;
                    }
                    // Player 1 is selecting
                    else
                        currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).left.activeValue;

                }

                // Move Menu Selection Right:
                if (km.ActionPressed(KeyboardManager.action.right, KeyboardManager.playerIndex.one))
                {
                    // Check if player 1 has made a selection
                    if (characters.Count == 1)
                    {
                        // Only allow movement if player 1 has not chosen that character
                        if ((items.Find(x => x.activeValue == currentMenuItem).right.activeValue) != (int)characters[0])
                            currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).right.activeValue;
                    }
                    // Player 1 is selecting
                    else
                        currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).right.activeValue;
                }


                // Press ENTER while some menu item is highlighted:
                if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one))
                {
                    // Add character to List 
                    characters.Add((MenuItem.Characters)items.Find(x => (int)x.activeValue == currentMenuItem).activeValue);

                    // If only one player or both players have made selections
                    if (numOfPlayers == 1 || characters.Count == 2)
                    {
                        parentManager.gameOptions.characters = characters;  // Add list of characters to game options
                        S_DifficultyMenu diffMenu = new S_DifficultyMenu(parentManager, 0, 0);
                        parentManager.AddStateQueue(diffMenu);
                        this.flagForDeletion = true;
                    }
                    // Begin character selection for player 2
                    else
                    {
                        // Move selection to first availble character for player two
                        if (currentMenuItem == 0)
                            currentMenuItem = 1;
                        else
                            currentMenuItem = 0;
                    }
       
                }

                // Option: Cancel Key
                if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.one))
                {
                    // If first player has made a choice 
                    if(characters.Count == 1)
                    {
                        // Remove the character choice so they can choose again
                        characters.RemoveAt(0);
                    }
                    // No selections have been made and we return to the player count menu
                    else
                    {
                        S_PlayerCountMenu playerCountMenu = new S_PlayerCountMenu(parentManager, 0, 0);
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
            int i = 0;
            foreach (MenuItem item in items)
            {
                if (item.activeValue != -1)
                {
                    Vector2 pos = new Vector2(item.xPos, item.yPos);
                    Vector2 cloudPos = new Vector2(item.xPos - SPRITE_WIDTH / 2, item.yPos - SPRITE_HEIGHT / 2);
                    Vector2 textPos = CenterString.getCenterStringVector(pos, item.text, this.parentManager.game.ft_mainMenuFont);

                    // Cloud Background:
                    sb.Draw(this.parentManager.game.spr_cloudIcon, cloudPos, Color.White);

                    // Draw Text:
                    if (i == currentMenuItem)
                        tColor = Color.Blue;
                    // Color first character choice grey
                    else if (characters.Count == 1 && i == (int)characters[0])
                        tColor = Color.Gray;
                    else
                        tColor = Color.Red;
                    sb.DrawString(this.parentManager.game.ft_mainMenuFont, item.text, textPos, tColor);

                    i++;
                }
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
