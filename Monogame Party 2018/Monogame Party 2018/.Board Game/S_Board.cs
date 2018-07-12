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

    // Constructor:
    public S_Board(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos) {
      cameraProperties = new CameraProperties();
    } // end constructor






  }
}
