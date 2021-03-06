﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018
{

    public class S_Board : State
    {


        // Member variables:
        public CameraProperties cameraProperties;
        public GameOptions gameOptions;
        public E_Space startingSpace;
        public E_Space finalSpace;
        public List<E_Space> spaces;

        public S_PlayerUI playerUI;

        // Game Vars
        public int numRounds;
        public S_Round round;
        public List<Player> players;
        public E_Dice testDice;


        // Constructor:
        public S_Board(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            cameraProperties = new CameraProperties();

            this.gameOptions = creator.gameOptions;

            //DEBUG:
            if (creator.gameOptions.numRounds < 1)
                this.numRounds = 1;
            else
                this.numRounds = gameOptions.numRounds;

            parentManager.boardGame = this;

            // Create testDice
            testDice = new E_Dice(this, parentManager.game.spr_testDice, MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y, 10);


            // Start first round:
            S_Round newRound = new S_Round(parentManager, 0, 0);
            parentManager.AddStateQueue(newRound);
            this.round = newRound;



        } // end constructor



        // UPDATE
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            //DEBUG:
            // Game is finished:
            if (round.currRound > numRounds) {

                // update music:
                parentManager.audioEngine.setNextSong(MGP_Constants.music.mainMenu);
                parentManager.audioEngine.playNextSong(40, true);

                // Clear all states except audio engine and this state:
                List<State> statesToRemove = new List<State>();
                foreach (State s in parentManager.states) {
                  if (s != parentManager.audioEngine && s != this) { statesToRemove.Add(s); }
                }

                // Remove them:
                foreach (State s in statesToRemove) { parentManager.RemoveState(s); }

                // Delay the update of the gameStateManager:
                sendDelay = 2;


                if (this.gameOptions.allowBonus)
                {
                    // Go to game results state to count bonuses
                    S_AlmostFinalResult results = new S_AlmostFinalResult(parentManager, 0, 0);
                    parentManager.AddStateQueue(results);
                }
                else
                {
                    // Go to final results
                    S_FinalResults finalResults = new S_FinalResults(parentManager, 0, 0);
                    parentManager.AddStateQueue(finalResults);

                }

                this.flagForDeletion = true;
                Console.WriteLine("Finished the game. Going to show the results state");
            }

            // Listen for pausing here:
            ListenPause();
        }

        // DRAW
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


    }

}
