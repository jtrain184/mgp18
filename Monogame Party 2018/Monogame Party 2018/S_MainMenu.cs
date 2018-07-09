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

    public enum buttons {
      CASTLE = 0,
      PIRATE,
      SETTINGS,
      ABOUT,
      EXIT
    }

    public List<mainMenuItem> items;

    int currentMenuItem;
    int numItems;

    // Constructor for Main Menu:
    public S_MainMenu(GameStateManager creator, EntityCounter ec, double xPos, double yPos) : base(creator, ec, xPos, yPos) {
      currentMenuItem = (int)buttons.CASTLE;

      items = new List<mainMenuItem>();


      // Game: Castle Land
      items.Add(new mainMenuItem(100, 200, "Castle Land", (int)buttons.CASTLE));

      // Game: Pirate Bay
      items.Add(new mainMenuItem(500, 300, "Pirate Bay", (int)buttons.PIRATE));

      // Settings
      items.Add(new mainMenuItem(100, 500, "Settings", (int)buttons.SETTINGS));

      // About
      items.Add(new mainMenuItem(900, 200, "About", (int)buttons.ABOUT));

      // Exit
      items.Add(new mainMenuItem(900, 500, "Exit", (int)buttons.EXIT));

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

      // Draw Background:
      SpriteBatch sb = this.parentManager.game.spriteBatch;

      sb.Begin();

      sb.Draw(this.parentManager.game.bg_titleScreen, new Vector2(0, 0), Color.White);

      // Draw Buttons -----------------------

      foreach (mainMenuItem item in items) {
        Vector2 loc = new Vector2(item.xPos, item.yPos);

        // Cloud Background:
        sb.Draw(this.parentManager.game.spr_cloudIcon, loc, Color.White);

        // Draw Text:
        sb.DrawString(this.parentManager.game.ft_mainMenuFont, item.text, loc, Color.Red);
      }


      // End drawing:
      sb.End();
    }




  } // end class definition
}
