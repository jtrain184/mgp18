using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Party_2018 {
  public class MenuItem {

    public int activeValue;
    public String text;
    public float xPos;
    public float yPos;


    public MenuItem above;
    public MenuItem right;
    public MenuItem below;
    public MenuItem left;

    public enum MainMenu
    {
        PIRATE = 0,
        MOUNTAIN,
        ABOUT,
        EXIT
    }
    public enum Difficulty
    {
        EASY = 0,
        MEDIUM,
        HARD
        }


        public MenuItem(float x, float y, String text, int activeValue) {
        this.xPos = x;
        this.yPos = y;
        this.text = text;
        this.activeValue = activeValue;
    }


  }
}
