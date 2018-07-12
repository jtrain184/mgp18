using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {
  public class CameraProperties {

    Vector2 pos;

    public CameraProperties() {
      pos.X = 0;
      pos.Y = 0;
    }

    public void setX(float x) { pos.X = x; }
    public void incX(float x) { pos.X += x; }

    public void setY(float y) { pos.Y = y; }
    public void incY(float y) { pos.Y += y; }

    public void setPos(Vector2 newPos) { pos.X = newPos.X; pos.Y = newPos.Y; }

    public float getX() { return pos.X; }
    public float getY() { return pos.Y; }
    public Vector2 getPos() { return pos; }

  }
}
