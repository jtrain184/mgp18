using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_LandAction : State
    {
        public int numCoins;
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
            if(spaceType == Entity.typeSpace.blue)
            {
                // Add a random amount of coins between 3 - 15 to curr player
                numCoins = creator.random.Next(3, 15);
                creator.round.currPlayer.coins += numCoins;
                landAction.text = "+ " + numCoins.ToString();
            }

            // Landed on a red space
            if (spaceType == Entity.typeSpace.red)
            {
                // Subtract a random amount of coins between 3 - 7 to curr player
                numCoins = creator.random.Next(3, 15);
                creator.round.currPlayer.coins -= numCoins;
                landAction.text = "- " + numCoins.ToString();
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
            // make the items move up for blue spaces
            else if (spaceType == Entity.typeSpace.blue)
                moveYPos--;
            // make the items move down for red spaces
            else if (spaceType == Entity.typeSpace.red)
                moveYPos ++;
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();
            Vector2 menuItemPos = new Vector2(landAction.xPos, landAction.yPos + moveYPos);
            Vector2 menuTextPos = menuItemPos + new Vector2(30, -25);     // Draw text to the right of object
            sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)menuItemPos.X - 50 / 2, (int)menuItemPos.Y - 50 / 2, 50, 50), Color.White);
            if(spaceType == Entity.typeSpace.blue)
                sb.DrawString(this.parentManager.game.ft_mainMenuFont, landAction.text, menuTextPos, Color.Blue);
            if (spaceType == Entity.typeSpace.red)
                sb.DrawString(this.parentManager.game.ft_mainMenuFont, landAction.text, menuTextPos, Color.Red);

            sb.End();
        }
    }
}
