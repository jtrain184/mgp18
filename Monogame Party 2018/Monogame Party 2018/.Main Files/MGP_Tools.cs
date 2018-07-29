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
        public static Vector2 EaseVector(Vector2 current, Vector2 target, float speed)
        {
            Vector2 moveDir = target - current; // get the direction we want to move in
            moveDir.Normalize();    // make movement only 1 at a time
            return current + (moveDir * speed);
        }

        public static float Ease(float currentVal, float targetVal, float speed) {
          // Speed suggested at 0.1
          return (currentVal + ((targetVal - currentVal) * speed));
        }




        public static void Follow_Player(GameStateManager parentManager, Player currPlayer)
        {
            // Move camera to current player
            parentManager.boardGame.cameraProperties.setPos(currPlayer.meeple.pos);

            // Make sure camera is still on the board
            MGP_Tools.KeepCameraOnBoard(parentManager);

        }

        public static void Assign_Star(S_Board board)
        {
            // Get index of next star based on where current star was
            int starIndex = board.spaces.FindIndex(x => x.type == Entity.typeSpace.star);
            

            // When creating game;
            if (starIndex < 0)
            {
                starIndex = 0;
                board.spaces[starIndex].prevType = Entity.typeSpace.blue;
            }
            
            // Randomly select a new postion for the star
            int nextStarIndex =  board.parentManager.random.Next(15, 30);

            // Change current star and new star
            board.spaces[starIndex].changeSpace(board.spaces[starIndex].prevType);
            board.spaces[(starIndex + nextStarIndex) % 40].changeSpace(Entity.typeSpace.star);
            

        }

        public static void KeepCameraOnBoard(GameStateManager parentManager)
        {
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

        public static void PanCameraFromStar(GameStateManager parentManager, E_Space currSpace, Player currPlayer){
           // TO DO







        }

    }
}
