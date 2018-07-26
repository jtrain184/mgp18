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

        public GameOptions gameOptions;
        public Player currPlayer;
        public bool playerIsPlaying;    //determine whether or not to change current player
        public int playerIndex;     // current player
        public int currRound;   // current round number

        // Constructor
        public S_Round(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            // Assign the round to gamestate manager
            parentManager.round = this;

            gameOptions = creator.gameOptions;

            currPlayer = gameOptions.players[0];
            playerIndex = 0;
            playerIsPlaying = false;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks) {
            base.Update(gameTime, ks);

            MGP_Tools.Follow_Player(parentManager, currPlayer);

            //DEBUG:
            if (!playerIsPlaying) {
                // Last player went. Go to minigame
                if (playerIndex == 4)
                {
                    S_MinigameInstructions minigameInstructions = new S_MinigameInstructions(parentManager, 0, 0);
                    parentManager.AddStateQueue(minigameInstructions);
                    playerIndex = 0;    // start with player one when round resumes
                    this.active = false;

                    
                }
                // Start next players turn
                else
                {
                    currPlayer = gameOptions.players[playerIndex];  // set current player
                    MGP_Tools.Follow_Player(parentManager, currPlayer); // move camera to current player

                    // start confirm player state
                    S_ConfirmPlayer confirmPlayer = new S_ConfirmPlayer(parentManager, 0, 0);
                    parentManager.AddStateQueue(confirmPlayer);
                    this.active = false;

                    //set vars for next round
                    playerIndex++;
                    playerIsPlaying = true;
                }
            }

            // Listen for and allow for pauses:
            ListenPause();


        } // end Update method



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
