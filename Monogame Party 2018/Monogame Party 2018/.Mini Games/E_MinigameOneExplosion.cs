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
    public class E_MinigameOneExplosion : Entity
    {
        public int resize;
        public bool isActive;
        // Contructor
        public E_MinigameOneExplosion(State parentState)
        {
            this.parentState = parentState;
            this.sprite = parentState.parentManager.game.minigame_one_explosion;
            this.pos.X = MGP_Constants.SCREEN_MID_X - 100;
            this.pos.Y = 100;
            this.resize = 0;

            this.isActive = false;
            this.visible = false;
        }
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // Increase size
            if(resize < 270)
            {
                resize+=6;
            }
            // Reset 
            else
            {
                resize = 0;
                this.active = false;

            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch sb = this.parentState.parentManager.game.spriteBatch;
            sb.Begin();
            sb.Draw(this.sprite, new Rectangle((int)this.pos.X - (resize / 2), (int)this.pos.Y - (resize / 2), 205 + resize, 205 + resize), Color.White);
            sb.End();
        }
    }
}
