using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {

  // A "Space" on the "Board"
  public class E_Space : Entity {

    public enum types {
      BLUE = 0,
      RED
    }

    // Member variables
    public int type;

    // Constructor:
    public E_Space(State parentState, Texture2D sprite, int x, int y, int type) : base(parentState, sprite, x, y) {

      this.type = type;
    }



    public override void Update(GameTime gametime, KeyboardState ks) {

    }


    public override void Draw(GameTime gametime) {

    }


  }
}
