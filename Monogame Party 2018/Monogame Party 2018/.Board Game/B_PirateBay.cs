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


    // DEBUG
    Vector2 SCREEN_MID = new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y);
    Vector2 DEBUG_POS = new Vector2(MGP_Constants.SCREEN_MID_X - 32, MGP_Constants.SCREEN_MID_Y - 32);


    // Collection of Spaces:
    public List<E_Space> spaces;
    public E_Dice testDice;

    // Constructor:
        public B_PirateBay(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos) {

      // Create testDice
      testDice = new E_Dice(this, parentManager.game.spr_testDice, 25, 150, 10);

      // initialize list of spaces:
      spaces = new List<E_Space>();

      // Placeholder
      E_Space space;

      // Create Spaces:
      space = new E_Space(this, parentManager.game.piece_blue64, 200, 200, (int)E_Space.types.BLUE);
      spaces.Add(space);

      space = new E_Space(this, parentManager.game.piece_red64, 500, 800, (int)E_Space.types.RED);
      spaces.Add(space);

      space = new E_Space(this, parentManager.game.piece_green64, 1200, 900, (int)E_Space.types.BLUE);
      spaces.Add(space);

      cameraProperties.setX(400);
      cameraProperties.setY(500);
    }



    // ** UPDATE **
    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);


      // DEBUG move the camera around!
      float speed = 7;
      if (km.KeyDown(Keys.D)) { cameraProperties.incX(speed); }
      if (km.KeyDown(Keys.A)) { cameraProperties.incX(-speed); }
      if (km.KeyDown(Keys.W)) { cameraProperties.incY(-speed); }
      if (km.KeyDown(Keys.S)) { cameraProperties.incY(speed); }
      // END DEBUG


      // Camera is fixated on CameraProperties object:
      this.parentManager.game.cameraObject.LookAt(cameraProperties.getPos());

      // testDice update
      testDice.Update(gameTime, ks);

    }


    // ** DRAW **
    public override void Draw(GameTime gameTime) {
      base.Draw(gameTime);

            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin(transformMatrix: this.parentManager.game.cameraObject.GetViewMatrix());

            // Background
            sb.Draw(this.parentManager.game.bg_pirateBay, new Vector2(xPos, yPos), Color.White);

            // Pieces:
            foreach (E_Space space in spaces) {
              sb.Draw(space.sprite, space.getPos(), Color.White);
            }

            // Test dice
            sb.Draw(testDice.sprite, testDice.getPos(), Color.White);
            testDice.Draw(gameTime);

            // End drawing in Camera:
            sb.End();



            // DRAW UI ON WHOLE SCREEN:
            sb.Begin();
              // ** DEBUG CENTER PIECE WITH X AND Y DRAWN TO SCREEN: **
              sb.Draw(this.parentManager.game.piece_green64, DEBUG_POS, Color.White);
              string textDebugX = "X: " + parentManager.game.cameraObject.ScreenToWorld(DEBUG_POS.X).ToString();
              sb.DrawString(this.parentManager.game.ft_mainMenuFont, textDebugX, DEBUG_POS, Color.White);

            sb.End();

    }
  }





}
