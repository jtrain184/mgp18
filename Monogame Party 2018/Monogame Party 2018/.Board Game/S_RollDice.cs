using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_RollDice : State
    {


        public Player currPlayer;
        public E_Dice dice;
        bool isRolling = true;
        public int rollTime;
        public const int ROLL_SPEED = 20;    // Roll Speed = How many times you want it to change per second
        public int compRollBegin;
        public int compRollEnd;
        public bool playerSelected;
        public bool bounceUp;
        public bool bounceDown;
        public int waitTime;
        int rollingSoundCounter; // for sfx while rolling
        const int ROLL_SOUND_LEN = 24;
        const int PRE_ROLL_SOUND_LEN = 6;

        // Constructor
        public S_RollDice(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currPlayer = parentManager.round.currPlayer;
            dice = parentManager.boardGame.testDice;
            playerSelected = false;
            bounceUp = false;
            bounceDown = false;
            rollTime = 60 / ROLL_SPEED; // Start rolling right away
            parentManager.boardGame.testDice.diceRoll = 1;  // start dice at 1;

            compRollBegin = 0;
            compRollEnd = creator.random.Next(ROLL_SPEED, ROLL_SPEED * 2); // have computer 'hit the dice' after 1 - 2 secs
            waitTime = 0;

            // Sound Effects:
            creator.audioEngine.playSound(MGP_Constants.soundEffects.dicePre, 1.0f);
            rollingSoundCounter = PRE_ROLL_SOUND_LEN;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks) {
            base.Update(gameTime, ks);

            if (rollTime == 60 / ROLL_SPEED && isRolling) // update according to roll speed
            {

                rollTime = 0;
                dice.Update(gameTime, ks);

                // if comp is rolling
                if (!currPlayer.isHuman)
                    compRollBegin++;
            }
            else
            {

                rollTime++;
            }

            // Allow the transition to run after select is pressed jsut once
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one) && currPlayer.isHuman == true)
            {
                playerSelected = true;
            }

           // If human wait till select is pressed. If computer, wait till comp roll ends
           if(playerSelected || compRollBegin == compRollEnd)
            {
                isRolling = false;

                // move meeple up
                if (!bounceUp)
                {
                    if(Vector2.Distance(currPlayer.meeple.pos, dice.pos) <= 50.0f)
                    {
                        bounceUp = true;    // stop moving up

                        // play hit sound effect just once:
                        parentManager.audioEngine.playSound(MGP_Constants.soundEffects.diceHit, 0.8f);
                    }
                    else
                    {
                        currPlayer.meeple.pos.Y = MGP_Tools.Ease(currPlayer.meeple.pos.Y, dice.pos.Y, 0.1f);
                    }
                }
                // move meeple back down
                else if(bounceUp && !bounceDown)
                {
                    if (Vector2.Distance(currPlayer.meeple.getPosCenter(), currPlayer.currSpace.getPosCenter()) <= 10.0f)
                    {
                        bounceDown = true;  // stop moving down
                    }
                    else
                    {
                        currPlayer.meeple.pos.Y = MGP_Tools.Ease(currPlayer.meeple.pos.Y, currPlayer.currSpace.pos.Y, 0.05f);
                    }
                }
                // transistions are finished
                else if(bounceUp && bounceDown){


                    // wait 1/4 of a sec before the meeple starts moving spaces
                    if (waitTime >= 15)
                    {


                        // Start moving the player based on the roll
                        S_MovePlayer movePlayer = new S_MovePlayer(parentManager, 0, 0, parentManager.boardGame.testDice.diceRoll);
                        parentManager.AddStateQueue(movePlayer);
                        this.flagForDeletion = true;
                    }
                    waitTime++;
                }



            }

            // Listen for and allow a pause
            ListenPause();


            // Rolling Sound Effect:
            if (isRolling) {
              rollingSoundCounter--;
              if (rollingSoundCounter <= 0) {
                rollingSoundCounter = ROLL_SOUND_LEN;
                parentManager.audioEngine.playSound(MGP_Constants.soundEffects.diceRolling, 0.4f);
              }
            }

        } // end update



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // Draw numbers on dice
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            parentManager.boardGame.testDice.Draw(gameTime);

        }


    }
}
