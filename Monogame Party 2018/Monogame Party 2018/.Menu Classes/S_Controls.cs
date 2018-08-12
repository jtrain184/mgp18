
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_Controls : State
    {
        public const int boxHeight = 750;
        public const int boxWidth = 600;
        public  const string description = "Game Controls\n" + 
            "Player 1 Player 2\n" +
            "Select...Enter/E/SpaceBar Select...Enter\n";


        public S_Controls(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            
        }

        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.all))
            {
                this.flagForDeletion = true;
                parentManager.states[1].active = true;
            }
            base.Update(gameTime, ks);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch sb = this.parentManager.game.spriteBatch;
            sb.Begin();

            // Background Box
            Vector2 backgroundBox = new Vector2(MGP_Constants.SCREEN_MID_X - boxWidth / 2, MGP_Constants.SCREEN_MID_Y - boxHeight / 2);
            sb.Draw(this.parentManager.game.spr_messageBox, new Rectangle((int)backgroundBox.X, (int)backgroundBox.Y, boxWidth, boxHeight), new Color(0, 0, 128, 230));
            // Text
            Vector2 textDesPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X + boxWidth / 2, backgroundBox.Y + boxHeight / 2), description, this.parentManager.game.ft_menuDescriptionFont);
            sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, description, new Vector2(backgroundBox.X + 10, backgroundBox.Y + 50), Color.White);


            sb.End();
        }
    }
}
