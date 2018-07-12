using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {
  public class B_PirateBay : S_Board {




    // Constructor:
    public B_PirateBay(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos) {

      // Create Spaces:
      E_Space space00 = new E_Space(this, parentManager.game.spr_cloudIcon, 200, 200, (int)E_Space.types.BLUE);

      cameraProperties.setX(400);
      cameraProperties.setY(500);

    }



    // ** UPDATE **
    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);


      // DEBUG move the camera around!
      float speed = 7;
      if (km.KeyDown(Keys.D)) { cameraProperties.incX(speed); }
      if (km.KeyDown(Keys.A)) { cameraProperties.incX(-speed); }
      if (km.KeyDown(Keys.W)) { cameraProperties.incY(-speed); }
      if (km.KeyDown(Keys.S)) { cameraProperties.incY(speed); }
      // END DEBUG

      if (km.ActionPressed(KeyboardManager.action.up, KeyboardManager.playerIndex.one)) {
      }



      // Camera is fixated on CameraProperties object:
      this.parentManager.game.cameraObject.LookAt(cameraProperties.getPos());

    }


    // ** DRAW **
    public override void Draw(GameTime gameTime) {
      base.Draw(gameTime);

            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin(transformMatrix: this.parentManager.game.cameraObject.GetViewMatrix());

            // Background
            sb.Draw(this.parentManager.game.bg_pirateBay, new Vector2(xPos, yPos), Color.White);


            // End drawing:
            sb.End();



    }






  }
}
