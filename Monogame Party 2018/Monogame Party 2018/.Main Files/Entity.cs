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

    public enum typeSpace {
      blue = 0,
      red,
      chance,
      bonus,
      star,
      invisible
    }

    // data
    public State parentState;
    public Vector2 pos;
    public bool active;
    public int id;
    public Texture2D sprite;
    public int spriteWidth;
    public int spriteHeight;

    // drawing
    public bool visible;

    // constructor:
    public Entity(State parentState, Texture2D sprite, int x, int y) {

      this.parentState = parentState;
      this.pos.X = x;
      this.pos.Y = y;
      this.id = EntityCounter.takeNumber();
      this.sprite = sprite;
      this.spriteWidth = sprite.Width;
      this.spriteHeight = sprite.Height;

      // Default values:
      this.active = true;
      this.visible = true;
    }


    // shortened constructor (used for Entities such as E_Space:
    public Entity(State parentState, Vector2 pos) {

      this.parentState = parentState;
      this.pos = pos;
      this.id = EntityCounter.takeNumber();
      this.sprite = parentState.parentManager.game.noSprite; // default sprite
      this.spriteWidth = sprite.Width;
      this.spriteHeight = sprite.Height;

      // Default values:
      this.active = true;
      this.visible = true;
    }

   public Entity()
        {
            // Default values:
            this.active = true;
            this.visible = true;
        }




    // VIRTUAL Functions (will be overridden)
    public virtual void Update(GameTime gameTime, KeyboardState ks) { }
    public virtual void Draw(GameTime gameTime) { }

    // Set:
    public void setX(float x) { pos.X = x; }
    public void setY(float y) { pos.Y = y; }

    public Vector2 getPos() { return pos; }
    public Vector2 getPosCenter() { return new Vector2(pos.X - spriteWidth / 2, pos.Y - spriteHeight / 2); }
 

    }
}
