using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {
  public class KeyboardManager {

    public enum playerIndex {
      one,
      two
    }

    public enum action {
      up,
      down,
      left,
      right,

      select,
      cancel,
      pause,

      action_1,
      action_2,
      action_3
    }

    // Member Variables
    KeyboardState kbCur;
    KeyboardState kbPrev;

    // Constructor
    public KeyboardManager() {
      this.kbCur = new KeyboardState();
      this.kbPrev = new KeyboardState();
    }

    public void KeysUpdateCurrent() { this.kbCur = Keyboard.GetState(); }
    public void KeysPushOld() { this.kbPrev = this.kbCur; }

    // Key was just pressed
    public bool KeyPressed(Keys key) {
      return (kbCur.IsKeyDown(key) && !kbPrev.IsKeyDown(key));
    }

    // Key was just released
    public bool KeyReleased(Keys key) {
      return (!kbCur.IsKeyDown(key) && kbPrev.IsKeyDown(key));
    }

    // Is the key currently being HELD down?
    public bool KeyDown(Keys key) { return kbCur.IsKeyDown(key); }

    // Is the key currently not being touched?
    public bool KeyUp(Keys key) { return !kbCur.IsKeyDown(key); }


    // Key was just pressed
    public bool ActionPressed(action a, playerIndex p) {

    // ------- PLAYER ONE PRESSED --------
    if (p == playerIndex.one) {

      switch (a) {

        case action.up:
          return KeyPressed(Keys.Up);

        case action.left:
          return KeyPressed(Keys.Left);

        case action.down:
          return KeyPressed(Keys.Down);

        case action.right:
          return KeyPressed(Keys.Right);

        case action.select:
          return KeyPressed(Keys.Enter); // on numPad

        case action.cancel:
          return KeyPressed(Keys.Decimal); // on numPad

        case action.pause:
          return KeyPressed(Keys.NumPad0); // on numPad

        case action.action_1:
          return KeyPressed(Keys.NumPad1); // on numPad

        case action.action_2:
          return KeyPressed(Keys.NumPad2); // on numPad

        case action.action_3:
          return KeyPressed(Keys.NumPad3); // on numPad

        default:
          return false;
      } // end switch
    } // end player one input check


    // ------- PLAYER TWO PRESSED --------
    else if (p == playerIndex.two) {

      switch (a) {

        case action.up:
          return KeyPressed(Keys.W);

        case action.left:
          return KeyPressed(Keys.A);

        case action.down:
          return KeyPressed(Keys.S);

        case action.right:
          return KeyPressed(Keys.D);

        case action.select:
          return KeyPressed(Keys.E);

        case action.cancel:
          return KeyPressed(Keys.Q);

        case action.pause:
          return KeyPressed(Keys.LeftShift);

        case action.action_1:
          return KeyPressed(Keys.B);

        case action.action_2:
          return KeyPressed(Keys.N);

        case action.action_3:
          return KeyPressed(Keys.M);

        default:
          return false;
      } // end switch
    } // end player two input check

    else
      return false;
    }


  } // end KeyboardManager class
}
