using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    static class MGP_Tools
    {
        public static Vector2 Ease(Vector2 current, Vector2 target, float speed)
        {
            Vector2 moveDir = target - current; // get the direction we want to move in
            moveDir.Normalize();    // make movement only 1 at a time
            //Console.WriteLine("Move Direction: " + moveDir);
            return current + (moveDir * speed);
        }

        public static void Follow_Player(GameStateManager parentManager, Player currPlayer)
        {
            //move camera to current player
            parentManager.boardGame.cameraProperties.setPos(currPlayer.meeple.pos);

            // Make screen doesn't go off the board
            // Check too far right
            if (parentManager.boardGame.cameraProperties.getX() > MGP_Constants.BOARD_MAX_WIDTH)
                parentManager.boardGame.cameraProperties.setX(MGP_Constants.BOARD_MAX_WIDTH);
            // Check too far left
            if (parentManager.boardGame.cameraProperties.getX() < MGP_Constants.BOARD_MIN_WIDTH)
                parentManager.boardGame.cameraProperties.setX(MGP_Constants.BOARD_MIN_WIDTH);
            // Check t0o far down
            if (parentManager.boardGame.cameraProperties.getY() > MGP_Constants.BOARD_MAX_HEIGHT)
                parentManager.boardGame.cameraProperties.setY(MGP_Constants.BOARD_MAX_HEIGHT);
            // Check too far up
            if (parentManager.boardGame.cameraProperties.getY() < MGP_Constants.BOARD_MIN_HEIGHT)
                parentManager.boardGame.cameraProperties.setY(MGP_Constants.BOARD_MIN_HEIGHT);

            // Camera is fixated on CameraProperties object:
            parentManager.game.cameraObject.LookAt(parentManager.boardGame.cameraProperties.getPos());
        }

    }
}
