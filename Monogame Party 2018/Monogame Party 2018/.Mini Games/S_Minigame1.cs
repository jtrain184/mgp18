using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_Minigame1 : State
    {
        public List<MenuItem> items;
        public List<int> itemsSelected;
        public List<Player> players;
        public List<MenuItem.Characters> characters;    // used in creating players, can be removed when a list of players is passed

        // ---------Replace once player class is made ------------------
        public class Player
        {
            public MenuItem.Characters character;
            public enum PlayerType
            {
                PLAYER_ONE = 0,
                PLAYER_TWO,
                COM
            }

            public PlayerType pType;

            // constructor
            public Player(MenuItem.Characters character, PlayerType pType)
            {
                this.character = character;
                this.pType = pType;
            }

        }

        int currentMenuItem;
        int currentBomb;
        Random random;
        Player currentPlayer;
        int numItems;
        int numOfPlayers;
        int playerIndex;


        //Set move time for com to 500 ms
        private static readonly TimeSpan comMoveSpeed = TimeSpan.FromMilliseconds(200);
        private TimeSpan comLastMove;
        private bool isMoving = false;
        private int comMove = 1;

        // Constructor for Main Menu:
        public S_Minigame1(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currentMenuItem = 0;
            random = new Random();
            currentBomb = random.Next(0, 5);
            Console.WriteLine("Bomb is at " + currentBomb);

            items = new List<MenuItem>();
            itemsSelected = new List<int>();
            players = new List<Player>();

            characters = new List<MenuItem.Characters>();   // Can be removed when player class is created

            numOfPlayers = parentManager.gameOptions.numPlayers;

            // Can be removed when player class is created
            characters.Add(MenuItem.Characters.Princess);
            characters.Add(MenuItem.Characters.Prince);
            characters.Add(MenuItem.Characters.Queen);
            characters.Add(MenuItem.Characters.King);


            // Create Players - Can be removed when player class is created

            // Player 1
            players.Add(new Player(parentManager.gameOptions.characters[0], Player.PlayerType.PLAYER_ONE));
            characters.Remove(players[0].character);

            // Player 2
            if(numOfPlayers == 2)
            {
                players.Add(new Player(parentManager.gameOptions.characters[1], Player.PlayerType.PLAYER_TWO));
                characters.Remove(players[1].character);
            }
            else
            {
                players.Add(new Player(characters[0], Player.PlayerType.COM));
                characters.Remove(players[1].character);
            }

            // Player 3
            players.Add(new Player(characters[0], Player.PlayerType.COM));
            characters.Remove(players[2].character);

            // Player 4
            players.Add(new Player(characters[0], Player.PlayerType.COM));


            currentPlayer = players[0];


            // Create Boxes for game


            // Box: One
            items.Add(new MenuItem(this.xPos + 300, this.yPos + 100, "Box 1", 0));
            numItems++;

            // Box: Two
            items.Add(new MenuItem(this.xPos + 650, this.yPos + 100, "Box 2", 1));
            numItems++;

            // Box: Three
            items.Add(new MenuItem(this.xPos + 1000, this.yPos + 100, "Box 3", 2));
            numItems++;

            // Box: Four
            items.Add(new MenuItem(this.xPos + 475, this.yPos + 300, "Box 4", 3));
            numItems++;

            // Box: Five
            items.Add(new MenuItem(this.xPos + 825, this.yPos + 300, "Box 5", 4));
            numItems++;


            // Create Player Items for game
            int playerXPos = 100;
            int playerID = 5;
            foreach(Player p in players)
            {
                items.Add(new MenuItem(this.xPos + playerXPos, this.yPos + 500, p.character.ToString(), playerID));
                playerXPos += 350;
                playerID++;
            }
            // Menu Description
            items.Add(new MenuItem(this.xPos + 650, this.yPos + 650,
                "Try and guess the boxes that do not contain a bomb" + System.Environment.NewLine +
                "Use [Arrow Keys] to select a box" + System.Environment.NewLine +
                "Press [Enter] to confirm your selection", -1));





        }


        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
         

            // If this is the top layer, allow moving active menu:
            if (this.isTopLayer)
            {
                playerIndex = players.FindIndex(x => x == currentPlayer);

                // Check if only one player left
                if (players.Count == 1)
                {
                    items[items.Count - 1].text = (players[0].character.ToString() + "\n WINS");
                }

                // Check if all players have gone
                else if (itemsSelected.Count == numItems - 1)
                {
                    itemsSelected = new List<int>();
                    currentBomb = random.Next(0, numItems);
                    Console.WriteLine("Bomb is at " + currentBomb);
                }
                // Whose Turn is it
                else
                    // move current menu item to first available item
                   
                {
                    if (currentPlayer.pType == Player.PlayerType.COM)
                    {
                        if (!isMoving)
                        {
                            comMove = random.Next(numItems, 10);
                            isMoving = true;
                            comLastMove = gameTime.TotalGameTime;

                            do
                            {
                                if (currentMenuItem == 0)
                                    currentMenuItem = numItems - 1;
                                else
                                    currentMenuItem--;
                            } while (itemsSelected.Contains(currentMenuItem));
                        }
                        else
                        {
                            if (comMove > 0)
                            {
                                // Only move every 500ms
                                if (comLastMove + comMoveSpeed < gameTime.TotalGameTime)
                                {
                                    comLastMove = gameTime.TotalGameTime;
                                    do
                                    {
                                        if (currentMenuItem == 0)
                                            currentMenuItem = numItems - 1;
                                        else
                                            currentMenuItem--;
                                    } while (itemsSelected.Contains(currentMenuItem));
                                    comMove--;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Computer chose item " + currentMenuItem);
                                isMoving = false;
                                // Computer has chosen current item
                                if (currentBomb == currentMenuItem)
                                {
                                    // Remove Items to draw
                                    numItems--;
                                    items.Remove(items[numItems]);
                                    
                                    items.Remove(items[numItems + playerIndex]);
                                   
                                    // Reset items selected list
                                    itemsSelected = new List<int>();

                                    // Remove player and get next player
                                    players.Remove(currentPlayer);

                                    // If removed player was the last player
                                    if (playerIndex == players.Count)
                                    {
                                        //next player is first player
                                        currentPlayer = players[0];
                                    }
                                    // if removed player was second to last player
                                    else if(playerIndex == players.Count - 1)
                                    {
                                        currentPlayer = players[playerIndex];
                                    }
                                    else
                                    {
                                        currentPlayer = players[playerIndex + 1];
                                    }

                                    // Reset bomb
                                    currentBomb = random.Next(0, numItems);
                                    
                                    Console.WriteLine("Bomb is at " + currentBomb);
                                }
                                else
                                {
                                    itemsSelected.Add(currentMenuItem);
                                    if (playerIndex == players.Count - 1)
                                    {
                                        currentPlayer = players[0];
                                    }
                                    else
                                    {
                                        currentPlayer = players[playerIndex + 1];
                                    }
                                }

                                // Move current item to forst available item
                                do
                                {
                                    if (currentMenuItem == 0)
                                        currentMenuItem = numItems - 1;
                                    else
                                        currentMenuItem--;
                                } while (itemsSelected.Contains(currentMenuItem));
                            }

                        }
                    }

                    else
                    {

                        // Move Menu Selection Left:
                        if (km.ActionPressed(KeyboardManager.action.left, KeyboardManager.playerIndex.one))
                        {
                            do
                            {
                                if (currentMenuItem == 0)
                                    currentMenuItem = numItems - 1;
                                else
                                    currentMenuItem--;
                            } while (itemsSelected.Contains(currentMenuItem));

                        }

                        // Move Menu Selection Right:
                        if (km.ActionPressed(KeyboardManager.action.right, KeyboardManager.playerIndex.one))
                        {
                            do
                            {
                                if (currentMenuItem == numItems - 1)
                                    currentMenuItem = 0;
                                else
                                    currentMenuItem++;
                        }   while (itemsSelected.Contains(currentMenuItem)) ;
                    }


                        // Press ENTER while some menu item is highlighted:
                        if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one))
                        {
                            
                            if (currentBomb == currentMenuItem)
                            {
                                numItems--;
                                items.Remove(items[numItems]);
                                
                             
                                items.Remove(items[numItems + playerIndex ]);
                         
                                itemsSelected = new List<int>();


                                players.Remove(currentPlayer);
                                // If removed player was the last player
                                if (playerIndex == players.Count)
                                {
                                    //next player is first player
                                    currentPlayer = players[0];
                                }
                                // if removed player was second to last player
                                else if (playerIndex == players.Count - 1)
                                {
                                    currentPlayer = players[playerIndex];
                                }
                                else
                                {
                                    currentPlayer = players[playerIndex + 1];
                                }
                                currentBomb = random.Next(0, numItems);
                                currentMenuItem = 0;
                                Console.WriteLine("Bomb is at " + currentBomb);
                            }
                            else
                            {
                                itemsSelected.Add(currentMenuItem);
                                if (playerIndex == players.Count - 1)
                                {
                                    currentPlayer = players[0];
                                }
                                else
                                {
                                    currentPlayer = players[playerIndex + 1];
                                }
                            }

                        }
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
            if (players.Count != 1)
            {
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
                        if (i == currentMenuItem || i == playerIndex + numItems)
                            tColor = Color.Blue;
                        // Color first character choice grey
                        else if (itemsSelected.Count > 0 && itemsSelected.Contains(i))
                            tColor = Color.Gray;
                        else
                            tColor = Color.Red;
                        sb.DrawString(this.parentManager.game.ft_mainMenuFont, item.text, textPos, tColor);

                        i++;
                    }
                }


                // Draw the Menu description cloud wider
                Vector2 menuItemPos = new Vector2(items[items.Count - 1].xPos, items[items.Count - 1].yPos);
                Vector2 menuTextPos = CenterString.getCenterStringVector(menuItemPos, items[items.Count - 1].text, this.parentManager.game.ft_menuDescriptionFont);
                sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)items[items.Count - 1].xPos - 600 / 2, (int)items[items.Count - 1].yPos - 140 / 2, 600, 140), Color.White);
                sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, items[items.Count - 1].text, menuTextPos, Color.Black);
            }
            else
            {
                // Draw the Menu description cloud wider
                Vector2 menuItemPos = new Vector2(650, 250);
                Vector2 menuTextPos = CenterString.getCenterStringVector(menuItemPos, items[items.Count - 1].text, this.parentManager.game.ft_menuDescriptionFont);
                sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle(650 - 600 / 2, 250 - 140 / 2, 600, 140), Color.White);
                sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, items[items.Count - 1].text, menuTextPos, Color.Black);
            }
            // End drawing:
            sb.End();
        }
    }


}
