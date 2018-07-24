using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_MovePlayer : State
    {
        public int moveNum;
        public Player currPlayer;
        // Constructor
        public S_MovePlayer(GameStateManager creator, float xPos, float yPos, int moveNum) : base(creator, xPos, yPos)
        {
            this.moveNum = moveNum;
            if (parentManager.round == null)    // DEBUG: Used for creating a board without players
                this.currPlayer = new Player(creator, Player.Type.FRANK, true);
            else
                this.currPlayer = parentManager.round.currPlayer;

        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);



            // Move the player
            if (moveNum > 0)
            {


                // Move the meeple untill it's close enough to space
                // Find next space
                E_Space spaceToMoveTo = currPlayer.currSpace.spacesAhead[0];

                if (Vector2.Distance(spaceToMoveTo.getPosCenter(), currPlayer.meeple.getPosCenter()) > 1.0F) {
          //currPlayer.meeple.setPos(MGP_Tools.EaseVector(currPlayer.meeple.getPosCenter(), spaceToMoveTo.getPosCenter(), 1.5F));
                    float newX = MGP_Tools.Ease(currPlayer.meeple.getPosCenter().X, spaceToMoveTo.getPosCenter().X, 0.15F);
                    float newY = MGP_Tools.Ease(currPlayer.meeple.getPosCenter().Y, spaceToMoveTo.getPosCenter().Y, 0.15F);
                    currPlayer.meeple.setPos(new Vector2(newX, newY));
                    MGP_Tools.Follow_Player(parentManager, currPlayer);
                }
                // Meeple has arrived at new space
                else
                {
                    moveNum--;
                    currPlayer.currSpace = spaceToMoveTo;

                    // TO DO : Create state to buy star option ----------------------------------------------------------------
                    if(currPlayer.currSpace.type == Entity.typeSpace.star)
                    {
                        S_LandAction landAction = new S_LandAction(parentManager, 0, 0);
                        parentManager.AddStateQueue(landAction);
                    }
                }



            }
            // finished moving meeple
            else
            {

                Console.WriteLine("Moved " + currPlayer.type + " " + moveNum.ToString() + " spaces");


                S_LandAction landAction = new S_LandAction(parentManager, 0, 0);
                parentManager.AddStateQueue(landAction);
                this.flagForDeletion = true;
            }


            // Listen for pausing here:
            ListenPause();


        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {

            // DEBUG: Show the number of spaces left to move.
            SpriteBatch sb = this.parentManager.game.spriteBatch;
            sb.Begin();
            SpriteFont spacesLeft = this.parentManager.game.ft_mainMenuFont;
            sb.DrawString(spacesLeft, moveNum.ToString(), new Vector2(MGP_Constants.SCREEN_MID_X + 250, MGP_Constants.SCREEN_MID_Y - 125), Color.Black);
            sb.End();

        }

        public void MovePlayer()
        {
            // Add code that moves the player to next spaces

        }
    }
}
