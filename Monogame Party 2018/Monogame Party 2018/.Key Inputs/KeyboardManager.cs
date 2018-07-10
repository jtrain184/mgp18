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
  }
}
