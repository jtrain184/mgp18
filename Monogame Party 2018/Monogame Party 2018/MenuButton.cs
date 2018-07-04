using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {

  public class MenuButton : Entity {

    public string text;

    // Constructor for Main Menu:
    public MenuButton(State creator, int xPos, int yPos, bool is_visible, int id) :
      base(creator, xPos, yPos, is_visible, id) {

      // Default values:
      text = "Unset text";
    }

    public void setText(string newText) {
      text = newText;
    }






  } // end class definition
}
