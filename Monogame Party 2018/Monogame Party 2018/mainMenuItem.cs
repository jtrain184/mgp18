using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Party_2018 {
  public class mainMenuItem {

    public int activeValue;
    public String text;
    public float xPos;
    public float yPos;

    public mainMenuItem(float x, float y, String text, int activeValue) {
      this.xPos = x;
      this.yPos = y;
      this.text = text;
      this.activeValue = activeValue;
    }


  }
}
