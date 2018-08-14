
using Microsoft.Xna.Framework;

namespace Monogame_Party_2018
{
    public class E_Glove : Entity
    {

        // ** CONSTRUCTOR **
        public E_Glove(GameStateManager gsm, Vector2 initPos) : base()
        {
            this.sprite = gsm.game.spr_glove;

            this.pos = initPos;

        } // end constructor


    } // end E_Glove class
}
