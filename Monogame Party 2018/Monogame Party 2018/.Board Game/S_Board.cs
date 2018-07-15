using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {

  public class S_Board : State {

    // Member variables:
    public CameraProperties cameraProperties;
    public GameOptions gameOptions;

    // Constructor:
    public S_Board(GameStateManager creator, float xPos, float yPos, GameOptions Opt) : base(creator, xPos, yPos) {
      cameraProperties = new CameraProperties();

      this.gameOptions = Opt;

      // Create characters:
      foreach (MenuItem.Characters i in this.gameOptions.characters) {


      }


    } // end constructor



    // UPDATE
    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);
    }

    // DRAW
    public override void Draw(GameTime gameTime) {
      base.Draw(gameTime);
    }

  }
}
