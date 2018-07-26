using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Monogame_Party_2018 {
  public class S_PlayerUI : State {

    int boxWidth = 350;
    int boxHeight = 150;
    int screenPadding = 16;

    // Box Positions
    Vector2 topLeft;
    Vector2 topRight;
    Vector2 bottomLeft;
    Vector2 bottomRight;

    public S_PlayerUI(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos) {

      // Box Positions
      topLeft = new Vector2((float)screenPadding, 0 + screenPadding); // top left
      topRight = new Vector2(MGP_Constants.SCREEN_WIDTH - boxWidth - screenPadding, 0 + screenPadding);
      bottomLeft = new Vector2(0 + screenPadding, MGP_Constants.SCREEN_HEIGHT - boxHeight - screenPadding);
      bottomRight = new Vector2(MGP_Constants.SCREEN_WIDTH - boxWidth - screenPadding, MGP_Constants.SCREEN_HEIGHT - boxHeight - screenPadding);

    }



    // Update:
    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);
    }




    // Draw:
    public override void Draw(GameTime gameTime) {
      base.Draw(gameTime);

      SpriteBatch sb = this.parentManager.game.spriteBatch;
      sb.Begin();

      // Player One:
      drawPlayer(parentManager.gameOptions.players[0], topLeft, sb, Color.White);

      // Player Two:
      drawPlayer(parentManager.gameOptions.players[1], topRight, sb, Color.Red);

      // Player Three:
      drawPlayer(parentManager.gameOptions.players[2], bottomLeft, sb, Color.Blue);

      // Player Four:
      drawPlayer(parentManager.gameOptions.players[3], bottomRight, sb, Color.Green);

      sb.End();
    }





    void drawPlayer(Player p, Vector2 pos, SpriteBatch sb, Color c) {
      // Box:
      sb.Draw(this.parentManager.game.spr_playerBox, pos, c);
      sb.Draw(this.parentManager.game.spr_boxInner, pos, Color.White);

      // Frame:
      sb.Draw(this.parentManager.game.spr_playerBoxFrame, pos, Color.White);

      // Picture:
      sb.Draw(p.closeupPicture, pos, Color.White);

      // Coin Count:

      // Star Count:

      // Current Place:

    }

  }
}
