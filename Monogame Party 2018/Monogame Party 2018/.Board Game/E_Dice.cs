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
        public const int ROLL_SPEED = 2;
        public int pipCount;
        public float timer = 0f;

        // Constructor:
        public E_Dice(State parentState, Texture2D sprite, int x, int y, int pips = 10) : base(parentState, sprite, x, y)
        {
            this.pipCount = pips;
        }

        // Member methods
        public void Roll()
        {

        }

        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                // TODO:  RESET THE TIMER WHEN IT REACHES 10
                // TODO:  CHANGE SPEED AT WHICH DICE UPDATES
                if (timer > pipCount) { timer = 0f; }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch sb = this.parentState.parentManager.game.spriteBatch;
            sb.Begin();
            SpriteFont pipFont = this.parentState.parentManager.game.ft_menuDescriptionFont;
            Vector2 dicePosition = this.getPos();
            // TODO:  FIX POSITION ---------------------------------------------------------------------------------------
            sb.DrawString(pipFont, Math.Ceiling(timer).ToString(), new Vector2(MGP_Constants.SCREEN_MID_X + 250, MGP_Constants.SCREEN_MID_Y - 125), Color.Black);
            sb.End();
        }
    }
}
