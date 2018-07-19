﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {
  public class B_PirateBay : S_Board {

    int BOARD_WIDTH = 2400;
    int BOARD_HEIGHT = 1800;

    int TILE_WIDTH = 100;
    int TILE_HEIGHT = 100;
    int NUM_COLUMNS = 24;
    int NUM_ROWS = 18;

    E_Space startingSpace;
    E_Space finalSpace;

    // Get a Vector 2 based on a tile (top left corner or center):
    public Vector2 GetTilePosOrigin(int column, int row) { return new Vector2((float)TILE_WIDTH * column, (float)TILE_HEIGHT * row); }
    public Vector2 GetTilePosCenter(int column, int row) { return new Vector2((float)TILE_WIDTH * column + TILE_WIDTH / 2, (float)TILE_HEIGHT * row + TILE_HEIGHT / 2); }
    // Get a simple x or y float based on a tile (top left corner or center):
    public int GetColXOrigin(int column) { return TILE_WIDTH * column; }
    public int GetColXCenter(int column) { return TILE_WIDTH * column + TILE_WIDTH / 2; }
    public int GetRowYOrigin(int row) { return TILE_HEIGHT * row; }
    public int GetRowYCenter(int row) { return TILE_HEIGHT * row + TILE_HEIGHT / 2; }

    // DEBUG
    Vector2 SCREEN_MID = new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y);
    Vector2 DEBUG_POS = new Vector2(MGP_Constants.SCREEN_MID_X - 32, MGP_Constants.SCREEN_MID_Y - 32);
    Vector2 DEBUG_TEXT_LINE1 = new Vector2(MGP_Constants.SCREEN_MID_X - 32, MGP_Constants.SCREEN_MID_Y - 64);
    Vector2 DEBUG_TEXT_LINE2 = new Vector2(MGP_Constants.SCREEN_MID_X - 32, MGP_Constants.SCREEN_MID_Y - 32);


    // Collection of Spaces:
    public List<E_Space> spaces;
    public E_Dice testDice;

    // Constructor:
        public B_PirateBay(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos) {

      // Create testDice
      testDice = new E_Dice(this, parentManager.game.spr_testDice, 25, 150, 10);

      // initialize list of spaces:
      spaces = new List<E_Space>();

      // position camera starting location:
      cameraProperties.setX(400);
      cameraProperties.setY(500);


      // Create Spaces:
      E_Space firstSpace;
      E_Space lastSpace;
      E_Space curSpace;
      E_Space prevSpace;

      // STARTING WITH BOTTOM RIGHT, MOVING LEFT
      // Bottom right space:
      curSpace = new E_Space(this, GetTilePosCenter(21, 16), Entity.typeSpace.blue);
      spaces.Add(curSpace);
      this.startingSpace = curSpace; // STARTING SPACE
      firstSpace = curSpace; // FIRST SPACE
      prevSpace = curSpace;

      // <<---- MOVING LEFT NOW: ----->>
      // 20, 16
      curSpace = new E_Space(this, GetTilePosCenter(20, 16), Entity.typeSpace.blue);
      spaces.Add(curSpace); // add to overall list
      curSpace.assignSpaces(prevSpace);
      prevSpace = curSpace;

      // 19, 16
      curSpace = new E_Space(this, GetTilePosCenter(19, 16), Entity.typeSpace.blue);
      spaces.Add(curSpace); // add to overall list
      curSpace.assignSpaces(prevSpace);
      prevSpace = curSpace;

      // 17, 16
      curSpace = new E_Space(this, GetTilePosCenter(17, 16), Entity.typeSpace.blue);
      spaces.Add(curSpace); // add to overall list
      curSpace.assignSpaces(prevSpace);
      prevSpace = curSpace;

      // 16, 15
      curSpace = new E_Space(this, GetTilePosCenter(16, 15), Entity.typeSpace.blue);
      spaces.Add(curSpace); // add to overall list
      curSpace.assignSpaces(prevSpace);
      prevSpace = curSpace;

      // 15, 15

      // 14, 15

      // 12, 15

      // 12, 16

      // 10, 16

      // 7, 16

      // 6, 16 CHANCE TIME

      // <<---- MOVING UP NOW: ----->>
      // 6, 15

      // 12, 13

      // 12, 10

      // 12, 9

      // 4, 9

      // 3, 9

      // 3, 8

      // 3, 7

      // 3, 3

      // 3, 2


      // <<---- MOVING RIGHT NOW: ----->>
      // 4, 1

      // 5, 1

      // 7, 1

      // 8, 2

      // 9, 2

      // 10, 3

      // 12, 3

      // 13, 3

      // 14, 3

      // 16, 3

      // 17, 4

      // 18, 4



      // <<---- MOVING DOWN NOW: ----->>
      // 19, 5

      // 19, 7

      // 19, 8

      // 19, 10

      // 19, 11

      // 19, 13

      // 21, 13

      // 22, 13

      // 22, 15 FINAL PIECE
      curSpace = new E_Space(this, GetTilePosCenter(22, 15), Entity.typeSpace.blue);
      spaces.Add(curSpace); // add to overall list
      curSpace.assignSpaces(prevSpace);
      this.finalSpace = curSpace; // FINAL SPACE

      // Finally, link up last space and first space:
      this.startingSpace.assignSpaces(this.finalSpace);

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



      // DEBUG: pressing 7 will print the Space list to the console:
      if (km.KeyPressed(Keys.Y)) {
        foreach (E_Space space in spaces) {
          Console.WriteLine("Space @ " + space.pos.X.ToString() + ", " + space.pos.Y.ToString());
          Console.WriteLine("Next Pieces:");
          foreach (E_Space reference in space.spacesAhead) { Console.WriteLine("    " + reference.pos.X.ToString() + ", " + reference.pos.Y.ToString()); }
          Console.WriteLine("Prev Pieces:");
          foreach (E_Space reference in space.spacesBehind) { Console.WriteLine("    " + reference.pos.X.ToString() + ", " + reference.pos.Y.ToString()); }
          Console.WriteLine("------------------------\n");
        }
      }



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
              if (space.visible)
                sb.Draw(space.sprite, space.getPosCenter(), Color.White);
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
              string textDebugX = "X: " + parentManager.game.cameraObject.ScreenToWorld(SCREEN_MID).X.ToString();
              string textDebugY = "Y: " + parentManager.game.cameraObject.ScreenToWorld(SCREEN_MID).Y.ToString();
              sb.DrawString(this.parentManager.game.ft_mainMenuFont, textDebugX, DEBUG_TEXT_LINE1, Color.White);
              sb.DrawString(this.parentManager.game.ft_mainMenuFont, textDebugY, DEBUG_TEXT_LINE2, Color.White);

            sb.End();

    }
  }





}
