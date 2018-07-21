
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
        public GameStateManager creator;
        public List<MenuItem> items;
        public List< Player.Type> players;

        int currentMenuItem;
        int numItems;
        int numOfPlayers;

        // Constructor for Main Menu:
        public S_CharacterMenu(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            this.creator = creator;
            currentMenuItem = 0;
            items = new List<MenuItem>();
            players = new List<Player.Type>();
            numOfPlayers = parentManager.gameOptions.numPlayers;


            // Character: Frank
            items.Add(new MenuItem(this.xPos + 300, this.yPos + 100, "Frank", (int)Player.Type.FRANK));
            numItems++;

            // Character: Louie
            items.Add(new MenuItem(this.xPos + 650, this.yPos + 100, "Louie", (int)Player.Type.LOUIE));
            numItems++;

            // Character: Manford
            items.Add(new MenuItem(this.xPos + 1000, this.yPos + 100, "Manford", (int)Player.Type.MANFORD));
            numItems++;

            // Character: Sue
            items.Add(new MenuItem(this.xPos + 300, this.yPos + 300, "Sue", (int)Player.Type.SUE));
            numItems++;

            // Character: Velma
            items.Add(new MenuItem(this.xPos + 650, this.yPos + 300, "Velma", (int)Player.Type.VELMA));
            numItems++;

            // Character: Wilber
            items.Add(new MenuItem(this.xPos + 1000, this.yPos + 300, "Wilber", (int)Player.Type.WILBER));
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
                if (item.activeValue > 2)
                {
                    item.above = items.Find(x => x.activeValue == item.activeValue - 3);
                    item.below = item;  // Item is already at the bottom
                }

                else
                {
                    item.above = item;  // Item is already at the top
                    item.below = items.Find(x => x.activeValue == item.activeValue + 3);
                }


                // set left and right values
                // left items
                if (item.activeValue % 3 == 0)
                {
                    item.left = item;   // Item is already on the left
                    item.right = items.Find(x => x.activeValue == item.activeValue + 1);
                }
                //Middle items
                else if(item.activeValue % 3 == 1)
                {
                    item.left = items.Find(x => x.activeValue == item.activeValue - 1);
                    item.right = items.Find(x => x.activeValue == item.activeValue + 1);
                }
                // right items
                else
                {
                    item.left = items.Find(x => x.activeValue == item.activeValue - 1);
                    item.right = item;   // Item is already on the right
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
                    if (players.Count == 1)
                    {
                        // Only allow movement if player 1 has not chosen that character
                        if (items.Find(x => x.activeValue == currentMenuItem).above.activeValue != (int)players[0])
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
                    if (players.Count == 1)
                    {
                        // Only allow movement if player 1 has not chosen that character
                        if (items.Find(x => x.activeValue == currentMenuItem).below.activeValue != (int)players[0])
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
                    if (players.Count == 1)
                    {
                        // Only allow movement if player 1 has not chosen that character
                        if (items.Find(x => x.activeValue == currentMenuItem).left.activeValue != (int)players[0])
                            currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).left.activeValue;
                        // the item to the left has been selected so choose the item to the left of the left item. 
                        else
                            currentMenuItem = items.Find(y => y.activeValue == items.Find(x => x.activeValue == currentMenuItem).left.activeValue).left.activeValue;
                    }
                    // Player 1 is selecting
                    else
                        currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).left.activeValue;

                }

                // Move Menu Selection Right:
                if (km.ActionPressed(KeyboardManager.action.right, KeyboardManager.playerIndex.one))
                {
                    // Check if player 1 has made a selection
                    if (players.Count == 1)
                    {
                        // Select the item directly to the right
                        if ((items.Find(x => x.activeValue == currentMenuItem).right.activeValue) != (int)players[0])
                            currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).right.activeValue;
                        // the item to the right has been selected so choose the item to the right of the right item. 
                        else
                            currentMenuItem = items.Find(y=> y.activeValue == items.Find(x => x.activeValue == currentMenuItem).right.activeValue).right.activeValue;

                    }
                    // Player 1 is selecting
                    else
                        currentMenuItem = items.Find(x => x.activeValue == currentMenuItem).right.activeValue;
                }


                // Press ENTER while some menu item is highlighted:
                if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one))
                {
                    // Add character to List 
                    players.Add((Player.Type)items.Find(x => (int)x.activeValue == currentMenuItem).activeValue);

                    // If only one player or both players have made selections
                    if (numOfPlayers == 1 || players.Count == 2)
                    {
                        // DEBUG: Add remaining characters to list
                        for(int j = 0; j < 6; j++)
                        {
                            if (!players.Contains((Player.Type)j))
                            {
                                players.Add((Player.Type)j);
                            }
                            if(players.Count >= 4)
                            {
                                j = 6;
                            }
                        }

                        // Create player entitities and add to game options
                        for(int i = 0; i < players.Count; i++)
                        {
                            // add as a human player
                            if(i < numOfPlayers)
                                parentManager.gameOptions.players.Add(new Player(this.creator, players[i], true));
                            // add a comp player
                            else
                                parentManager.gameOptions.players.Add(new Player(this.creator, players[i], false));

                        }
                         
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
                    if(players.Count == 1)
                    {
                        // Remove the character choice so they can choose again
                        players.RemoveAt(0);
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
                    else if (players.Count == 1 && i == (int)players[0])
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
