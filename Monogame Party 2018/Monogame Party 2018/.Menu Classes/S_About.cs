using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogame_Party_2018
{
    public class S_About : State
    {
        public List<string> aboutText;
        public List<string> aboutHeader;
        public List<Vector2> boxDim;
        public int index = 0;
        State parentState;

        public S_About(GameStateManager creator, float xPos, float yPos, State parentState) : base(creator, xPos, yPos)
        {
            aboutHeader = new List<string>()
            {
                "The Story",
                "The Features",
                "The Credits"
            };

            aboutText = new List<string>()
            {
                //The Story
                "A Mario Party inspired video board game experience\n" +
                "for up to two human and two to three scripted AI players.\n" +
                "Traverse the dangerous waters and beaches of Pirate Bay!\n" +
                "Collect coins, purchase stars and battle your foes to become\n" +
                "the richest meeple that ever dared set anchor in this menacing harbor!",

                // The Features
                "- Choose from six different characters\n" +
                "- Three different A.I. difficulties\n" +
                "- Three different options for game length\n" +
                "- Choose whether or not to award bonuses at the end of the game\n" +
                "- Alternating selection of mini games to play",

                // The Credits
                "This game was created by Team Caelum, OSU Class of '18\n" +
                "for their capstone project\n\n" +
                "James 'Cam' Abreu\n" +
                "     - Game Engine,\n" +
                "     - Pirate Bay board graphics,\n" +
                "     - All music and SFX custom made,\n" +
                "     - Chance time game\n\n" +
                "Christopher Bugsch\n" +
                "     - Menu System,\n" +
                "     - Moving players/Buying stars,\n" +
                "     - Bomb mini game,\n" +
                "     - End of game results\n\n" +
                "Phillip Jarrett\n" +
                "     - Repository Lead,\n" +
                "     - Rolling dice,\n" +
                "     - Racing mini game,\n" +
                "     - Testing/Debugging\n\n" +
                "A Special Thanks to  Janice Dixon\n" +
                "for creating the games amazing title screen artwork"
            };

            // Box dimensions 
            boxDim = new List<Vector2>()
            {
                new Vector2(700, 350),
                new Vector2(700, 350),
                new Vector2(900, 750)
            };

            this.parentState = parentState;
        }

        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            // Press Cancel Key: Goes back to main menu:
            if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.one))
            {
                parentState.active = true;
                parentState.visible = true;
                this.flagForDeletion = true;
            }

            if (km.ActionPressed(KeyboardManager.action.left, KeyboardManager.playerIndex.all))
            {
                index = (index > 0) ? index - 1 : 0;
            }

            if (km.ActionPressed(KeyboardManager.action.right, KeyboardManager.playerIndex.all))
            {
                index = (index < 2) ? index + 1 : 2;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);



            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();
            sb.Draw(parentManager.game.bg_titleScreen, new Vector2(0, 0), Color.White);

            // Background Box
            Vector2 backgroundBox = new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y);
            sb.Draw(this.parentManager.game.spr_messageBox, new Rectangle((int)backgroundBox.X - (int)boxDim[index].X / 2, (int)backgroundBox.Y - (int)boxDim[index].Y / 2, (int)boxDim[index].X, (int)boxDim[index].Y), new Color(0, 0, 128, 150));

            // Description Header
            Vector2 textHeaderPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X - 1, backgroundBox.Y - ((int)boxDim[index].Y / 2 - 60)), aboutHeader[index], this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, aboutHeader[index], textHeaderPos, Color.Black);

            textHeaderPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X, backgroundBox.Y - ((int)boxDim[index].Y / 2 - 61)), aboutHeader[index], this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, aboutHeader[index], textHeaderPos, Color.Black);

            textHeaderPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X, backgroundBox.Y - ((int)boxDim[index].Y / 2 - 59)), aboutHeader[index], this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, aboutHeader[index], textHeaderPos, Color.Black);

            textHeaderPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X + 1, backgroundBox.Y - ((int)boxDim[index].Y / 2 - 60)), aboutHeader[index], this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, aboutHeader[index], textHeaderPos, Color.Black);

            textHeaderPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X, backgroundBox.Y - ((int)boxDim[index].Y / 2 - 60)), aboutHeader[index], this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, aboutHeader[index], textHeaderPos, Color.White);




            // Description Text
            Vector2 textDesPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X - 1, backgroundBox.Y), aboutText[index], this.parentManager.game.ft_menuDescriptionFont);
            sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, aboutText[index], textDesPos, Color.Black);

            textDesPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X, backgroundBox.Y - 1), aboutText[index], this.parentManager.game.ft_menuDescriptionFont);
            sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, aboutText[index], textDesPos, Color.Black);

            textDesPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X + 1, backgroundBox.Y), aboutText[index], this.parentManager.game.ft_menuDescriptionFont);
            sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, aboutText[index], textDesPos, Color.Black);

            textDesPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X, backgroundBox.Y + 1), aboutText[index], this.parentManager.game.ft_menuDescriptionFont);
            sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, aboutText[index], textDesPos, Color.Black);

            textDesPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X, backgroundBox.Y), aboutText[index], this.parentManager.game.ft_menuDescriptionFont);
            sb.DrawString(this.parentManager.game.ft_menuDescriptionFont, aboutText[index], textDesPos, Color.White);

            if (index < 2)
            {
                string text = ">>";

                Vector2 smTextPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X + (boxDim[index].X / 2 - 40), backgroundBox.Y + (boxDim[index].Y / 2 - 35)), text, parentManager.game.ft_rollDice_lg);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X - 2, smTextPos.Y), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X + 2, smTextPos.Y), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y - 2), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y + 2), Color.Black);

                sb.DrawString(parentManager.game.ft_rollDice_lg, text, smTextPos, Color.White);
            }

            if (index > 0)
            {
                string text = "<<";

                Vector2 smTextPos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X - (boxDim[index].X / 2 - 40), backgroundBox.Y + (boxDim[index].Y / 2 - 35)), text, parentManager.game.ft_rollDice_lg);
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
