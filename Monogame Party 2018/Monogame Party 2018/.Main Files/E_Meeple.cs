using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {
  public class E_Meeple : Entity {

        // Member variables:
        public Player.Type type;
        GameStateManager gsm;

        // Constructor
        public E_Meeple(GameStateManager gameStateManager, Vector2 pos, Player.Type type) : base() {
            gsm = gameStateManager;

            switch (type)
            {

                case Player.Type.FRANK:
                    this.sprite = gameStateManager.game.spr_Frank;
                    break;

                case Player.Type.LOUIE:
                    this.sprite = gameStateManager.game.spr_Louie;
                    break;

                case Player.Type.MANFORD:
                    this.sprite = gameStateManager.game.spr_Manford;
                    break;

                case Player.Type.SUE:
                    this.sprite = gameStateManager.game.spr_Sue;
                    break;

                case Player.Type.VELMA:
                    this.sprite = gameStateManager.game.spr_Velma;
                    break;

                case Player.Type.WILBER:
                    this.sprite = gameStateManager.game.spr_Wilber;
                    break;

            }


        } // end constructor

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch sb = gsm.game.spriteBatch;
            sb.Begin();
            
            sb.Draw(this.sprite, this.pos, Color.White);
            sb.End();
        }





    }
}
