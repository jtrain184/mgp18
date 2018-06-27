using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {


  enum depth {
    DEP_SPLASH_SCREEN = 50,
    DEP_MAIN_MENU = 100,
    DEP_BOARD = 300,
    DEP_BOARD_UI = 400,
    DEP_DICE = 500,
    DEP_MINIGAME_INSTRUCTIONS = 600,
    DEP_MINIGAME = 700,
    DEP_PAUSE = 900
  }



  public abstract class State {

    State parent;
    List<State> children;

    public bool active; // whether or not step function is run
    public bool visible; // whether or not draw function is run
    public bool topLayer; // Listen for keyboard input?
    public int player;

    // CONSTRUCTOR:
    public State (int playerIndex, bool active, bool visible) {
      this.player = playerIndex;
      this.active = active;
      this.visible = visible;
    }


    // VIRTUAL Update function
    public virtual void Update(GameTime gameTime, KeyboardState ks) {



    }


    // VIRTUAL Draw function
    public virtual void Draw(GameTime gameTime) {

    }


  }

}
