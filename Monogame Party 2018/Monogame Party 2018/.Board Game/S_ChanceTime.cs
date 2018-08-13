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

    public enum condition {
      rightCoin10 = 0,
      rightCoin20,
      rightCoin30,

      leftCoin10,
      leftCoin20,
      leftCoin30,

      rightStar1,
      rightStar2,

      leftStar1,
      leftStar2,

      swapCoins,
      swapStars,

      bothLoseCoin10,
      bothLoseCoin20,
      bothLoseCoin30,
      bothLoseStar,

      bothGainCoin20,
      bothGainStar,

      MAX_CONDITIONS
    }

    public enum BlockType {
      character = 0,
      condition
    }

    // Member variables:
    S_LandAction landActionCreator; // used to go back to
    readonly Player p;
    int diceNum;
    int aiHitTime;
    int aiTimer;
    const int AI_HIT_TIMER_MIN = 140;
    const int AI_HIT_TIMER_MAX = 185;
    bool hit;

    bool finishTransition;
    int finishTimer;
    const int FINISH_TIMER_COMPLETE = 160;

    List<Player> playersToPick;

    Player leftPlayer;
    E_Meeple leftMeeple;
    bool leftMeepleMove = false;
    Vector2 leftMeepleStartPos;
    Vector2 leftMeepleEndPos;

    Player rightPlayer;
    E_Meeple rightMeeple;
    bool rightMeepleMove = false;
    Vector2 rightMeepleStartPos;
    Vector2 RightMeepleEndPos;

    condition chanceEvent;
    bool implementedEvent;

    const int BLOCK_SIZE = 192; // size of the block
    int BLOCK_LEFT_X = ((int)(MGP_Constants.SCREEN_WIDTH * 0.25)) - BLOCK_SIZE / 2;
    int BLOCK_MIDDLE_X = ((int)(MGP_Constants.SCREEN_WIDTH * 0.5)) - BLOCK_SIZE / 2;
    int BLOCK_RIGHT_X = ((int)(MGP_Constants.SCREEN_WIDTH * 0.75)) - BLOCK_SIZE / 2;
    int BLOCK_Y = 240;



    E_ChanceBlock leftBlock;
    E_ChanceBlock middleBlock;
    E_ChanceBlock rightBlock;


    // String drawing:
    Vector2 TITLE_POS = new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y / 5);
    Vector2 FINISH_POS = new Vector2(MGP_Constants.SCREEN_MID_X, ((int)(MGP_Constants.SCREEN_MID_Y*0.45f)));

    // Make music with SFX variables:
    const int DELAY_DRUM_START = 32;
    int musicCounter;
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
      this.diceNum = 0;
      this.hit = false;

      this.musicCounter = 0;
      this.delayDrum = DELAY_DRUM_START;
      this.delayString = this.delayDrum * 2;
      this.drumVolume = DRUM_ACCENT_VOLUME;
      this.suspenseLevel = 0.2f;
      this.highString = true;

      this.aiTimer = 0;
      this.aiHitTime = creator.random.Next(AI_HIT_TIMER_MIN, AI_HIT_TIMER_MAX);

      this.finishTransition = false;
      this.finishTimer = 0;

      // List of players that can be picked (starts with all):
      this.playersToPick = new List<Player>();
      foreach (Player p in creator.gameOptions.players) {
        this.playersToPick.Add(p);
      }

      implementedEvent = false; // the action is taken for the event chosen

      // Create the three blocks:
      leftBlock = new E_ChanceBlock(this, creator.game.spr_chanceBlock, BLOCK_LEFT_X, BLOCK_Y, BlockType.character, playersToPick);
      middleBlock = new E_ChanceBlock(this, creator.game.spr_chanceBlock, BLOCK_MIDDLE_X, BLOCK_Y, BlockType.condition, playersToPick);
      rightBlock = new E_ChanceBlock(this, creator.game.spr_chanceBlock, BLOCK_RIGHT_X, BLOCK_Y, BlockType.character, playersToPick);

      leftMeepleStartPos = new Vector2(-100f, 580f);
      leftMeepleEndPos = new Vector2((float)(MGP_Constants.SCREEN_WIDTH*0.25), 580f);

      rightMeepleStartPos = new Vector2(MGP_Constants.SCREEN_WIDTH + 100f, 580f);
      RightMeepleEndPos = new Vector2((float)(MGP_Constants.SCREEN_WIDTH*0.75), 580f);

    } // end constructor







    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);


      // DEBUG: Cancel out:
      if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.all)) {
        closeChanceTime();
      }


      // HUMAN CONTROLS:
      // Hit dices #1, #2, and #3
      if (km.ActionPressed(KeyboardManager.action.select, p.playerControlsIndex) && p.isHuman == true) {
        diceNum++;
        hit = true;
      } // end if pressed and human



      // AI SCRIPTING:
      if (!p.isHuman) {
        this.aiTimer++;
        if (aiTimer >= this.aiHitTime) {
          diceNum++; // Next die

          // reset timer and new timer window:
          this.aiHitTime = parentManager.random.Next(AI_HIT_TIMER_MIN, AI_HIT_TIMER_MAX);
          aiTimer = 0;
          hit = true;
        }
      }


      if (hit) {

        // LEFT die (player) --------------------------------------------
        if (diceNum == 1) {
          suspenseLevel += 0.2f;
          delayDrum = (int)(delayDrum*0.75);
          delayString = (int)(delayString*0.75);

          // left block chosen, other blocks faster now:
          leftBlock.hitBlock();
          leftBlock.newBlockColor(Color.Tan);
          leftPlayer = leftBlock.getCurrentPlayer(); // get the left chosen player
          leftBlock.newHighlightColor(leftPlayer.characterColor);
          middleBlock.increaseFaceChangeSpeed(0.6f);
          rightBlock.increaseFaceChangeSpeed(0.6f);
          rightBlock.removePlayerFace(leftPlayer); // REMOVE THIS PLAYER FROM RIGHT BLOCK

          // Create player meeple:
          leftMeeple = new E_Meeple(parentManager, leftMeepleStartPos, leftPlayer.type);
          leftMeeple.setPos(leftMeepleStartPos);
          leftMeepleMove = true;
        }

        // RIGHT die (player) --------------------------------------------
        else if (diceNum == 2) {
          suspenseLevel += 0.2f;
          delayDrum = (int)(delayDrum*0.75);
          delayString = (int)(delayString*0.75);

          rightBlock.hitBlock();
          rightBlock.newBlockColor(Color.Tan);
          rightPlayer = rightBlock.getCurrentPlayer(); // get the right chosen player
          rightBlock.newHighlightColor(rightPlayer.characterColor);
          middleBlock.increaseFaceChangeSpeed(0.6f);

          // Create player meeple:
          rightMeeple = new E_Meeple(parentManager, rightMeepleStartPos, rightPlayer.type);
          rightMeeple.setPos(rightMeepleStartPos);
          rightMeepleMove = true;
        }

        // MIDDLE die (condition) ----------------------------------------
        else {
          middleBlock.newBlockColor(Color.Tan);
          middleBlock.newHighlightColor(Color.Yellow);
          finishTransition = true;
          middleBlock.hitBlock();
          chanceEvent = middleBlock.getCurrentCondition(); // get the right chosen player
        }
        hit = false;
      } // end if hit


      // EASE the left meeple to his spot for shame:
      if (leftMeepleMove) {
        if (Vector2.Distance(leftMeeple.getPos(), leftMeepleEndPos) > 1.0F) {
          float newX = MGP_Tools.Ease(leftMeeple.getPos().X, leftMeepleEndPos.X, 0.15F);
          float newY = MGP_Tools.Ease(leftMeeple.getPos().Y, leftMeepleEndPos.Y, 0.15F);
          leftMeeple.setPos(new Vector2(newX, newY));
        }
      }

      // EASE the right meeple to his spot for shame:
      if (rightMeepleMove) {
        if (Vector2.Distance(rightMeeple.getPos(), RightMeepleEndPos) > 1.0F) {
          float newX = MGP_Tools.Ease(rightMeeple.getPos().X, RightMeepleEndPos.X, 0.15F);
          float newY = MGP_Tools.Ease(rightMeeple.getPos().Y, RightMeepleEndPos.Y, 0.15F);
          rightMeeple.setPos(new Vector2(newX, newY));
        }
      }


      // THE EVENT WAS CHOSEN, IT MUUUUST BE DONEEEE AHAHAHAHHAA:
      if (finishTransition && !implementedEvent) {

        int amount = 0;
        switch (chanceEvent) {

          // COINS
          case condition.leftCoin10:

            amount = 10;
            if (rightPlayer.coins < amount)
              amount = rightPlayer.coins;

            leftPlayer.coins += amount;
            leftPlayer.totalCoinsGained += amount;
            rightPlayer.coins = MGP_Tools.NonNegSub(rightPlayer.coins, amount);
            rightPlayer.totalCoinsLost += amount;
            break;

          case condition.leftCoin20:
            amount = 20;
            if (rightPlayer.coins < amount)
              amount = rightPlayer.coins;

            leftPlayer.coins += amount;
            leftPlayer.totalCoinsGained += amount;
            rightPlayer.coins = MGP_Tools.NonNegSub(rightPlayer.coins, amount);
            rightPlayer.totalCoinsLost += amount;
            break;

          case condition.leftCoin30:
            amount = 30;
            if (rightPlayer.coins < amount)
              amount = rightPlayer.coins;

            leftPlayer.coins += amount;
            leftPlayer.totalCoinsGained += amount;
            rightPlayer.coins = MGP_Tools.NonNegSub(rightPlayer.coins, amount);
            rightPlayer.totalCoinsLost += amount;
            break;

          case condition.rightCoin10:
            amount = 10;
            if (leftPlayer.coins < amount)
              amount = leftPlayer.coins;

            rightPlayer.coins += amount;
            rightPlayer.totalCoinsGained += amount;
            leftPlayer.coins = MGP_Tools.NonNegSub(leftPlayer.coins, amount);
            leftPlayer.totalCoinsLost += amount;
            break;

          case condition.rightCoin20:
            amount = 20;
            if (leftPlayer.coins < amount)
              amount = leftPlayer.coins;

            rightPlayer.coins += amount;
            rightPlayer.totalCoinsGained += amount;
            leftPlayer.coins = MGP_Tools.NonNegSub(leftPlayer.coins, amount);
            leftPlayer.totalCoinsLost += amount;
            break;

          case condition.rightCoin30:
            amount = 30;
            if (leftPlayer.coins < amount)
              amount = leftPlayer.coins;

            rightPlayer.coins += amount;
            rightPlayer.totalCoinsGained += amount;
            leftPlayer.coins = MGP_Tools.NonNegSub(leftPlayer.coins, amount);
            leftPlayer.totalCoinsLost += amount;
            break;

          // STARS
          case condition.rightStar1:
            amount = 1;
            if (leftPlayer.stars < amount)
              amount = leftPlayer.stars;

            leftPlayer.stars = MGP_Tools.NonNegSub(leftPlayer.stars, amount);
            rightPlayer.stars += amount;
            break;

          case condition.rightStar2:
            amount = 2;
            if (leftPlayer.stars < amount)
              amount = leftPlayer.stars;

            leftPlayer.stars = MGP_Tools.NonNegSub(leftPlayer.stars, amount);
            rightPlayer.stars += amount;
            break;

          case condition.leftStar1:
            amount = 1;
            if (rightPlayer.stars < amount)
              amount = rightPlayer.stars;

            rightPlayer.stars = MGP_Tools.NonNegSub(rightPlayer.stars, amount);
            leftPlayer.stars += amount;
            break;

          case condition.leftStar2:
            amount = 2;
            if (rightPlayer.stars < amount)
              amount = rightPlayer.stars;

            rightPlayer.stars = MGP_Tools.NonNegSub(rightPlayer.stars, amount);
            leftPlayer.stars += amount;
            break;

          // SWAPS:
          case condition.swapCoins:
            int leftCoins = leftPlayer.coins;
            int rightCoins = rightPlayer.coins;

            leftPlayer.coins = rightCoins;
            rightPlayer.coins = leftCoins;

            if (leftCoins < rightCoins)
              rightPlayer.totalCoinsLost += (rightCoins - leftCoins);
            else if (rightCoins < leftCoins)
              leftPlayer.totalCoinsLost += (leftCoins - rightCoins);
            break;

          case condition.swapStars:
            int leftStars = leftPlayer.stars;
            int rightStars = rightPlayer.stars;

            leftPlayer.stars = rightStars;
            rightPlayer.stars = leftStars;
            break;

          // BOTH LOSE:
          case condition.bothLoseCoin10:
            leftPlayer.coins = MGP_Tools.NonNegSub(leftPlayer.coins, 10);
            leftPlayer.totalCoinsLost += 10;
            rightPlayer.coins = MGP_Tools.NonNegSub(rightPlayer.coins, 10);
            rightPlayer.totalCoinsLost += 10;
            break;

          case condition.bothLoseCoin20:
            leftPlayer.coins = MGP_Tools.NonNegSub(leftPlayer.coins, 20);
            leftPlayer.totalCoinsLost += 20;
            rightPlayer.coins = MGP_Tools.NonNegSub(rightPlayer.coins, 20);
            rightPlayer.totalCoinsLost += 20;
            break;

          case condition.bothLoseCoin30:
            leftPlayer.coins = MGP_Tools.NonNegSub(leftPlayer.coins, 30);
            leftPlayer.totalCoinsLost += 30;
            rightPlayer.coins = MGP_Tools.NonNegSub(rightPlayer.coins, 30);
            rightPlayer.totalCoinsLost += 30;
            break;

          case condition.bothLoseStar:
            leftPlayer.stars = MGP_Tools.NonNegSub(leftPlayer.stars, 1);
            rightPlayer.stars = MGP_Tools.NonNegSub(rightPlayer.stars, 1);
            break;

          // BOTH WIN:
          case condition.bothGainCoin20:
            leftPlayer.coins += 20;
            leftPlayer.totalCoinsGained += 20;
            rightPlayer.coins += 20;
            rightPlayer.totalCoinsGained += 20;
            break;

          case condition.bothGainStar:
            leftPlayer.stars += 1;
            rightPlayer.stars += 1;
            break;

          default:
            Console.WriteLine("Error, default value in S_ChanceTime for implementedEvent");
            break;

        } // end switch

        implementedEvent = true;
      } // finish implementation


      // Give the player time to celebrate/grieve their gains/losses:
      if (finishTransition) {
        finishTimer++;
        if (finishTimer >= FINISH_TIMER_COMPLETE) { closeChanceTime(); }
      }


      // Update Dice Entities:
      if (!finishTransition) {
        leftBlock.Update(gameTime, ks);
        middleBlock.Update(gameTime, ks);
        rightBlock.Update(gameTime, ks);
      }




      // ------------------ Music designed algorithmically with SFX: --------------------------------
      if (!finishTransition) {
        musicCounter++;

        // Drum:
        if (musicCounter % delayDrum == 0)
          parentManager.audioEngine.playSound(MGP_Constants.soundEffects.chanceTimeDrum, drumVolume + suspenseLevel);

        // String:
        if (musicCounter % delayString == 0) {
          if (this.highString) {
            parentManager.audioEngine.playSound(MGP_Constants.soundEffects.chanceTimeHigh, 0.4f + suspenseLevel);
            this.highString = false;
          }
          else {
            parentManager.audioEngine.playSound(MGP_Constants.soundEffects.chanceTimeLow, 0.4f + suspenseLevel);
            this.highString = true;
          }
        }

        // change accent if needed:
        if (musicCounter % DRUM_ACCENT == 0)
          drumVolume = DRUM_ACCENT_VOLUME;
        else
          drumVolume = DRUM_NORMAL_VOLUME;
      }
    // ------------------------------------ END PROGRAMATIC MUSIC ------------------------------
    } // end update








    // Draw:
    public override void Draw(GameTime gameTime) {
      base.Draw(gameTime);


      SpriteBatch sb = this.parentManager.game.spriteBatch;


      sb.Begin();
      sb.Draw(this.parentManager.game.bg_chanceTime, new Vector2(xPos, yPos), Color.White);

      string t = "CHANCE TIME!";
      Vector2 boldTextPos = CenterString.getCenterStringVector(TITLE_POS, t, this.parentManager.game.ft_confirmPlayer_Bold);
      Vector2 boldTextPosB = new Vector2(boldTextPos.X + 4, boldTextPos.Y + 4);
      sb.DrawString(this.parentManager.game.ft_confirmPlayer_Bold, t, boldTextPosB, Color.Black);
      sb.DrawString(this.parentManager.game.ft_confirmPlayer_Bold, t, boldTextPos, Color.White);

      if (finishTransition) {
        string text = "FINISHED!";
        Vector2 finishPos = CenterString.getCenterStringVector(FINISH_POS, text, this.parentManager.game.ft_confirmPlayer_Bold);
        Vector2 finishPosB = new Vector2(finishPos.X + 4, finishPos.Y + 4);
        Vector2 finishPosBB = new Vector2(finishPos.X + 8, finishPos.Y + 8);
        sb.DrawString(this.parentManager.game.ft_confirmPlayer_Bold, text, finishPosBB, Color.Black);
        sb.DrawString(this.parentManager.game.ft_confirmPlayer_Bold, text, finishPosB, Color.SaddleBrown);
        sb.DrawString(this.parentManager.game.ft_confirmPlayer_Bold, text, finishPos, Color.Yellow);
      }

      sb.End();


      // Draw Dice Entities:
      leftBlock.Draw(gameTime);
      middleBlock.Draw(gameTime);
      rightBlock.Draw(gameTime);


      // Draw meeples
      if (leftMeepleMove)
        leftMeeple.Draw(gameTime);

      if (rightMeepleMove)
        rightMeeple.Draw(gameTime);
    }






    void closeChanceTime() {
      landActionCreator.active = true;
      this.flagForDeletion = true;
      parentManager.audioEngine.playNextSong(5);
    }


  }
}
