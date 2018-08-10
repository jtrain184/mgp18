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
        public List<MenuItem> items;
        public List<int> itemsSelected;
        public List<Player> players;
        public List<Player> comPlayers;
        public List<Player> resultsList;

        public Texture2D arrowSprite;

        Random random;
        Player playerOne;
        Player playerTwo;
        bool twoPlayers = false;
        int numOfPlayers;
        KeyboardManager.action[] directionKeys = { KeyboardManager.action.up, KeyboardManager.action.down, KeyboardManager.action.left, KeyboardManager.action.right };

        //Debug
        public bool playGame;

        //Set move time for com
        private static readonly TimeSpan comMoveSpeed = TimeSpan.FromMilliseconds(1000);
        private TimeSpan comLastMove;
        private bool raceOver = false;

        double difficultyMultiplier;

        // Constructor for Main Menu:
        public S_Minigame2(GameStateManager creator, float xPos, float yPos, bool playGame) : base(creator, xPos, yPos)
        {
            random = new Random();
            items = new List<MenuItem>();
            itemsSelected = new List<int>();

            // Get player info
            players = new List<Player>();
            players = creator.gameOptions.players;
            int numPlayers = creator.gameOptions.numPlayers;
            resultsList = new List<Player>();
            numOfPlayers = parentManager.gameOptions.numPlayers;

            // Set up computer players
            comPlayers = new List<Player>();
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
                comPlayers.Add(players[1]);
            }

            comPlayers.Add(players[2]);
            comPlayers.Add(players[3]);

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

            foreach (Player p in players)
            {
                Console.WriteLine("Current multiplier = " + difficultyMultiplier);

                Console.WriteLine("Changing position of " + p.type + "at " + playerXPos + ", ");

                p.meeple.setX(playerXPos);
                p.meeple.setY(550);
                playerXPos += MGP_Constants.SCREEN_WIDTH / 5;
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
            Vector2 playerOnePosition = players[0].meeple.getPos();
            // Check for correct key press, and move up
            if (km.ActionPressed(players[0].currMove, KeyboardManager.playerIndex.one))
            {
                playerOne.meeple.setY(playerOnePosition.Y - move);
                playerOne.currMove = directionKeys[random.Next(directionKeys.Length)];
                Console.WriteLine("Current move 1 = " + players[0].currMove);
                if (playerOnePosition.Y - move <= 20) { raceOver = true; }
            }
            if (twoPlayers)
            {
                if (km.ActionPressed(playerTwo.currMove, KeyboardManager.playerIndex.two))
                {
                    Vector2 playerTwoPosition = playerTwo.meeple.getPos();
                    playerTwo.meeple.setY(playerTwoPosition.Y - move);
                    playerTwo.currMove = directionKeys[random.Next(directionKeys.Length)];
                    Console.WriteLine("Current move 1 = " + players[0].currMove);
                    if (playerTwoPosition.Y - move <= 20) { raceOver = true; }
                }
            }

            // Delay computer move
            if (comLastMove + comMoveSpeed < gameTime.TotalGameTime)
            {
                comLastMove = gameTime.TotalGameTime;

                foreach (Player com in comPlayers)
                {
                    if (comChanceToMove())
                    {
                        Vector2 comPosition = com.meeple.getPos();
                        com.meeple.setY(comPosition.Y - move);
                        com.currMove = directionKeys[random.Next(directionKeys.Length)];
                        if (comPosition.Y - move <= 20) { raceOver = true; }
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

                S_MinigameResults minigameResults = new S_MinigameResults(parentManager, 0, 0, resultsList, 2);
                parentManager.AddStateQueue(minigameResults);
                this.flagForDeletion = true;
                Console.WriteLine("Finished minigame, going to results");
            }

            // Check if race is over
            if (raceOver)
            {
                players.Sort((x, y) => y.meeple.pos.Y.CompareTo(x.meeple.pos.Y));
                // Add player to results list
                foreach (Player player in players)
                {
                    resultsList.Add(player);
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
                sb.Draw(players[i].meeple.sprite, players[i].meeple.getPos(), Color.White);
            }

            // Draw player instructions
            foreach (Player p in players)
            {
                Vector2 playerInstructionPosition = p.meeple.getPos();
                playerInstructionPosition.Y = 600;
                playerInstructionPosition.X = playerInstructionPosition.X - 21;
                arrowSprite = getMoveSprite(p.currMove);
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
