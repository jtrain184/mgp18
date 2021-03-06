﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace Monogame_Party_2018
{
    public class S_LandAction : State
    {
        public const int numCoins = 3;
        public Entity.typeSpace spaceType;
        public bool finishedAnimation;
        public int moveYPos;
        public MenuItem landAction;     // Update to new sprite when created



        // Constructor
        public S_LandAction(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            finishedAnimation = false;  // used to tell state when to move on
            moveYPos = 0;   // used to move the item down
            landAction = new MenuItem(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y, "", 0);

            spaceType = creator.round.currPlayer.currSpace.type;



            // Landed on a blue space
            if (spaceType == Entity.typeSpace.blue)
            {
                // Add 3 coins to player
                creator.round.currPlayer.coins += numCoins;
                creator.round.currPlayer.totalCoinsGained += numCoins;
                landAction.text = "+ " + numCoins.ToString();

                // Change player ui color:
                creator.round.currPlayer.uiColor = Color.Blue;

                // Play sound effect:
                parentManager.audioEngine.playSound(MGP_Constants.soundEffects.spaceBlue, 1.0f);
            }


            // Landed on a red space
            else if (spaceType == Entity.typeSpace.red)
            {
                // Subtract a 3 coins from player
                creator.round.currPlayer.coins -= numCoins;
                // Prevent negative values
                if (creator.round.currPlayer.coins < 0) { creator.round.currPlayer.coins = 0; }
                landAction.text = "- " + numCoins.ToString();

                // Change player ui color:
                creator.round.currPlayer.uiColor = Color.Red;

                // Play sound effect:
                parentManager.audioEngine.playSound(MGP_Constants.soundEffects.spaceRed, 0.6f);
            }


            // Landed on CHANCE TIME
            else if (spaceType == Entity.typeSpace.chance)
            {
                landAction.text = " This is a chance space";

                S_ChanceTime chanceEvent = new S_ChanceTime(parentManager, 0, 0, this);
                parentManager.AddStateQueue(chanceEvent);

                // Change player ui color:
                creator.round.currPlayer.uiColor = Color.Orange;

                // Silence music:
                creator.audioEngine.stopMusic(40);
            }


            // Landed on STAR SPACE
            else if (spaceType == Entity.typeSpace.star)
            {

                // Change player ui color:
                creator.round.currPlayer.uiColor = Color.Yellow;
            }
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
            //DEBUG:
            if (finishedAnimation)
            {

                parentManager.round.active = true;      //Make S_Round Active
                parentManager.round.playerIsPlaying = false;   // Allow S_Round to get next player
                this.flagForDeletion = true;
                Console.WriteLine("Performed land action code. Going back to S_Round");

            }
            if (Math.Abs(moveYPos) > 60)
                finishedAnimation = true;
            // make the items move up for blue spaces or star spaces
            else if (spaceType == Entity.typeSpace.blue || spaceType == Entity.typeSpace.star)
                moveYPos--;
            // make the items move down for red spaces
            else if (spaceType == Entity.typeSpace.red)
                moveYPos++;
            // DEBUG: Skip chance spaces for now
            else
            {
                moveYPos++;
                //moveYPos = 61;
            }
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();

            // Draw coins animation if landing on red or blue
            if (spaceType == Entity.typeSpace.blue || spaceType == Entity.typeSpace.red)
            {
                Vector2 coinPos = new Vector2(landAction.xPos, landAction.yPos + moveYPos);
                Vector2 coinTextPos = coinPos + new Vector2(30, -25);     // Draw text to the right of object


                sb.Draw(this.parentManager.game.spr_coin, new Rectangle((int)coinPos.X - 50 / 2, (int)coinPos.Y - 50 / 2, 50, 50), Color.White);
                if (spaceType == Entity.typeSpace.blue)
                    sb.DrawString(this.parentManager.game.ft_mainMenuFont, landAction.text, coinTextPos, Color.Blue);
                if (spaceType == Entity.typeSpace.red)
                    sb.DrawString(this.parentManager.game.ft_mainMenuFont, landAction.text, coinTextPos, Color.Red);
            }
            sb.End();
        } // end draw
    }
}
