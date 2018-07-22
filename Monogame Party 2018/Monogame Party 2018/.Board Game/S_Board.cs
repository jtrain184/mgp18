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

    public class S_Board : State
    {

        // Member variables:
        public CameraProperties cameraProperties;
        public GameOptions gameOptions;

        // Game Vars
        public int numRounds;
        public int currRound;
        public S_Round round;
        public List<Player> players;

        public E_Dice testDice;



        // TODO ------------------- ROUNDS ----------------------- <<< -------- DO IT


        // Constructor:
        public S_Board(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            cameraProperties = new CameraProperties();

            this.gameOptions = creator.gameOptions;
            this.currRound = 1;
            //DEBUG:
            if (creator.gameOptions.numRounds < 1)
                this.numRounds = 1;
            else
                this.numRounds = gameOptions.numRounds;

            parentManager.boardGame = this;

            // Create testDice
            testDice = new E_Dice(this, parentManager.game.spr_testDice, MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y, 10);

            // Create characters:
            // foreach (MenuItem.Characters i in this.gameOptions.characters) {


            //}


        } // end constructor



        // UPDATE
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            //DEBUG:
            // Game is finished:
            if (currRound > numRounds) {
                S_GameResults gameResults = new S_GameResults(parentManager, 0, 0);
                parentManager.AddStateQueue(gameResults);
                this.flagForDeletion = true; // delete this S_Board object
                Console.WriteLine("Finished the game. Going to show the results state");
            }

            // confirm player start?
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one)) {

                // Create a new Round:
                S_Round newRound = new S_Round(parentManager, 0, 0);
                parentManager.AddStateQueue(newRound);
                this.active = false;
                Console.WriteLine("Paused the S_Board");
                currRound++;
            }
        }

        // DRAW
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

    }
}
