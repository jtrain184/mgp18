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

    // A an Entity that can represent dice
    public class E_Dice : Entity
    {
        // Member variables
        public int pipCount;
        public int diceRoll = 1;
        public const int MAX_DICE_ROLL = 9;    // Set the max number you can roll on a dice

        // Constructor:
        public E_Dice(State parentState, Texture2D sprite, int x, int y, int pips = 1000) : base(parentState, sprite, x, y)
        {
            this.pipCount = pips;
            this.pos = new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 150);

        }

        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // For random sequence
            // diceRoll = parentState.parentManager.random.Next(1, Max_DICE_ROLL);

            // For sequential sequence
            if (diceRoll >= MAX_DICE_ROLL)
                diceRoll = 1;
            else
                diceRoll++;


            this.pos = parentState.parentManager.round.currPlayer.currSpace.pos - new Vector2(0, 150);

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch sb = this.parentState.parentManager.game.spriteBatch;
            sb.Begin();
            SpriteFont pipFont = this.parentState.parentManager.game.ft_playerUIdata;
            //Vector2 dicePosition = this.getPos();

            // TODO:  FIX POSITION ---------------------------------------------------------------------------------------
            Vector2 dicePos = new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 150);

            sb.Draw(this.parentState.parentManager.game.spr_diceBox, dicePos, Color.White);
            sb.DrawString(pipFont, diceRoll.ToString(), new Vector2(dicePos.X + 20, dicePos .Y + 20), Color.DarkBlue);
            
            sb.End();
        }
    }
}
