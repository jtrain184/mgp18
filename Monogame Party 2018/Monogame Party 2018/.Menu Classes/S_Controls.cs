
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

            // Key Input Sprite
            sb.Draw(this.parentManager.game.key_inputs, new Vector2(0, 0), Color.White);

            string text = "Cancel...Go back";

            Vector2 smTextPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, 675), text, parentManager.game.ft_rollDice_lg);
            sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X - 2, smTextPos.Y), Color.Black);
            sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X + 2, smTextPos.Y), Color.Black);
            sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y - 2), Color.Black);
            sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y + 2), Color.Black);

            sb.DrawString(parentManager.game.ft_rollDice_lg, text, smTextPos, Color.White);

            sb.End();
        }
    }
}
