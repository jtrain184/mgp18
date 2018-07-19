using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {

  // A "Space" on the "Board"
  public class E_Space : Entity {

    // Member variables
    public Entity.typeSpace type;
    public List<E_Space> spacesAhead;
    public List<E_Space> spacesBehind;


    // Constructor:
    public E_Space(State parentState, Vector2 pos, Entity.typeSpace type) : base(parentState, pos) {
      this.type = type;

      spacesAhead = new List<E_Space>();
      spacesBehind = new List<E_Space>();

      // Get graphic based on type:
      switch (type) {
        case Entity.typeSpace.blue:
          this.sprite = parentState.parentManager.game.piece_blue64;
          break;

        case Entity.typeSpace.red:
          this.sprite = parentState.parentManager.game.piece_red64;
          break;

        case Entity.typeSpace.chance:
          this.sprite = parentState.parentManager.game.piece_white64;
          break;

        case Entity.typeSpace.bonus:
          this.sprite = parentState.parentManager.game.piece_purple64;
          break;

        case Entity.typeSpace.star:
          this.sprite = parentState.parentManager.game.piece_green64; // TODO: make a star space sprite
          break;

        case Entity.typeSpace.invisible:
          this.sprite = parentState.parentManager.game.noSprite;
          this.visible = false;
          break;

        default:
          this.sprite = parentState.parentManager.game.noSprite;
          break;
      }


      this.spriteWidth = sprite.Width;
      this.spriteHeight = sprite.Height;

    }




    void addAhead(E_Space newSpace) { this.spacesAhead.Add(newSpace); }
    void addBehind(E_Space newSpace) { this.spacesBehind.Add(newSpace); }
    void removeAhead(E_Space newSpace) { this.spacesAhead.Remove(newSpace); }
    void removeBehind(E_Space newSpace) { this.spacesBehind.Remove(newSpace); }

    public void assignSpaces(E_Space prev) {
      prev.addAhead(this);
      this.addBehind(prev);
    }


    // Update:
    public override void Update(GameTime gameTime, KeyboardState ks) { }


    // Draw:
    public override void Draw(GameTime gameTime) { }


  }
}
