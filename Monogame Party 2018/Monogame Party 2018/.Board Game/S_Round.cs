using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_Round : State
    {
        // Vars
        //public Player currPlayer;
        public GameOptions gameOptions;
        public Player currPlayer;  //Swap with Player code once we get that set up
        public bool playerIsPlaying;
        public int playerIndex;

        // Constructor
        public S_Round(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            // Assign this round to gamestate manager
            parentManager.round = this;

            gameOptions = creator.gameOptions;

            // DEBUG:
            Console.WriteLine("Created Round " + creator.boardGame.currRound);
            currPlayer = gameOptions.players[0];
            playerIndex = -1;
            playerIsPlaying = false;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);


            //DEBUG:
            if (!playerIsPlaying)
            {
                // Last player went. Go to minigame
                if (playerIndex == 3)
                {
                    S_MinigameInstructions minigameInstructions = new S_MinigameInstructions(parentManager, 0, 0);
                    parentManager.AddStateQueue(minigameInstructions);
                    this.flagForDeletion = true;
                    Console.WriteLine("Round over, going to minigame. Closing Round state");
                }
                else
                {
                    playerIndex++;
                    playerIsPlaying = true;
                    currPlayer = gameOptions.players[playerIndex];


                    S_ConfirmPlayer confirmPlayer = new S_ConfirmPlayer(parentManager, 0, 0);
                    parentManager.AddStateQueue(confirmPlayer);
                    this.active = false;
                    Console.WriteLine("Paused the S_Round");
                }
            }

        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
