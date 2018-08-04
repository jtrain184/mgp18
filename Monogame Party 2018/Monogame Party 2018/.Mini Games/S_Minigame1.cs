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
        public List<E_MinigameOnePlunger> plungers;
        public int selectedPlungers;
        public List<Player> players;
        public List<Player> resultsList;


        public E_MinigameOnePlunger currSelection;
        public int plungerIndex;
        public Player currentPlayer;
        public int playerIndex;
        public List<Vector2> playerPositions;
        int numOfPlayers;

        public E_MinigameOneExplosion explosionSprite;
        public bool isExploding;


        //Debug
        public bool playGame;

        //Set move time for com to 500 ms
        private static readonly TimeSpan comMoveSpeed = TimeSpan.FromMilliseconds(500);
        private TimeSpan comLastMove;
        private bool isMoving = false;
        private int comMove = 1;

        // Constructor for Main Menu:
        public S_Minigame1(GameStateManager creator, float xPos, float yPos, bool playGame) : base(creator, xPos, yPos)
        {
            // Create the list of players and empty list for results
            players = new List<Player>();
            players = creator.gameOptions.players;
            resultsList = new List<Player>();

            // Create the list of positions for players
            playerPositions = new List<Vector2>();
            playerPositions.Add(new Vector2(150, 500));
            playerPositions.Add(new Vector2(450, 500));
            playerPositions.Add(new Vector2(750, 500));
            playerPositions.Add(new Vector2(1050, 500));

            // Create the list of plungers
            plungers = new List<E_MinigameOnePlunger>();
            plungers.Add(new E_MinigameOnePlunger(this, creator.game.minigame_one_plungerUp, 300, 275, Color.Green));
            plungers.Add(new E_MinigameOnePlunger(this, creator.game.minigame_one_plungerUp, 440, 275, Color.Blue));
            plungers.Add(new E_MinigameOnePlunger(this, creator.game.minigame_one_plungerUp, 590, 275, Color.Yellow));
            plungers.Add(new E_MinigameOnePlunger(this, creator.game.minigame_one_plungerUp, 740, 275, Color.Red));
            plungers.Add(new E_MinigameOnePlunger(this, creator.game.minigame_one_plungerUp, 875, 275, Color.Purple));

            // Set one of the plungers to be the bomb
            plungers[creator.random.Next(0, plungers.Count)].isBomb = true;

            // DEBUG: 
            Console.WriteLine("Bomb is color: " + plungers.Find(x => x.isBomb == true).color);

            // Start with 0 selected plungers
            selectedPlungers = 0;

            // Set number of human players
            numOfPlayers = parentManager.gameOptions.numPlayers;

            // Start with first player
            playerIndex = 0;
            currentPlayer = players[playerIndex];
            plungerIndex = 0;
            currSelection = plungers[plungerIndex];

            explosionSprite = new E_MinigameOneExplosion(this);
            isExploding = false;

            // DEBUG: SKIP THE GAME
            this.playGame = playGame;
        }


        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // DEBUG: SKIP THE GAME
            if (!playGame)
            {
                foreach (Player p in players)
                {
                    resultsList.Add(p);
                }

                S_MinigameResults minigameResults = new S_MinigameResults(parentManager, 0, 0, resultsList);
                parentManager.AddStateQueue(minigameResults);
                this.flagForDeletion = true;
                Console.WriteLine("Finished minigame, going to results");
            }

            // Check if exploding animation is active
            if (isExploding)
            {
                explosionSprite.Update(gameTime, ks);
                if (!explosionSprite.active)
                    isExploding = false;

            }
            else
            {
                // Update player index 
                playerIndex = players.FindIndex(x => x == currentPlayer);

                // Check if only one player left
                if (players.Count == 1)
                {
                    // Add player to results list
                    resultsList.Add(players[0]);
                    S_MinigameResults minigameResults = new S_MinigameResults(parentManager, 0, 0, resultsList);
                    parentManager.AddStateQueue(minigameResults);
                    this.flagForDeletion = true;

                }

                // Check if all players have gone
                else if (selectedPlungers == plungers.Count - 1)
                {
                    // Reset plungers
                    resetSelections();
                }
                // Whose Turn is it
                else
                {


                    // Computer Logic
                    if (!currentPlayer.isHuman)
                    {
                        // Set random amount of moves for computer
                        if (!isMoving)
                        {
                            comMove = parentManager.random.Next(plungers.Count, 10);
                            isMoving = true;
                            comLastMove = gameTime.TotalGameTime;
                        }
                        else
                        {
                            // Move computer selection
                            if (comMove > 0)
                            {
                                // Only move every 500ms
                                if (comLastMove + comMoveSpeed < gameTime.TotalGameTime)
                                {
                                    comLastMove = gameTime.TotalGameTime;
                                    // move selection by (skipping over already pressed plungers)
                                    getNextSelection();
                                    comMove--;
                                }
                            }
                            // Computer has finished moving
                            else
                            {
                                isMoving = false;

                                handleSelection();

                                // Move current selection to next available plunger
                                getNextSelection();
                            }

                        }
                    }   // End of Computer Logic

                    else
                    {

                        // Move Menu Selection Left:
                        if (km.ActionPressed(KeyboardManager.action.left, KeyboardManager.playerIndex.one))
                        {
                            getNextSelection();

                        }

                        // Move Menu Selection Right:
                        if (km.ActionPressed(KeyboardManager.action.right, KeyboardManager.playerIndex.one))
                        {
                            getNextSelectionRight();
                        }


                        // Human Player selectes a plunger
                        if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one))
                        {
                            handleSelection();

                            // Move current selection to next available plunger
                            getNextSelection();
                        }
                    }


                }
            }


        }

        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            
            SpriteBatch sb = this.parentManager.game.spriteBatch;
            sb.Begin();

            // Draw Background:
            sb.Draw(this.parentManager.game.minigame_one_background, new Vector2(0, 0), Color.White);

            // Draw Plungers
            foreach(E_MinigameOnePlunger p in plungers)
            {
                sb.Draw(p.sprite, p.pos, p.color);
            }

            // Draw current player highlighter
            sb.Draw(parentManager.game.minigame_one_currPlayer, new Rectangle((int)playerPositions[playerIndex].X - 30, (int)playerPositions[playerIndex].Y - 15, 150, 150), new Color(255, 255, 255, 255));    // Draw color white semi transparent

            // Draw Players
            for (int i = 0; i < players.Count; i++)
            {
                sb.Draw(players[i].meeple.sprite, new Rectangle((int)playerPositions[i].X, (int)playerPositions[i].Y, 100, 100), Color.White);
            }

            

            // Draw current selection hand
            sb.Draw(parentManager.game.spr_glove, new Vector2(currSelection.pos.X - 60, currSelection.pos.Y + 40), Color.White);

       
            // End drawing:
            sb.End();

            if (isExploding)
            {
                explosionSprite.Draw(gameTime);
            }
                
            

        }


        // HELPER FUNCTIONS

        // Reset plunger selections
        public void resetSelections()
        {
            selectedPlungers = 0;
            foreach(E_MinigameOnePlunger p in plungers)
            {
                p.isBomb = false;
                p.pressed = false;
                p.Update_Sprite();
            }

            // Assign a new bomb
            plungers[parentManager.random.Next(0, plungers.Count)].isBomb = true;

            // Reset current selection
            currSelection = plungers[0];
        }

        // Get next available plunger selection to the left
        public void getNextSelection()
        {
            do
            {
                if (currSelection == plungers[0])
                    currSelection = plungers[plungers.Count - 1];
                else
                    currSelection = plungers[plungers.FindIndex(x => x == currSelection) - 1];
            } while (currSelection.pressed);
        }

        // Get next available plunger selection to the right
        public void getNextSelectionRight()
        {
            do
            {
                if (currSelection == plungers[plungers.Count - 1])
                    currSelection = plungers[0];
                else
                    currSelection = plungers[plungers.FindIndex(x => x == currSelection) + 1];
            } while (currSelection.pressed);
        }

        // Player made selection
        public void handleSelection()
        {
            // Player selected the bomb
            if (currSelection.isBomb)
            {
                bombChosen();
            }
            // Player did not select the bomb
            else
            {
                bombNotChosen();
            }
        }

        // Selection is bomb
        public void bombChosen()
        {

            // Remove plunger from game
            plungers.Remove(currSelection);

            // Reset plungers
            resetSelections();

            // Remove player from game
            players.Remove(currentPlayer);

            // Add player to results list
            resultsList.Add(currentPlayer);


            // Get next player
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

            isExploding = true;
            explosionSprite.active = true;
            

        }

        // Selection is not bomb
        public void bombNotChosen()
        {
            // Mark selection as pressed
            selectedPlungers++;
            currSelection.pressed = true;
            currSelection.Update_Sprite();

            // get next player
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
