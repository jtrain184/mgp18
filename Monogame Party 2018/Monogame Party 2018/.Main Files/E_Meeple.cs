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
    public int type;

    // Constructor
    public E_Meeple(State parentState, Texture2D sprite, int x, int y, int type) : base(parentState, sprite, x, y) {


      // create the character based on 'type':
      this.type = type;


    } // end constructor




  }
}
