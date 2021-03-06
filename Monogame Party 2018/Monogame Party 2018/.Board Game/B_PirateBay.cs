﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018
{
    public class B_PirateBay : S_Board
    {

        static public int BOARD_WIDTH = 2400;
        static public int BOARD_HEIGHT = 1800;

        const int TILE_WIDTH = 100;
        const int TILE_HEIGHT = 100;

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
        Vector2 DEBUG_POS = new Vector2(MGP_Constants.SCREEN_MID_X + 64, MGP_Constants.SCREEN_MID_Y - 16);
        Vector2 DEBUG_TEXT_LINE1 = new Vector2(MGP_Constants.SCREEN_MID_X + 64, 0);
        Vector2 DEBUG_TEXT_LINE2 = new Vector2(MGP_Constants.SCREEN_MID_X + 64, 24);
        Vector2 DEBUG_TEXT_LINE3 = new Vector2(MGP_Constants.SCREEN_MID_X + 64, 48);


        // Collection of Spaces:
        //public List<E_Space> spaces;

        // Constructor:
        public B_PirateBay(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            // --------------------------- Create Spaces: ----------------------------------
            // initialize list of spaces:
            spaces = new List<E_Space>();
            E_Space curSpace;
            E_Space prevSpace;

            // STARTING WITH BOTTOM RIGHT
            // <<---- MOVING LEFT NOW: ----->>
            // Bottom right space:
            curSpace = new E_Space(this, GetTilePosCenter(21, 16), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace);
            this.startingSpace = curSpace; // STARTING SPACE
            prevSpace = curSpace;



            // 20, 16
            // Blue
            curSpace = new E_Space(this, GetTilePosCenter(20, 16), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 19, 16
            // Blue
            curSpace = new E_Space(this, GetTilePosCenter(19, 16), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 17, 16
            // Red
            curSpace = new E_Space(this, GetTilePosCenter(17, 16), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 16, 15
            // Blue
            curSpace = new E_Space(this, GetTilePosCenter(16, 15), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 15, 15
            // Blue
            curSpace = new E_Space(this, GetTilePosCenter(15, 15), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 14, 15
            // Red
            curSpace = new E_Space(this, GetTilePosCenter(14, 15), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 12, 15
            // Blue
            curSpace = new E_Space(this, GetTilePosCenter(12, 15), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 12, 16
            // Blue
            curSpace = new E_Space(this, GetTilePosCenter(12, 16), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 10, 16
            curSpace = new E_Space(this, GetTilePosCenter(10, 16), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 7, 16
            curSpace = new E_Space(this, GetTilePosCenter(7, 16), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 6, 16 CHANCE TIME
            curSpace = new E_Space(this, GetTilePosCenter(6, 16), Entity.typeSpace.chance);
            curSpace.setOverlapPositions(E_Space.direction.left);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;


            // <<---- MOVING UP NOW: ----->>
            // 6, 15
            curSpace = new E_Space(this, GetTilePosCenter(6, 15), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.up);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 6, 13
            curSpace = new E_Space(this, GetTilePosCenter(6, 13), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.up);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 6, 10
            curSpace = new E_Space(this, GetTilePosCenter(6, 10), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.up);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 6, 9
            curSpace = new E_Space(this, GetTilePosCenter(6, 9), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.up);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 4, 9
            curSpace = new E_Space(this, GetTilePosCenter(4, 9), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.up);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 3, 9
            curSpace = new E_Space(this, GetTilePosCenter(3, 9), Entity.typeSpace.chance);
            curSpace.setOverlapPositions(E_Space.direction.up);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 3, 8
            curSpace = new E_Space(this, GetTilePosCenter(3, 8), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.up);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 3, 7
            curSpace = new E_Space(this, GetTilePosCenter(3, 7), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.up);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 3, 3
            curSpace = new E_Space(this, GetTilePosCenter(3, 3), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.up);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 3, 2
            curSpace = new E_Space(this, GetTilePosCenter(3, 2), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.up);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;



            // <<---- MOVING RIGHT NOW: ----->>
            // 4, 1
            curSpace = new E_Space(this, GetTilePosCenter(4, 1), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 5, 1
            curSpace = new E_Space(this, GetTilePosCenter(5, 1), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 7, 1
            curSpace = new E_Space(this, GetTilePosCenter(7, 1), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 8, 2
            curSpace = new E_Space(this, GetTilePosCenter(8, 2), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 9, 2
            curSpace = new E_Space(this, GetTilePosCenter(9, 2), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 10, 3
            curSpace = new E_Space(this, GetTilePosCenter(10, 3), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 12, 3
            curSpace = new E_Space(this, GetTilePosCenter(12, 3), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 13, 3
            curSpace = new E_Space(this, GetTilePosCenter(13, 3), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 14, 3
            curSpace = new E_Space(this, GetTilePosCenter(14, 3), Entity.typeSpace.chance);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 16, 3
            curSpace = new E_Space(this, GetTilePosCenter(16, 3), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 17, 4
            curSpace = new E_Space(this, GetTilePosCenter(17, 4), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 18, 4
            curSpace = new E_Space(this, GetTilePosCenter(18, 4), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.right);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;



            // <<---- MOVING DOWN NOW: ----->>
            // 19, 5
            curSpace = new E_Space(this, GetTilePosCenter(19, 5), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.down);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 19, 7
            curSpace = new E_Space(this, GetTilePosCenter(19, 7), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.down);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 19, 8
            curSpace = new E_Space(this, GetTilePosCenter(19, 8), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.down);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 19, 10
            curSpace = new E_Space(this, GetTilePosCenter(19, 10), Entity.typeSpace.chance);
            curSpace.setOverlapPositions(E_Space.direction.down);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 19, 11
            curSpace = new E_Space(this, GetTilePosCenter(19, 11), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.down);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 19, 13
            curSpace = new E_Space(this, GetTilePosCenter(19, 13), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.down);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 21, 13
            curSpace = new E_Space(this, GetTilePosCenter(21, 13), Entity.typeSpace.blue);
            curSpace.setOverlapPositions(E_Space.direction.down);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 22, 13
            curSpace = new E_Space(this, GetTilePosCenter(22, 13), Entity.typeSpace.red);
            curSpace.setOverlapPositions(E_Space.direction.down);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            prevSpace = curSpace;

            // 22, 15 FINAL PIECE
            curSpace = new E_Space(this, GetTilePosCenter(22, 15), Entity.typeSpace.chance);
            curSpace.setOverlapPositions(E_Space.direction.down);
            spaces.Add(curSpace); // add to overall list
            curSpace.assignSpaces(prevSpace);
            this.finalSpace = curSpace; // FINAL SPACE

            // Finally, link up last space and first space:
            this.startingSpace.assignSpaces(this.finalSpace);

            // Assign the star space to a random space
            MGP_Tools.Assign_Star(this);

            // Assign starting space to all players
            foreach (Player p in this.gameOptions.players)
            {
                p.currSpace = this.startingSpace;
                p.meeple.setPos(p.currSpace.getMeepleLocation());

                // Occupy that space so another meeple doesn't run him/her over:
                p.currSpace.occupySpace(p);
            }
        } // end constructor



        // ** UPDATE **
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // DEBUG: pressing Y will print the Space list to the console:
            if (km.KeyPressed(Keys.Y))
            {
                foreach (E_Space space in spaces)
                {
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
        }


        // ** DRAW **
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin(transformMatrix: this.parentManager.game.cameraObject.GetViewMatrix());

            // Background
            sb.Draw(this.parentManager.game.bg_pirateBay, new Vector2(xPos, yPos), Color.White);

            // Pieces:
            foreach (E_Space space in spaces)
            {
                if (space.visible)
                    sb.Draw(space.sprite, space.getPosCenter(), Color.White);
            }


            // End drawing in Camera:
            sb.End();


            // Draw the meeples

            for (int i = 3; i >= 0; i--) {
              this.gameOptions.players[i].meeple.Draw(gameTime);
            }


            // DRAW UI ON WHOLE SCREEN:
            if (parentManager.debugMode)
            {
                sb.Begin();
                // ** DEBUG CENTER PIECE WITH X AND Y DRAWN TO SCREEN: **
                sb.Draw(this.parentManager.game.spr_cameraCrosshair, DEBUG_POS, Color.White);

                string textDebugX = "X: " + parentManager.game.cameraObject.ScreenToWorld(SCREEN_MID).X.ToString();
                string textDebugY = "Y: " + parentManager.game.cameraObject.ScreenToWorld(SCREEN_MID).Y.ToString();
                sb.DrawString(this.parentManager.game.ft_debugMedium, "Camera Position", DEBUG_TEXT_LINE1, Color.Black);
                sb.DrawString(this.parentManager.game.ft_debugSmall, textDebugX, DEBUG_TEXT_LINE2, Color.White);
                sb.DrawString(this.parentManager.game.ft_debugSmall, textDebugY, DEBUG_TEXT_LINE3, Color.White);

                sb.End();
            }

        }
    }





}
