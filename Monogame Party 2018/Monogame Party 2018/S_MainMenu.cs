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
      numItems++;

      // Game: Pirate Bay
      items.Add(new mainMenuItem(500, 300, "Pirate Bay", (int)buttons.PIRATE));
      numItems++;

      // Settings
      //items.Add(new mainMenuItem(100, 500, "Settings", (int)buttons.SETTINGS));
      //numItems++;

      // About
      items.Add(new mainMenuItem(900, 200, "About", (int)buttons.ABOUT));
      numItems++;

      // Exit
      items.Add(new mainMenuItem(900, 500, "Exit", (int)buttons.EXIT));
      numItems++;
    }


    // Update:
    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);

      // If this is the top layer, allow moving active menu:
      //if (this.isTopLayer) {


      // Move Menu Selection Up:
      if (parentManager.km.KeyPressed(Keys.Up)) {
          if (currentMenuItem == (numItems - 1)) { currentMenuItem = 0; }
          else { currentMenuItem++; }
      }

      // Move Menu Selection Down:
      if (parentManager.km.KeyPressed(Keys.Down)) {
          if (currentMenuItem == 0) { currentMenuItem = numItems - 1; }
          else { currentMenuItem--; }
      }

      // if (parentManager.km.KeyPressed(Keymap.Select)) {
      if (parentManager.km.KeyPressed(Keys.Enter)) {

        // Go to whatever menu item you chose:
        if (currentMenuItem == (int)buttons.CASTLE) {
          S_MainMenu newMenu = new S_MainMenu(parentManager, parentManager.eCounter, 0, 0);
          parentManager.AddStateQueue(newMenu);
        }


        // choosing exit actually exits the game:



      }


    }

    // Draw:
    public override void Draw(GameTime gameTime) {
      base.Draw(gameTime);

      // Draw Background:
      SpriteBatch sb = this.parentManager.game.spriteBatch;

      sb.Begin();

      sb.Draw(this.parentManager.game.bg_titleScreen, new Vector2(0, 0), Color.White);

      // Draw Buttons -----------------------
      Color tColor;
      int i = 0;
      foreach (mainMenuItem item in items) {
        Vector2 loc = new Vector2(item.xPos, item.yPos);

        // Cloud Background:
        sb.Draw(this.parentManager.game.spr_cloudIcon, loc, Color.White);

        // Draw Text:
        if (i == currentMenuItem)
          tColor = Color.Blue;
        else
          tColor = Color.Red;
        sb.DrawString(this.parentManager.game.ft_mainMenuFont, item.text, loc, tColor);

        i++;
      }


      // End drawing:
      sb.End();
    }




  } // end class definition
}
