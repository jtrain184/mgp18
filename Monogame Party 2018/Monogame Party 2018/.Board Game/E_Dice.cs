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
        //public const int ROLL_SPEED = 20;
        public int pipCount;
        //public float timer = 0F;
        public int diceRoll = 1;
        public const int MAX_DICE_ROLL = 10;    // Set the max number you can roll on a dice

        // Constructor:
        public E_Dice(State parentState, Texture2D sprite, int x, int y, int pips = 1000) : base(parentState, sprite, x, y)
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

            // For random sequence
            // diceRoll = parentState.parentManager.random.Next(1, Max_DICE_ROLL);

            // For sequential sequence
            if (diceRoll >= MAX_DICE_ROLL)
                diceRoll = 1;
            else
                diceRoll++;
            
            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch sb = this.parentState.parentManager.game.spriteBatch;
            sb.Begin();
            SpriteFont pipFont = this.parentState.parentManager.game.ft_menuDescriptionFont;
            Vector2 dicePosition = this.getPos();
            // TODO:  FIX POSITION ---------------------------------------------------------------------------------------
            sb.DrawString(pipFont, diceRoll.ToString(), new Vector2(MGP_Constants.SCREEN_MID_X + 250, MGP_Constants.SCREEN_MID_Y - 125), Color.Black);
            sb.End();
        }
    }
}
