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
        public int compRollBegin;
        public int compRollEnd;

        // Constructor
        public S_RollDice(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currPlayer = parentManager.round.currPlayer;
            rollTime = 60 / ROLL_SPEED; // Start rolling right away
            parentManager.boardGame.testDice.diceRoll = 1;  // start dice at 1;

            compRollBegin = 0;
            compRollEnd = creator.random.Next(ROLL_SPEED, ROLL_SPEED * 3); // have computer 'hit the dice' after 1 - 3 secs
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks) {
            base.Update(gameTime, ks);

            if (rollTime == 60 / ROLL_SPEED && isRolling) // update according to roll speed
            {

                rollTime = 0;
                parentManager.boardGame.testDice.Update(gameTime, ks);

                // if comp is rolling
                if (!currPlayer.isHuman)
                    compRollBegin++;
            }
            else
            {

                rollTime++;
            }



            // If human wait till select is pressed. If computer, wait till comp roll ends
            if ((km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one) && currPlayer.isHuman == true) || compRollBegin == compRollEnd)
            {
                isRolling = false;
                Console.WriteLine(currPlayer.type + " has rolled a " + parentManager.boardGame.testDice.diceRoll.ToString());



                // Get player roll and reset dice
                S_MovePlayer movePlayer = new S_MovePlayer(parentManager, 0, 0, parentManager.boardGame.testDice.diceRoll);

                parentManager.AddStateQueue(movePlayer);
                this.flagForDeletion = true;
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
    }
}
