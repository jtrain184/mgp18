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

    State parent;
    State child;

    public bool active; // whether or not step function is run
    public bool visible; // whether or not draw function is run
    public bool isTopLayer; // Listen for keyboard input?
    public int player;
    public bool flagForDeletion; // at end of Update, delete me (sent to manager)

    // List of all Entity objects related to this State
    List<Entity> eList = new List<Entity>();

    // CONSTRUCTOR:
    public State(State parent, int playerIndex, bool active, bool visible) {
      this.parent = parent;
      this.player = playerIndex;
      this.active = active;
      this.visible = visible;
      this.flagForDeletion = false;
    }

    // VIRTUAL Functions (will be overridden)
    public virtual void Update(GameTime gameTime, KeyboardState ks) { }
    public virtual void Draw(GameTime gameTime) { }

  } // end State class
}
