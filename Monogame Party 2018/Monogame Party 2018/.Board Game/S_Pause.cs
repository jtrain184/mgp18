using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {
  public class S_Pause : State {

    public enum pauseOptions {
      resume = 0,
      viewBoard,
      quit,
      exitProgram
    }

    // ** Member Variables **
    // 'player' member variable inherited from State
    public List<State> statesToPause;
    public KeyboardManager.playerIndex playerWhoPaused;

    // Pause menu items:
    public List<PauseItem> pauseItems;
    public const int piHeight = 64;
    public int currentSelection;

    // Selection glove:
    public E_Glove glove;
    public int gloveAdjustX = 64 + 8;

    // transparent black overlay:
    Rectangle overlay;
    Texture2D overlayTexture;

    Rectangle menuBox;
    Texture2D menuBoxTexture;

    Vector2 pauseTitlePos = new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - MGP_Constants.SCREEN_HEIGHT/3);
    Vector2 piPos;


    // ** CONSTRUCTOR **
    public S_Pause (GameStateManager creator, float xPos, float yPos, KeyboardManager.playerIndex playerWhoPaused) : base(creator, xPos, yPos) {

      this.playerWhoPaused = playerWhoPaused;

      // Center title:
      pauseTitlePos = CenterString.getCenterStringVector(pauseTitlePos, "Paused", this.parentManager.game.ft_mainMenuFont);

      // pause items:
      pauseItems = new List<PauseItem>();
      piPos = new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - MGP_Constants.SCREEN_HEIGHT / 4); // init position
      SpriteFont sf = parentManager.game.ft_mainMenuFont;
      int numItems = 0;
      Vector2 startPos = new Vector2(piPos.X, piPos.Y + (numItems * piHeight));
      PauseItem curItem = new PauseItem(startPos, "Resume", sf, S_Pause.pauseOptions.resume);
      pauseItems.Add(curItem);
      currentSelection = numItems; // first selection here
      numItems++;
      this.glove = new E_Glove(creator, new Vector2(curItem.screenPosCentered.X - gloveAdjustX, curItem.screenPosCentered.Y));

      curItem = new PauseItem(new Vector2(piPos.X, piPos.Y + (numItems*piHeight)), "View Board", sf, S_Pause.pauseOptions.viewBoard);
      pauseItems.Add(curItem);
      numItems++;

      curItem = new PauseItem(new Vector2(piPos.X, piPos.Y + (numItems*piHeight)), "Quit Match", sf, S_Pause.pauseOptions.quit);
      pauseItems.Add(curItem);
      numItems++;

      curItem = new PauseItem(new Vector2(piPos.X, piPos.Y + (numItems*piHeight)), "Exit Program", sf, S_Pause.pauseOptions.exitProgram);
      pauseItems.Add(curItem);
      numItems++;


      // Store all other States into a list of states that we will 'pause'
      statesToPause = new List<State>();
      foreach (State s in creator.states) {

        // only change states that are already active (don't tamper with other states)
        if (s.active == true)
          statesToPause.Add(s);
      }


      // Pause those selected states:
      foreach (State s in statesToPause) {
        if (s != this) // don't pause itself
          s.active = false;
      }


      // Create the transparent black background:
      overlayTexture = new Texture2D(parentManager.game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
      Color[] c = new Color[1];
      int transparencyAmount = (int)(255f*0.6f);
      c[0] = Color.FromNonPremultiplied(255, 255, 255, transparencyAmount);
      overlayTexture.SetData<Color>(c);

      // Create the menu background box:
      menuBoxTexture = new Texture2D(parentManager.game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
      transparencyAmount = (int)(255f*0.8f);
      c[0] = Color.FromNonPremultiplied(255, 255, 255, transparencyAmount);
      menuBoxTexture.SetData<Color>(c);

      // Create the shapes:
      overlay = new Rectangle(0, 0, MGP_Constants.SCREEN_WIDTH, MGP_Constants.SCREEN_HEIGHT);
      int BoxHeight = (int)(MGP_Constants.SCREEN_HEIGHT * 0.75f);
      int BoxWidth = (int)(MGP_Constants.SCREEN_WIDTH * 0.4f);
      Vector2 BoxPos = new Vector2(MGP_Constants.SCREEN_MID_X - BoxWidth/2, MGP_Constants.SCREEN_MID_Y - BoxHeight/2);
      menuBox = new Rectangle((int)BoxPos.X, (int)BoxPos.Y, BoxWidth, BoxHeight);
    }




    // ** UPDATE **
    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);



      // Update selection / glove position:
      // PRESS DOWN
      if (km.ActionPressed(KeyboardManager.action.down, this.playerWhoPaused)) {
        if (currentSelection < pauseItems.Count - 1) { currentSelection++; }
        else { currentSelection = 0; }

        // Update Glove pos:
        glove.pos = new Vector2(pauseItems[currentSelection].screenPosCentered.X - gloveAdjustX, pauseItems[currentSelection].screenPosCentered.Y);
      }

      // PRESS UP
      if (km.ActionPressed(KeyboardManager.action.up, this.playerWhoPaused)) {
        if (currentSelection > 0) { currentSelection--; }
        else { currentSelection = pauseItems.Count - 1; }

        // Update Glove pos:
        glove.pos = new Vector2(pauseItems[currentSelection].screenPosCentered.X - gloveAdjustX, pauseItems[currentSelection].screenPosCentered.Y);
      }

      // PRESS ENTER
      bool pressedResume = false; // false until proven true
      if (km.ActionPressed(KeyboardManager.action.select, this.playerWhoPaused)) {

        S_Pause.pauseOptions option = (S_Pause.pauseOptions)currentSelection;

        switch (option) {
          case S_Pause.pauseOptions.resume:
            pressedResume = true;
            break;

          case S_Pause.pauseOptions.viewBoard:
            // VIEW BOARD STATE HERE                << ------------- TODO TODO TODO
            break;

          case S_Pause.pauseOptions.quit:
            //parentManager.gameOptions.            << --------------- CLEAR GAME OPTIONS
            parentManager.clearStates();
            State newMenu = new S_MainMenu(parentManager, 0, 0);
            parentManager.AddStateQueue(newMenu);
            break;

          case S_Pause.pauseOptions.exitProgram:
            parentManager.game.Exit();
            break;

          default:
            break;
        }

      }




      // UNPAUSE:
      if ((km.ActionPressed(KeyboardManager.action.pause, this.playerWhoPaused)) ||
          (km.ActionPressed(KeyboardManager.action.cancel, this.playerWhoPaused)) ||
          (pressedResume)) {
        //List<State> statesToPause = new List<State>();

        Console.WriteLine("Player " + ((int)this.playerWhoPaused + 1).ToString() + " resumed the game");

        // Unpause other states now:
        foreach (State s in statesToPause) {
          // Unpause those states:
          s.active = true;
        }


        // Destroy pause menu state
        flagForDeletion = true;
        sendDelay = 2;
      } // end UNPAUSE (if press pause/cancel button)







    }





    // DRAW
    public override void Draw(GameTime gameTime) {
      base.Draw(gameTime);

      SpriteBatch sb = this.parentManager.game.spriteBatch;


      // Draw relative to the screen, regardless of camera position
      sb.Begin();

      // Draw transparent Black Background box:
      sb.Draw(overlayTexture, overlay, Color.Black);

      // Draw the Menu Box:
      sb.Draw(menuBoxTexture, menuBox, Color.Black);

      // Draw the "Paused" text
      sb.DrawString(parentManager.game.ft_mainMenuFont, "Paused", pauseTitlePos, Color.CornflowerBlue);

      // Draw pause menu items:
      int i = 0;
      Color c = Color.White;
      foreach (PauseItem pi in pauseItems) {
        if (i == currentSelection)
          c = Color.CornflowerBlue;
        else
          c = Color.White;

        sb.DrawString(pi.font, pi.text, pi.screenPosCentered, c);
        i++;
      }

      // Draw the glove:
      sb.Draw(glove.sprite, glove.pos, Color.White);



      sb.End();
    }






  }





}
