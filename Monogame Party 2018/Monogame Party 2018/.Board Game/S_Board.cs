using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018
{

    public class S_Board : State {


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
                if (this.gameOptions.allowBonus)
                {
                    // Go to game results state to count bonuses
                    S_GameResults gameResults = new S_GameResults(parentManager, 0, 0);
                    parentManager.AddStateQueue(gameResults);
                }
                else
                {
                    // Go to final results
                    S_FinalResults finalResults = new S_FinalResults(parentManager, 0, 0);
                    parentManager.AddStateQueue(finalResults);

                }

                // Remove all states
                foreach (State s in parentManager.states)
                {
                    s.flagForDeletion = true;
                }
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
