using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_About : State
    {
        MenuItem menuDescription;
        MenuItem aboutText;

        public S_About(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            // Menu Description
            menuDescription = new MenuItem(this.xPos + 650, this.yPos + 650, "Press [Backspace] to return to the main menu", -1);

            aboutText = new MenuItem(this.xPos + 650, this.yPos + 350, "Monogame Party is a party game where up to four " + System.Environment.NewLine +
               "players compete in a boardgame containing minigames.  " + System.Environment.NewLine 
               + "Up to two of the players are human controlled, while " + System.Environment.NewLine
               + "the rest are controlled by the computer.", -1);
        }

        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            // Press Cancel Key: Goes back to main menu:
            if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.one))
            {
                S_MainMenu playerCountMenu = new S_MainMenu(parentManager, 0, 0);
                parentManager.AddStateQueue(playerCountMenu);
                this.flagForDeletion = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

   

            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();

            sb.Draw(this.parentManager.game.bg_titleScreen, new Vector2(xPos, yPos), Color.White);

            Vector2 aboutCloudPos = new Vector2(aboutText.xPos, aboutText.yPos);
           // Vector2 aboutCloudPos = new Vector2(aboutText.xPos - 340 / 2, aboutText.yPos - 220 / 2);
            Vector2 textPos = CenterString.getCenterStringVector(aboutCloudPos, aboutText.text, this.parentManager.game.ft_menuDescriptionFont);

            // Draw About Text in a cloud
            sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)aboutCloudPos.X - 650 / 2, (int)aboutCloudPos.Y - 250 / 2, 650, 250), Color.White);
            sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, aboutText.text, textPos, Color.Black);          

            // Draw the Menu description cloud wider
            Vector2 menuItemPos = new Vector2(menuDescription.xPos, menuDescription.yPos);
            Vector2 menuTextPos = CenterString.getCenterStringVector(menuItemPos, menuDescription.text, this.parentManager.game.ft_menuDescriptionFont);
            sb.Draw(this.parentManager.game.spr_cloudIcon, new Rectangle((int)menuDescription.xPos - 600 / 2, (int)menuDescription.yPos - 140 / 2, 600, 140), Color.White);
            sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, menuDescription.text, menuTextPos, Color.Black);

            sb.End();
        }

    }
}
