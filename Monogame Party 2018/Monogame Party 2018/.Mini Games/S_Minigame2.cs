using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_Minigame2 : State
    {
        public List<Player> players;
        public List<int> comPlayers; // Stores index of computer players
        public List<Player> resultsList;

        public Texture2D arrowSprite;

        Random random;
        Player playerOne;
        Player playerTwo;
        public bool twoPlayers = false;
        public int numOfPlayers;
        KeyboardManager.action[] directionKeys = { KeyboardManager.action.up, KeyboardManager.action.down, KeyboardManager.action.left, KeyboardManager.action.right };
        public List<Vector2> currentMeeplePositions = new List<Vector2>(4);

        //Debug
        public bool playGame;

        //Set move time for com
        private static readonly TimeSpan comMoveSpeed = TimeSpan.FromMilliseconds(1000);
        private TimeSpan comLastMove;
        private bool raceOver = false;
        public double difficultyMultiplier;

        // Constructor for Main Menu:
        public S_Minigame2(GameStateManager creator, float xPos, float yPos, bool playGame) : base(creator, xPos, yPos)
        {
            random = new Random();

            // Get player info
            players = new List<Player>();
            players = creator.gameOptions.players.ToList();
            resultsList = new List<Player>();
            numOfPlayers = parentManager.gameOptions.numPlayers;

            // Set up computer players
            comPlayers = new List<int>();
            playerOne = players[0];
            if (numOfPlayers == 2)
            {
                // Two players, store player 2
                playerTwo = players[1];
                twoPlayers = true;
            }
            else
            {
                // Add second player to list of computer players
                comPlayers.Add(1);
            }

            comPlayers.Add(2);
            comPlayers.Add(3);

            if (parentManager.gameOptions.difficulty == MenuItem.Difficulty.EASY)
            {
                difficultyMultiplier = MGP_Constants.EASY_MULTIPLIER;
            }
            else if (parentManager.gameOptions.difficulty == MenuItem.Difficulty.MEDIUM)
            {
                difficultyMultiplier = MGP_Constants.MEDIUM_MULTIPLIER;
            }
            else
            {
                difficultyMultiplier = MGP_Constants.HARD_MULTIPLIER;
            }

            // Create Players and start positions for game
            int playerXPos = MGP_Constants.SCREEN_WIDTH / 5;

            for (int i = 0; i < players.Count; i++)
            {
                currentMeeplePositions.Add(new Vector2(playerXPos, 550));
                playerXPos += MGP_Constants.SCREEN_WIDTH / 5;
                // DEBUG
                Console.WriteLine("Start position " + i + " = " + currentMeeplePositions[i]);
            }

            // Select first move at random
            foreach (Player player in players)
            {
                player.currMove = directionKeys[random.Next(directionKeys.Length)];
            }

            // DEBUG: SKIP THE GAME
            this.playGame = playGame;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            float move = 50;
            Vector2 playerOnePosition = currentMeeplePositions[0];
            // Check for correct key press, and move up
            if (km.ActionPressed(players[0].currMove, KeyboardManager.playerIndex.one))
            {
                currentMeeplePositions[0] = new Vector2(playerOnePosition.X, playerOnePosition.Y - move);
                playerOne.currMove = directionKeys[random.Next(directionKeys.Length)];
                // DEBUG
                Console.WriteLine("Current move 1 = " + players[0].currMove);
                if (playerOnePosition.Y - move <= 20) { raceOver = true; }
            }
            if (twoPlayers)
            {
                if (km.ActionPressed(playerTwo.currMove, KeyboardManager.playerIndex.two))
                {
                    Vector2 playerTwoPosition = currentMeeplePositions[1];
                    currentMeeplePositions[1] = new Vector2(playerTwoPosition.X, playerTwoPosition.Y - move);
                    playerTwo.currMove = directionKeys[random.Next(directionKeys.Length)];
                    Console.WriteLine("Current move 1 = " + players[0].currMove);
                    if (playerTwoPosition.Y - move <= 20) { raceOver = true; }
                }
            }

            // Delay computer move
            if (comLastMove + comMoveSpeed < gameTime.TotalGameTime)
            {
                comLastMove = gameTime.TotalGameTime;

                foreach (int com in comPlayers)
                {
                    if (comChanceToMove())
                    {
                        currentMeeplePositions[com] = new Vector2(currentMeeplePositions[com].X, currentMeeplePositions[com].Y - move);
                        players[com].currMove = directionKeys[random.Next(directionKeys.Length)];
                        if (currentMeeplePositions[com].Y - move <= 20) { raceOver = true; }
                    }
                }
            }

            // DEBUG: SKIP THE GAME
            if (!playGame)
            {
                foreach (Player p in players)
                {
                    resultsList.Add(p);
                }
                this.flagForDeletion = true;
                Console.WriteLine("Finished minigame, going to results");
                S_MinigameResults minigameResults = new S_MinigameResults(parentManager, 0, 0, resultsList, 2);
                parentManager.AddStateQueue(minigameResults);
               
            }

            // Check if race is over
            if (raceOver)
            {
                // Old way of sorting
                // players.Sort((x, y) => y.meeple.pos.Y.CompareTo(x.meeple.pos.Y));
                float max = float.MinValue;
                List<int> previousMax = new List<int>();
                int maxIndex = -1000;
                for (int i = 0; i < currentMeeplePositions.Count; i++) { 
                    for (int j = 0; j < currentMeeplePositions.Count; j++)
                    {
                        if(currentMeeplePositions[j].Y > max && previousMax.Contains(j) == false)
                        {
                            max = currentMeeplePositions[j].Y;
                            maxIndex = j;
                        }
                    }
                    // Set old max, reset max
                    previousMax.Add(maxIndex);
                    max = float.MinValue;
                    // Add to results list
                    resultsList.Add(players[maxIndex]);
                }

                S_MinigameResults minigameResults = new S_MinigameResults(parentManager, 0, 0, resultsList, 2);
                parentManager.AddStateQueue(minigameResults);
                this.flagForDeletion = true;
                Console.WriteLine("Finished minigame, going to results");
            }
        }

        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();
            // Player Name 
            // Vector2 meeplePos = new Vector2(50, 1280 - );
            //Vector2 playerNamePos = new Vector2(meeplePos.X + playerMeeple.Width + 10, meeplePos.Y);
            //sb.DrawString(this.parentManager.game.ft_mainMenuFont, playerName, playerNamePos, Color.White);

            sb.Draw(this.parentManager.game.minigame_two_background, new Vector2(xPos, yPos), Color.White);
            sb.Draw(this.parentManager.game.minigame_two_racetrack, new Vector2(215, yPos), Color.White);
            sb.Draw(this.parentManager.game.minigame_two_racetrack, new Vector2(470, yPos), Color.White);
            sb.Draw(this.parentManager.game.minigame_two_racetrack, new Vector2(720, yPos), Color.White);
            sb.Draw(this.parentManager.game.minigame_two_racetrack, new Vector2(975, yPos), Color.White);


            for (int i = 3; i >= 0; i--)
            {
                sb.Draw(players[i].meeple.sprite, currentMeeplePositions[i], Color.White);
            }

            // Draw player instructions
            for (int i = 0; i < players.Count; i++)
            {
                Vector2 playerInstructionPosition = new Vector2(currentMeeplePositions[i].X - 21, 600);
                arrowSprite = getMoveSprite(players[i].currMove);
                sb.Draw(arrowSprite, playerInstructionPosition, Color.White);
            }

            // End drawing:
            sb.End();
        }

        public bool comChanceToMove()
        {
            var n = random.NextDouble();
            return n > difficultyMultiplier;
        }

        public Texture2D getMoveSprite(KeyboardManager.action currMove)
        {
            Texture2D moveSprite = parentManager.game.minigame_two_up_arrow;
            switch (currMove)
            {
                case KeyboardManager.action.up:
                    moveSprite = parentManager.game.minigame_two_up_arrow;
                    break;
                case KeyboardManager.action.down:
                    moveSprite = parentManager.game.minigame_two_down_arrow;
                    break;
                case KeyboardManager.action.left:
                    moveSprite = parentManager.game.minigame_two_left_arrow;
                    break;
                case KeyboardManager.action.right:
                    moveSprite = parentManager.game.minigame_two_right_arrow;
                    break;
                default:
                    break;
            }
            return moveSprite;
        }
    }
}
