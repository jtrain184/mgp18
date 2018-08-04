using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
namespace Monogame_Party_2018
{
    public class S_ConfirmPlayer : State
    {
        public int transitionTimerOne;
        public int transitionTimerTwo;
        public int roundsLeft;
        public string playerName;

        // Constructor
        public S_ConfirmPlayer(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            transitionTimerOne = 0;
            transitionTimerTwo = 0;
            roundsLeft = parentManager.gameOptions.numRounds - parentManager.round.currRound + 1;
            playerName = parentManager.round.currPlayer.type.ToString().ToUpper();

        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            if(transitionTimerOne < 60) 
                transitionTimerOne++; 
            else
            {
                if (transitionTimerTwo < 60)
                    transitionTimerTwo++;
                else
                {
                    S_RollDice rollDice = new S_RollDice(parentManager, 0, 0);
                    parentManager.AddStateQueue(rollDice);
                    this.flagForDeletion = true;
                }
            }
            
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();
            if(transitionTimerOne < 60)
            {
                // Box 1 covering top half
                Vector2 boxOne = new Vector2(0, 0 - (transitionTimerOne * 12));
                sb.Draw(this.parentManager.game.confirmPlayerFade, new Rectangle((int)boxOne.X, (int)boxOne.Y, MGP_Constants.SCREEN_WIDTH, MGP_Constants.SCREEN_MID_Y), Color.White);

                // Box 2 covering bottom half
                Vector2 boxTwo = new Vector2(0, MGP_Constants.SCREEN_MID_Y + (transitionTimerOne * 12));
                sb.Draw(this.parentManager.game.confirmPlayerFade, new Rectangle((int)boxTwo.X, (int)boxTwo.Y, MGP_Constants.SCREEN_WIDTH, MGP_Constants.SCREEN_MID_Y), Color.White);
            }
            else
            {
                // draw text 

                // Draw text for last 3 rounds
                string text = ""; 
                if (roundsLeft == 1)
                {
                    text = "LAST TURN"; 
                }
                else if(roundsLeft <= 3)
                {
                    text = "LAST" + roundsLeft.ToString() + " TURNS";
                }
                Vector2 boldTextPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 75), text, this.parentManager.game.ft_confirmPlayer_Bold);
                sb.DrawString(this.parentManager.game.ft_confirmPlayer_Bold, text, boldTextPos, Color.White);

                Vector2 textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 75), text, this.parentManager.game.ft_confirmPlayer);
                sb.DrawString(this.parentManager.game.ft_confirmPlayer, text, textPos, Color.Black);

                Vector2 sm_textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 75), text, this.parentManager.game.ft_confirmPlayer_sm);
                sb.DrawString(this.parentManager.game.ft_confirmPlayer_sm, text, sm_textPos, Color.Gold);


                // Draw text for players name
                text = playerName + " START";
                boldTextPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y), text, this.parentManager.game.ft_confirmPlayer_Bold);
                sb.DrawString(this.parentManager.game.ft_confirmPlayer_Bold, text, boldTextPos, Color.White);

                textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y), text, this.parentManager.game.ft_confirmPlayer);
                sb.DrawString(this.parentManager.game.ft_confirmPlayer, text, textPos, Color.Black);

                sm_textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y), text, this.parentManager.game.ft_confirmPlayer_sm);
                sb.DrawString(this.parentManager.game.ft_confirmPlayer_sm, text, sm_textPos, Color.Gold);

            }

            sb.End();
        }// End Draw

       
    }
}
