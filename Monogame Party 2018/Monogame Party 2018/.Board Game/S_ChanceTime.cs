using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {
  public class S_ChanceTime : State {

    // Member variables:
    S_LandAction landActionCreator; // used to go back to
    readonly Player p;


    // Constructor:
    public S_ChanceTime(GameStateManager creator, float xPos, float yPos, S_LandAction creatorState) : base(creator, xPos, yPos) {
      this.p = creator.round.currPlayer;
      this.landActionCreator = creatorState;
      this.landActionCreator.active = false;
    } // end constructor







    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);


      // If HUMAN:
      if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one) && p.isHuman == true) {



        // debug:
        landActionCreator.active = true;
        this.flagForDeletion = true;
      }



    }



    // Draw:
    public override void Draw(GameTime gameTime) {
      base.Draw(gameTime);


      SpriteBatch sb = this.parentManager.game.spriteBatch;


      sb.Begin();
      sb.Draw(this.parentManager.game.bg_chanceTime, new Vector2(xPos, yPos), Color.White);

      string t = "CHANCE TIME!";
      Vector2 boldTextPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y), t, this.parentManager.game.ft_confirmPlayer_Bold);
      sb.DrawString(this.parentManager.game.ft_confirmPlayer_Bold, t, boldTextPos, Color.White);

      sb.End();

    }



  }
}
