using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {

  public abstract class State {

    public bool active;
    public bool visible;
    public int depth;

    // VIRTUAL Update function
    public virtual void Update(GameTime gameTime) {

    }


    // VIRTUAL Draw function
    public virtual void Draw(GameTime gameTime) {

    }


  }

}
