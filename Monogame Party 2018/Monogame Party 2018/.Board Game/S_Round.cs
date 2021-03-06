﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

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
        public int minigame = 1;

        public bool roundStart = true;

        // Constructor
        public S_Round(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            // Assign the round to gamestate manager
            parentManager.round = this;

            gameOptions = creator.gameOptions;

            // Set first player to player index 0;
            playerIndex = 0;
            currPlayer = gameOptions.players[playerIndex];
            playerIsPlaying = false;
            currRound = 1;

        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // do this at the beginning of every round, but only once:
            if (roundStart)
            {

                // Set the player ui's to white at the beginning of each round:
                foreach (Player p in parentManager.gameOptions.players) { p.uiColor = Color.White; }

                // switch minigames:
                minigame = (minigame == 0) ? 1 : 0;

                // Queue pirate bay music again:
                parentManager.audioEngine.setNextSong(MGP_Constants.music.pirateBay);
                parentManager.audioEngine.playNextSong(85, true);
                Console.WriteLine("Added pirateBay to music queue");

                // only run once each round:
                roundStart = false;
            }

            // Move camera to player
            MGP_Tools.Follow_Player(parentManager, currPlayer);

            // Start next player's turn
            if (!playerIsPlaying)
            {
                // Last player went. Go to minigame
                if (playerIndex == 4)
                {

                    // Queue minigame music:
                    parentManager.audioEngine.setNextSong(MGP_Constants.music.minigame);
                    parentManager.audioEngine.playNextSong(50, true);

                    S_MinigameInstructions minigameInstructions = new S_MinigameInstructions(parentManager, 0, 0, minigame);
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

            // Update player places:
            updatePlayerPlaces();

        } // end Update method



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


        public void updatePlayerPlaces()
        {
            // start with all players:
            List<Player> playersLeft = new List<Player>();
            List<Player> playersToRemove = new List<Player>();
            foreach (Player p in parentManager.gameOptions.players)
            {
                playersLeft.Add(p);
            }

            // now based on that score, place the players:
            int place = 1; // start with first place
            while (playersLeft.Count > 0)
            {
                int largestScore = 0;
                int curScore;
                foreach (Player p in playersLeft)
                { // determine largest score
                    curScore = p.GetCombinedScore();
                    if (curScore > largestScore)
                        largestScore = curScore;
                }

                foreach (Player p in playersLeft)
                { // assign place of current largest score (can be ties)
                    curScore = p.GetCombinedScore();
                    if (curScore == largestScore)
                    {
                        p.place = place;
                        playersToRemove.Add(p);
                    }
                }

                // Clean up playersLeft (cannot remove directly in above loop)
                foreach (Player p in playersToRemove) { playersLeft.Remove(p); }
                playersToRemove.Clear();

                place++; // since there are only 4 players, the maximum place will have is 4
            } // end while

        } // end update places



    } // end S_Round class
}
