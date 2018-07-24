using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {
  public class S_Pause : State {


    // ** Member Variables **
    // 'player' member variable inherited from State
    public List<State> statesToPause;
    public KeyboardManager.playerIndex playerWhoPaused;

    // transparent black overlay:
    Rectangle overlay;
    Texture2D overlayTexture;

    Rectangle menuBox;
    Texture2D menuBoxTexture;



    // ** CONSTRUCTOR **
    public S_Pause (GameStateManager creator, float xPos, float yPos, KeyboardManager.playerIndex playerWhoPaused) : base(creator, xPos, yPos) {

      this.playerWhoPaused = playerWhoPaused;

      // Store all other States into a list of states that we will 'pause'
      statesToPause = new List<State>();
      foreach (State s in creator.states) {

        // only change states that are already active (don't tamper with other states)
        if (s.active == true)
          statesToPause.Add(s);
      }


      // Pause those selected states:
      foreach (State s in statesToPause) {
        if (s != this) // don't pause itself
          s.active = false;
      }


      // Create the transparent black background:
      overlayTexture = new Texture2D(parentManager.game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
      Color[] c = new Color[1];
      int transparencyAmount = (int)(255f*0.5f);
      c[0] = Color.FromNonPremultiplied(255, 255, 255, transparencyAmount);
      overlayTexture.SetData<Color>(c);

      // Create the menu background box:
      menuBoxTexture = new Texture2D(parentManager.game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
      transparencyAmount = (int)(255f*0.8f);
      c[0] = Color.FromNonPremultiplied(255, 255, 255, transparencyAmount);
      menuBoxTexture.SetData<Color>(c);

      // Create the shapes:
      overlay = new Rectangle(0, 0, MGP_Constants.SCREEN_WIDTH, MGP_Constants.SCREEN_HEIGHT);
      int BoxHeight = (int)(MGP_Constants.SCREEN_HEIGHT * 0.75f);
      int BoxWidth = (int)(MGP_Constants.SCREEN_WIDTH * 0.4f);
      Vector2 BoxPos = new Vector2(MGP_Constants.SCREEN_MID_X - BoxWidth/2, MGP_Constants.SCREEN_MID_Y - BoxHeight/2);
      menuBox = new Rectangle((int)BoxPos.X, (int)BoxPos.Y, BoxWidth, BoxHeight);
    }




    // ** UPDATE **
    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);

      if ((km.ActionPressed(KeyboardManager.action.pause, this.playerWhoPaused)) ||
          (km.ActionPressed(KeyboardManager.action.cancel, this.playerWhoPaused))) {
        //List<State> statesToPause = new List<State>();

        Console.WriteLine("Player " + ((int)this.playerWhoPaused + 1).ToString() + " resumed the game");

        // Unpause other states now:
        foreach (State s in statesToPause) {
          // Unpause those states:
          s.active = true;
        }


        // Destroy pause menu state
        flagForDeletion = true;
        sendDelay = 2;
      }

    }





    // DRAW
    public override void Draw(GameTime gameTime) {
      base.Draw(gameTime);

      SpriteBatch sb = this.parentManager.game.spriteBatch;


      // Draw relative to the screen, regardless of camera position
      sb.Begin();

      // Draw transparent Black Background box:
      sb.Draw(overlayTexture, overlay, Color.Black);

      // Draw the Menu Box:
      sb.Draw(menuBoxTexture, menuBox, Color.Black);

      // Draw the "Paused" text
      Vector2 middleScreen = new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - MGP_Constants.SCREEN_HEIGHT/3);
      middleScreen = CenterString.getCenterStringVector(middleScreen, "Paused", this.parentManager.game.ft_mainMenuFont);
      sb.DrawString(parentManager.game.ft_mainMenuFont, "Paused", middleScreen, Color.Red);


      sb.End();
    }






  }





}
