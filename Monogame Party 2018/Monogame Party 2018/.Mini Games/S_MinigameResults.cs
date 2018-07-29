using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
namespace Monogame_Party_2018
{
    public class S_MinigameResults : State
    {
        public List<Player> results;
        // Constructor
        public S_MinigameResults(GameStateManager creator, float xPos, float yPos, List<Player> results) : base(creator, xPos, yPos)
        {
            this.results = results;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // Wait till player presses enter to start the next round
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one))
            {
                // Begin next round:
                parentManager.round.currRound++;
                parentManager.round.playerIsPlaying = false;
                this.flagForDeletion = true;
                parentManager.round.active = true;
            }
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();

            // DEBUG IN PROGRESS
            sb.Draw(this.parentManager.game.confirmPlayerFade, new Rectangle(0, 0, MGP_Constants.SCREEN_WIDTH, MGP_Constants.SCREEN_HEIGHT), Color.White);

            string text = "Minigame Results Screen\nUnder Construction\nPress Enter to continue";
            Vector2 textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y), text, this.parentManager.game.ft_confirmPlayer);
            sb.DrawString(this.parentManager.game.ft_confirmPlayer, text, textPos, Color.White);

            sb.End();
        }
    }
    
}
