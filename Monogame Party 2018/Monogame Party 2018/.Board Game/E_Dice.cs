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

        // Constructor:
        public E_Dice(State parentState, Texture2D sprite, int x, int y, int pips) : base(parentState, sprite, x, y)
        {

            this.pipCount = pips;
        }

        // Member methods
        public void Roll()
        {

        }



        public override void Update(GameTime gametime, KeyboardState ks)
        {

        }


        public override void Draw(GameTime gametime)
        {

        }


    }
}
