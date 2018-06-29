using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {

  class Entity {

    // data
    public int x;
    public int y;
    public bool active;
    public int id;

    // drawing
    public bool visible;

    // constructor:
    public Entity(int xPos, int yPos, bool is_visible, int id) {

      // active by default. Use function to deactivate
      active = true;

      x = xPos;
      y = yPos;
      active = is_visible;
    }

  }
}
