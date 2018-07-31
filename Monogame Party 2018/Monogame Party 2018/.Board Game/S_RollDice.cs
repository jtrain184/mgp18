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
                        bounceUp = true;
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
                        bounceDown = true;
                    }
                    else
                    {
                        currPlayer.meeple.pos.Y = MGP_Tools.Ease(currPlayer.meeple.pos.Y, currPlayer.currSpace.pos.Y, 0.05f);
                    }
                }
                else if(bounceUp && bounceDown){

                    if (waitTime >= 15)
                    {
                        // Get player roll and reset dice
                        S_MovePlayer movePlayer = new S_MovePlayer(parentManager, 0, 0, parentManager.boardGame.testDice.diceRoll);

                        parentManager.AddStateQueue(movePlayer);
                        this.flagForDeletion = true;
                    }
                    waitTime++;
                }


               
            }

            // Listen for and allow a pause
            ListenPause();

        } // end update



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // Draw numbers on dice
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            parentManager.boardGame.testDice.Draw(gameTime);

        }


        public bool moveMeepleUp()
        {
            if ((parentManager.round.currPlayer.meeple.pos.Y - parentManager.boardGame.testDice.pos.Y) <= 64)
                return true;
            else
                return false;
        }

        public bool moveMeepleDown()
        {
            if (Vector2.Distance(parentManager.round.currPlayer.meeple.pos, parentManager.round.currPlayer.currSpace.getPosCenter()) <= 1.0f)
                return true;
            else
                return false;
        }
    }
}
