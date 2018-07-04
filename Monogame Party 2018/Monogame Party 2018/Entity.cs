using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {

  public abstract class Entity {

    // data
    State state;
    public double x;
    public double y;
    public bool active;
    public int id;

    // drawing
    public bool visible;

    // constructor:
    public Entity(State creator, int xPos, int yPos, bool is_visible, int id) {

      state = creator;
      x = xPos;
      y = yPos;
      visible = is_visible;

      // Default values:
      active = true;
    }

    // VIRTUAL Functions (will be overridden)
    public virtual void Update(GameTime gametime, KeyboardState ks) { }
    public virtual void Draw(GameTime gametime) { }

  }
}
