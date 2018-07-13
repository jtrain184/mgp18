﻿using System;
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
    State parentState;
    public Vector2 pos;
    public bool active;
    public int id;
    public Texture2D sprite;

    // drawing
    public bool visible;

    // constructor:
    public Entity(State parentState, Texture2D sprite, int x, int y) {

      this.parentState = parentState;
      this.pos.X = x;
      this.pos.Y = y;
      this.id = EntityCounter.takeNumber();
      this.sprite = sprite;

      // Default values:
      this.active = true;
      this.visible = true;
    }

    // VIRTUAL Functions (will be overridden)
    public virtual void Update(GameTime gametime, KeyboardState ks) { }
    public virtual void Draw(GameTime gametime) { }

    // Set:
    public void setX(float x) { pos.X = x; }
    public void setY(float y) { pos.Y = y; }

    public Vector2 getPos() { return pos; }

  }
}
