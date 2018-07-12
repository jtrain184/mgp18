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

    }



    // ** UPDATE **
    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);


    }


    // ** DRAW **
    public override void Draw(GameTime gameTime) {
      base.Draw(gameTime);

            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();

            // Background
            sb.Draw(this.parentManager.game.bg_pirateBay, new Vector2(xPos, yPos), Color.White);


            // End drawing:
            sb.End();



    }






  }
}
