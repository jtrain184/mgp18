using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {

  public class S_MainMenu : State {

    // Constructor for Main Menu:
    public S_MainMenu(GameStateManager creator, int PlayerIndex, EntityCounter ec) : base(creator, PlayerIndex, ec) {


      // Start Button:
      MenuButton start = new MenuButton(this, Constants.SCREEN_CENTER_X, Constants.SCREEN_CENTER_Y, true, ec.takeNumber());
      start.setText("Begin Game");
      addEntity(start);

    }


    // Update:
    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);

      // Only run update code if active:
      if (!this.active)
        return;

      // Code for update main menu here:


    }

    // Draw:
    public override void Draw(GameTime gameTime) {
      base.Draw(gameTime);

      // Only run draw code if visible:
      if (!this.visible)
        return;


      // Code for draw main menu here:
    }




  } // end class definition
}
