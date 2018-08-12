
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
        public const int cloudWidth = 320;
        public const int cloudHeight = 80;
        public List<MenuItem> meeples;
        public List<Player.Type> players;
        public int comPlayer;

        public int currentMenuItem;
        public int numItems;
        public int numOfPlayers;

        public const string description = "Choose the character(s)\nthe Player(s) will use";
        public Vector2 glovePos;
        public bool moveGlove = false;


        // Constructor for Main Menu:
        public S_CharacterMenu(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            this.creator = creator;
            currentMenuItem = 0;

            players = new List<Player.Type>();
            numOfPlayers = parentManager.gameOptions.numPlayers;

            items = new List<MenuItem>()
            {
                new MenuItem(MGP_Constants.SCREEN_MID_X - (cloudWidth + 50), MGP_Constants.SCREEN_MID_Y - 75, "Frank", (int)Player.Type.FRANK),
                new MenuItem(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 75, "Louie", (int)Player.Type.LOUIE),
                new MenuItem(MGP_Constants.SCREEN_MID_X + (cloudWidth + 50), MGP_Constants.SCREEN_MID_Y - 75, "Manford", (int)Player.Type.MANFORD),
                new MenuItem(MGP_Constants.SCREEN_MID_X - (cloudWidth + 50), MGP_Constants.SCREEN_MID_Y + 25, "Sue", (int)Player.Type.SUE),
                new MenuItem(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y + 25, "Velma", (int)Player.Type.VELMA),
                new MenuItem(MGP_Constants.SCREEN_MID_X + (cloudWidth + 50), MGP_Constants.SCREEN_MID_Y + 25, "Wilber", (int)Player.Type.WILBER)
            };
            numItems = items.Count;

            meeples = new List<MenuItem>()
            {
                new MenuItem(items[0].xPos - (cloudWidth / 2 - 20), items[0].yPos - 24, parentManager.game.spr_Frank),
                new MenuItem(items[1].xPos - (cloudWidth / 2 - 20), items[1].yPos - 24, parentManager.game.spr_Louie),
                new MenuItem(items[2].xPos - (cloudWidth / 2 - 20), items[2].yPos - 24, parentManager.game.spr_Manford),
                new MenuItem(items[3].xPos - (cloudWidth / 2 - 20), items[3].yPos - 24, parentManager.game.spr_Sue),
                new MenuItem(items[4].xPos - (cloudWidth / 2 - 20), items[4].yPos - 24, parentManager.game.spr_Velma),
                new MenuItem(items[5].xPos - (cloudWidth / 2 - 20), items[5].yPos - 24, parentManager.game.spr_Wilber)
            };

            // MAP BUTTONS
            for (int i = 0; i < numItems; i++)
            {
                try { items[i].left = items[i - 1]; } catch (Exception) { items[i].left = null; }
                try { items[i].right = items[i + 1]; } catch (Exception) { items[i].right = null; }
                try { items[i].above = items[i - 3]; } catch (Exception) { items[i].above = null; }
                try { items[i].below = items[i + 3]; } catch (Exception) { items[i].below = null; }
            }

            glovePos = new Vector2(items[0].xPos - (cloudWidth / 2 + 60), items[0].yPos - 40);
        }


        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // Move Menu Selection Up:
            if (km.ActionPressed(KeyboardManager.action.up, KeyboardManager.playerIndex.one))
            {
                if (items[currentMenuItem].above != null && (players.Count == 0 || items[currentMenuItem].above.activeValue != (int)players[0]))
                {
                    currentMenuItem = items[currentMenuItem].above.activeValue;
                    moveGlove = true;
                }
            }

            // Move Menu Selection Down:
            if (km.ActionPressed(KeyboardManager.action.down, KeyboardManager.playerIndex.one))
            {
                if (items[currentMenuItem].below != null && (players.Count == 0 || items[currentMenuItem].below.activeValue != (int)players[0]))
                {
                    currentMenuItem = items[currentMenuItem].below.activeValue;
                    moveGlove = true;
                }
            }

            // Move Menu Selection Left:
            if (km.ActionPressed(KeyboardManager.action.left, KeyboardManager.playerIndex.one))
            {
                if (items[currentMenuItem].left != null && (players.Count == 0 || items[currentMenuItem].left.activeValue != (int)players[0]))
                {
                    currentMenuItem = items[currentMenuItem].left.activeValue;
                    moveGlove = true;
                }
            }

            // Move Menu Selection Right:
            if (km.ActionPressed(KeyboardManager.action.right, KeyboardManager.playerIndex.one))
            {
                if (items[currentMenuItem].right != null && (players.Count == 0 || items[currentMenuItem].right.activeValue != (int)players[0]))
                {
                    currentMenuItem = items[currentMenuItem].right.activeValue;
                    moveGlove = true;
                }
            }

            // Move glove
            if (moveGlove)
            {
                if (Vector2.Distance(glovePos, new Vector2(items[currentMenuItem].xPos - (cloudWidth / 2 + 60), items[currentMenuItem].yPos + 40)) < 1.0f)
                {
                    moveGlove = false;
                }
                else
                {
                    glovePos.X = MGP_Tools.Ease(glovePos.X, items[currentMenuItem].xPos - (cloudWidth / 2 + 60), 0.5f);
                    glovePos.Y = MGP_Tools.Ease(glovePos.Y, items[currentMenuItem].yPos - 40, 0.5f);
                }
            }


            // Press ENTER while some menu item is highlighted:
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one))
            {
                // Add character to List
                players.Add((Player.Type)items[currentMenuItem].activeValue);

                // If only one player or both players have made selections
                if (numOfPlayers == 1 || players.Count == 2)
                {
                    // Add the computer players at random
                    while (players.Count < 4)
                    {
                        comPlayer = parentManager.random.Next(0, 5);
                        if (!players.Contains((Player.Type)comPlayer))
                            players.Add((Player.Type)comPlayer);
                    }

                    // Create player entitities and add to game options
                    for (int i = 0; i < players.Count; i++)
                    {
                        // add as a human player
                        if(i == 0)
                            parentManager.gameOptions.players.Add(new Player(this.creator, players[i], true, KeyboardManager.playerIndex.one));
                        else if (i == numOfPlayers - 1)
                            parentManager.gameOptions.players.Add(new Player(this.creator, players[i], true, KeyboardManager.playerIndex.two));
                        // add a comp player
                        else
                            parentManager.gameOptions.players.Add(new Player(this.creator, players[i], false, KeyboardManager.playerIndex.none));
                    }

                    // Move onto next menu
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

            } // end of enter action key press

            // Option: Cancel Key
            if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.one))
            {
                // If first player has made a choice
                if (players.Count == 1)
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

            } // end of cancel key
        } // end of update


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
                sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)(items[i].xPos - (cloudWidth / 2)), (int)items[i].yPos - (cloudHeight / 2), cloudWidth, cloudHeight), Color.White);

                // Meeple
                sb.Draw(meeples[i].sprite, meeples[i].pos, Color.White);

                // Draw Text:
                if (i == currentMenuItem)
                    tColor = Color.Blue;
                // Color first character choice grey
                else if (players.Count == 1 && i == (int)players[0])
                    tColor = Color.Gray;
                else
                    tColor = Color.Red;
                Vector2 textPos = CenterString.getCenterStringVector(new Vector2(items[i].xPos, items[i].yPos), items[i].text, parentManager.game.ft_mainMenuFont);
                sb.DrawString(this.parentManager.game.ft_mainMenuFont, items[i].text, textPos, tColor);
            }

            // Background Box
            Vector2 backgroundBox = new Vector2(MGP_Constants.SCREEN_MID_X - 450, MGP_Constants.SCREEN_MID_Y + 150);
            sb.Draw(this.parentManager.game.spr_messageBox, new Rectangle((int)backgroundBox.X, (int)backgroundBox.Y, 900, 150), new Color(0, 0, 128, 150));

            // Description Text
            Vector2 textDesPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X + 450, backgroundBox.Y + 75), description, this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, description, textDesPos, Color.White);

            // Glove
            sb.Draw(parentManager.game.spr_glove, glovePos, Color.White);

            // End drawing:
            sb.End();
        }
    }
}
