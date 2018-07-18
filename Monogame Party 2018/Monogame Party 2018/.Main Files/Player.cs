using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {
  public class Player {

    public bool isHuman;
    public E_Meeple meeple;

    public int coins;
    public int stars;

    public int place; // such as first place, second place, etc..

    // Other interesting information tracked throughout gameplay:
    public int totalMiniGameWins;
    public int totalMiniGameLosses;

    public int totalRedSpaceLands;
    public int totalBlueSpaceLands;
    public int totalChanceSpaceLands;
    public int totalSpecialSpaceLands;

    public int totalCoinsGained;
    public int totalCoinsLost;


    public Player(int type) {

    }


  }
}
