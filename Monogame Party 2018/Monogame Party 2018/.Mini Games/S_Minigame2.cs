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
        public List<Player> resultsList;

        Random random;
        Player playerOne;
        Player playerTwo;
        int numOfPlayers;
        int playerIndex;
        KeyboardManager.action[] directionKeys = { KeyboardManager.action.up, KeyboardManager.action.down, KeyboardManager.action.left, KeyboardManager.action.right };
        KeyboardManager.action currentMoveplayerOne;
        KeyboardManager.action currentMoveplayerTwo;
        KeyboardManager.action currentMoveplayerThree;
        KeyboardManager.action currentMoveplayerFour;


        //Debug
        public bool playGame;

        //Set move time for com to 500 ms
        private static readonly TimeSpan comMoveSpeed = TimeSpan.FromMilliseconds(200);
        private TimeSpan comLastMove;
        private bool isMoving = false;
        private int comMove = 1;

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
            playerOne = players[0];
            if(numOfPlayers  == 2) { playerTwo = players[1]; }

            if(parentManager.gameOptions.difficulty == MenuItem.Difficulty.EASY)
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
            
            foreach(Player p in players)
            {
                Console.WriteLine("Current multiplier = " + difficultyMultiplier);

                Console.WriteLine("Changing position of " + p.type + "at " + playerXPos + ", ");

                p.meeple.setX(playerXPos);
                p.meeple.setY(520);
                playerXPos += MGP_Constants.SCREEN_WIDTH / 5;
            }

            // Select first move at random
            currentMoveplayerOne = directionKeys[random.Next(directionKeys.Length)];
            Console.WriteLine("Current move 1 = " + currentMoveplayerOne);
            currentMoveplayerTwo = directionKeys[random.Next(directionKeys.Length)];
            Console.WriteLine("Current move 2 = " + currentMoveplayerTwo);
            currentMoveplayerThree = directionKeys[random.Next(directionKeys.Length)];
            Console.WriteLine("Current move 3 = " + currentMoveplayerThree);
            currentMoveplayerFour = directionKeys[random.Next(directionKeys.Length)];
            Console.WriteLine("Current move 4 = " + currentMoveplayerFour);


            // DEBUG: SKIP THE GAME
            this.playGame = playGame;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            float move = 10;
            Vector2 playerOnePosition = players[0].meeple.getPos();
            // Check for correct key press, and move up
            if (km.ActionPressed(currentMoveplayerOne,KeyboardManager.playerIndex.one))
            {
                players[0].meeple.setY(playerOnePosition.Y - move);
                currentMoveplayerOne = directionKeys[random.Next(directionKeys.Length)];
                Console.WriteLine("Current move 1 = " + currentMoveplayerOne);
            }

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

                // Check if only one player left
                if (players.Count == 1)
                {
                    items[items.Count - 1].text = (players[0].type.ToString() + "\n WINS");

                    // Add player to results list
                    resultsList.Add(players[0]);
                    S_MinigameResults minigameResults = new S_MinigameResults(parentManager, 0, 0, resultsList);
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

            sb.Draw(this.parentManager.game.bg_titleScreen, new Vector2(xPos, yPos), Color.White);

            for (int i = 3; i >= 0; i--)
            {
                sb.Draw(players[i].meeple.sprite, players[i].meeple.getPos(), Color.White);
            }

            // Player one instruction
            Vector2 playerOneInstructionPos= players[0].meeple.getPos();
            playerOneInstructionPos.Y = 600;
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, currentMoveplayerOne.ToString(), playerOneInstructionPos, Color.White);


            // End drawing:
            sb.End();
        }
    }
}
