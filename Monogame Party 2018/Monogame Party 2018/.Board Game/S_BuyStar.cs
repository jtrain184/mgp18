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
    public class S_BuyStar : State
    {
        public int moveYPos = 0; // for animation
        public bool buystar = false;
        public bool broke;
        public bool startAnimation = false;
        public int AI_Timer = 0;
        MenuItem buyStarPrompt;

        public S_BuyStar(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            buyStarPrompt = new MenuItem(MGP_Constants.SCREEN_MID_X - 300, MGP_Constants.SCREEN_MID_Y - 75, "", 0);
            // Player does not have enough coins to buy star
            if (parentManager.round.currPlayer.coins < 20)
            {
                this.broke = true;
                buyStarPrompt.text = "Boo Hoo!\nYou don't have enough Coins.\nCome back when you have at least 20 coins.";
            }
            else
            {
                
                buystar = true;
                // award star and deduct coins
                parentManager.round.currPlayer.coins -= 20;
                parentManager.round.currPlayer.stars++;
 
                buyStarPrompt.text = "You finally made it!\nYou get a star!";
                this.broke = false;
            }
        }

        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // Buy the star Or acknowledging they suck and cant buy the star, either way move on
            if (parentManager.km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.all) || AI_Timer > 60 || moveYPos > 90)
            {
                // start the animation for getting a star
                if(buystar)
                    startAnimation = true;

                // Finished star animation or couldnt buy star
                if (moveYPos > 90 || !buystar)
                {
                    // Move the star to new space
                    if(buystar)
                    {
                        MGP_Tools.Assign_Star(parentManager.boardGame);
                        parentManager.round.currPlayer.currSpace.type = Entity.typeSpace.chance;
                    }

                    this.flagForDeletion = true;
                    // Start moving the player again
                    parentManager.states.Find(s => s.GetType() == typeof(S_MovePlayer)).active = true;
                }

            }
            // If player is buying star, start animation for buying star 
            if (startAnimation)
                moveYPos++;

            // If current play is com, increase AI_Timer
            if(!parentManager.round.currPlayer.isHuman)
                AI_Timer++;
         
           
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();

            
            // buying star
            if (startAnimation)
            {
                // Draw the star starting dead center then moving up
                Vector2 starPos = new Vector2(MGP_Constants.SCREEN_MID_X - 100, MGP_Constants.SCREEN_MID_Y - 100 - moveYPos);
                sb.Draw(this.parentManager.game.spr_star, new Rectangle((int)starPos.X, (int)starPos.Y, 200, 200), Color.White);


                // draw the coins below the star moving down
                Vector2 coinPos = new Vector2(MGP_Constants.SCREEN_MID_X - 50, MGP_Constants.SCREEN_MID_Y + 35 + moveYPos);
                Vector2 coinText = coinPos + new Vector2(55, 0);     // Draw text to the right of object
                sb.Draw(this.parentManager.game.spr_coin, coinPos, Color.White);
                sb.DrawString(this.parentManager.game.ft_mainMenuFont, "- 20", coinText, Color.Red);

            }
            // print message
            else
            {
                Vector2 messageScreen = new Vector2(buyStarPrompt.xPos, buyStarPrompt.yPos);
                Vector2 textPos = CenterString.getCenterStringVector(new Vector2(messageScreen.X + 300, messageScreen.Y + 75), buyStarPrompt.text, this.parentManager.game.ft_menuDescriptionFont);

           
                sb.Draw(this.parentManager.game.spr_messageBox, new Rectangle((int)messageScreen.X, (int)messageScreen.Y, 600, 150), new Color(0, 0, 128, 200));
                sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, buyStarPrompt.text, textPos, Color.White);
    
            }

            if (parentManager.round.currPlayer.isHuman && !startAnimation)
            {
                string text = "Select ... Continue";

                Vector2 smTextPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, 675), text, parentManager.game.ft_rollDice_lg);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X - 2, smTextPos.Y), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X + 2, smTextPos.Y), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y - 2), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y + 2), Color.Black);

                sb.DrawString(parentManager.game.ft_rollDice_lg, text, smTextPos, Color.White);
            }
          
            
            
            sb.End();
        }
    }
}
