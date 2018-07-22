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
        bool isRolling = true;
        public int rollTime;
        public const int ROLL_SPEED = 20;    // Roll Speed = How many times you want it to change per second

        // Constructor
        public S_RollDice(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currPlayer = parentManager.round.currPlayer;
            rollTime = 60 / ROLL_SPEED; // Start rolling right away
            parentManager.boardGame.testDice.diceRoll = 1;  // start dice at 1;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            if (rollTime == 60 / ROLL_SPEED && isRolling) // Update twice per second
            {
                //isRolling = true;
                rollTime = 0;
                parentManager.boardGame.testDice.Update(gameTime, ks);
            }
            else
            {
                //isRolling = false;
                rollTime++;
            }


            /* if (isRolling)
             {

                 // testDice update
                 parentManager.boardGame.testDice.Update(gameTime, ks);
             }
             */

            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one))
            {
                isRolling = false;
                Console.WriteLine(currPlayer.type + " has rolled a " + parentManager.boardGame.testDice.diceRoll.ToString());

                // DEBUG: Paused game here to make sure dice roll number matches what is displayed

             
                
                // Get player roll and reset dice
                S_MovePlayer movePlayer = new S_MovePlayer(parentManager, 0, 0, parentManager.boardGame.testDice.diceRoll);
                
                parentManager.AddStateQueue(movePlayer);
                this.flagForDeletion = true;
                

            }
        }



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
