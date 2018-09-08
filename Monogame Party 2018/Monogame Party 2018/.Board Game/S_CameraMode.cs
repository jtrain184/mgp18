
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;


namespace Monogame_Party_2018
{
  public class S_CameraMode : State {
    public CameraProperties cameraProperties;
    const float speed = 14;
    S_Pause s_Pause;
    Vector2 oldCameraPos;


    public S_CameraMode(GameStateManager creator, float xPos, float yPos, S_Pause s_Pause) : base(creator, xPos, yPos) {
      cameraProperties = parentManager.boardGame.cameraProperties;
      // Pause the pause menu (lol)
      this.s_Pause = s_Pause;
      s_Pause.active = false;
      s_Pause.visible = false;

      // Hide other states so we see just the gameboard
      foreach (State s in parentManager.states) {
        if (!s.GetType().Equals(typeof(B_PirateBay)) && s != this) { s.visible = false; }
      }

      // Save where the camera was
      oldCameraPos = cameraProperties.getPos();
    }

    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);


      // Move Right
      if (parentManager.km.KeyDown(Keys.D)) {
        if (cameraProperties.getX() < MGP_Constants.BOARD_MAX_WIDTH)
          cameraProperties.incX(speed);
      }
      // Move Left
      if (parentManager.km.KeyDown(Keys.A)) {
        if (cameraProperties.getX() > MGP_Constants.BOARD_MIN_WIDTH)
          cameraProperties.incX(-speed);
      }
      // Move Up
      if (parentManager.km.KeyDown(Keys.W)) {
        if (cameraProperties.getY() > MGP_Constants.BOARD_MIN_HEIGHT)
          cameraProperties.incY(-speed);
      }
      // Move Down
      if (parentManager.km.KeyDown(Keys.S)) {
        if (cameraProperties.getY() < MGP_Constants.BOARD_MAX_HEIGHT)
          cameraProperties.incY(speed);
      }

      // Exit
      if ((parentManager.km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.all)) ||
          (parentManager.km.ActionPressed(KeyboardManager.action.pause, KeyboardManager.playerIndex.all)) ||
          (parentManager.km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.all)))

            {
                //Show all states again
                foreach (State s in parentManager.states)
                {
                    s.visible = true;
                }

                // move camera back to where it was
                cameraProperties.setPos(oldCameraPos);

                s_Pause.active = true;
                s_Pause.visible = true;
                this.flagForDeletion = true;
                sendDelay = 2;
            }

            // Camera is fixated on CameraProperties object:
            this.parentManager.game.cameraObject.LookAt(cameraProperties.getPos());
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);


            SpriteBatch sb = this.parentManager.game.spriteBatch;
            sb.Begin();

            // Draw the button instructions
            Texture2D moveControl = parentManager.game.keys_move;
            Vector2 moveControlPos = new Vector2(MGP_Constants.SCREEN_MID_X - 250, 475);
            sb.Draw(moveControl, moveControlPos, Color.White);

            String text = "            ... Move Camera\n\n\nselect    ... Return";
            Vector2 smTextPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, 575), text, parentManager.game.ft_rollDice_lg);
            sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X - 2, smTextPos.Y), Color.Black);
            sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X + 2, smTextPos.Y), Color.Black);
            sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y - 2), Color.Black);
            sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y + 2), Color.Black);

            sb.DrawString(parentManager.game.ft_rollDice_lg, text, smTextPos, Color.White);

            sb.End();
        }


    }
}
