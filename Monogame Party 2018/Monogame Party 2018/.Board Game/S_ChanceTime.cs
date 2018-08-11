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

    // Make music with SFX variables:
    const int DELAY_DRUM_START = 32;
    int delayCounter;
    int delayDrum;
    const int DRUM_ACCENT = 4;
    float DRUM_ACCENT_VOLUME = 0.6f;
    float DRUM_NORMAL_VOLUME = 0.3f;
    float drumVolume;
    int delayString;
    float suspenseLevel;
    bool highString;

    // Constructor:
    public S_ChanceTime(GameStateManager creator, float xPos, float yPos, S_LandAction creatorState) : base(creator, xPos, yPos) {
      this.p = creator.round.currPlayer;
      this.landActionCreator = creatorState;
      this.landActionCreator.active = false;

      this.delayCounter = 0;
      this.delayDrum = DELAY_DRUM_START;
      this.delayString = this.delayDrum * 2;
      this.drumVolume = DRUM_ACCENT_VOLUME;
      this.suspenseLevel = 0.2f;
      this.highString = true;
    } // end constructor







    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);


      // If HUMAN:
      if ((km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one) && p.isHuman == true) ||
         (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.all))) {

        // debug:
        landActionCreator.active = true;
        this.flagForDeletion = true;
      }



      // ------------------ Music designed algorithmically with SFX: --------------------------------
      delayCounter++;

      // Drum:
      if (delayCounter % delayDrum == 0)
        parentManager.audioEngine.playSound(MGP_Constants.soundEffects.chanceTimeDrum, drumVolume + suspenseLevel);

      // String:
      if (delayCounter % delayString == 0) {
        if (this.highString) {
          parentManager.audioEngine.playSound(MGP_Constants.soundEffects.chanceTimeHigh, 0.5f + suspenseLevel);
          this.highString = false;
        }
        else {
          parentManager.audioEngine.playSound(MGP_Constants.soundEffects.chanceTimeLow, 0.5f + suspenseLevel);
          this.highString = true;
        }
      }

      // change accent if needed:
      if (delayCounter % DRUM_ACCENT == 0)
        drumVolume = DRUM_ACCENT_VOLUME;
      else
        drumVolume = DRUM_NORMAL_VOLUME;
    // ------------------------------------ END PROGRAMATIC MUSIC ------------------------------




    } // end update



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
