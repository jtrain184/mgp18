using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Monogame_Party_2018 {

  // This is where all the global constants will go
  public static class Constants {
    public const int SCREEN_WIDTH = 800;
    public const int SCREEN_HEIGHT = 600;

    public const int SCREEN_CENTER_X = SCREEN_WIDTH / 2;
    public const int SCREEN_CENTER_Y = SCREEN_HEIGHT / 2;
  }




  public class GameStateManager {

    // List of states, we will loop over these:
    public List<State> states = new List<State>();

    // Shared by all:
    KeyboardState input;
    public EntityCounter eCounter = new EntityCounter(); // used to give each entity a unique number

    // References for easy access:
    public MonogameParty game;
    public SpriteBatch sb;

    public GameStateManager(MonogameParty game) {
      this.game = game;
    }



    // Update
    public void Update(GameTime gameTime) {

      input = Keyboard.GetState();
      bool inputSent = false;
      var num = states.Count - 1;

      // Loop through all states and update them!:
      while (num > 0) {

        // Start with topmost state:
        State s = states[num];

        // First (topmost) state is the 'toplayer'. Send ONLY it the input:
        if (!inputSent) {
          s.isTopLayer = true;
          inputSent = true;
        } else { s.isTopLayer = false; } // all other layers false (updates last frame if changed)

        // Only update if State is 'active' and not flagged for deletion:
        if (s.active && !s.flagForDeletion) { s.Update(gameTime, input); }

        // State is flagged for deletion, remove it now:
        if (s.flagForDeletion) { RemoveState(s); }
        --num;
      } // end while
    } // end UPDATE


    public void Draw(GameTime gameTime) {

      // Start at bottom of state list, draw bottom up
      foreach (State s in states) {

        // If visible, draw:
        if (s.visible) { s.Draw(gameTime); }
      } // end foreach
    } // end DRAW



    // TODO
    public void AddState(State state, int playerIndex) {
      states.Add(state);
    }


    // TODO
    public void RemoveState(State state) {
      states.Remove(state);
    }





  } // end class definition
} // end namespace
