using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Monogame_Party_2018 {

  public class GameStateManager {


    // List of states, we will loop over these:
    List<State> states = new List<State>();
    List<State> statesToUpdate = new List<State>();

    SpriteBatch spriteBatch;
    SpriteFont font;

    Texture2D blankTexture;

    KeyboardState input;



    // Default spritebatch shared by all the states
    public SpriteBatch SpriteBatch {
      get { return spriteBatch; }
    }

    // font shared by all the states:
    public SpriteFont Font {
      get { return font; }
    }


    public void Update(GameTime gameTime) {

      // TODO
      // Read keyboard:
      input = Keyboard.GetState();

      // Make a copy of the current state list to avoid confusion
      // if one state removes other states, etc...
      statesToUpdate.Clear();
      foreach (State s in states)
        statesToUpdate.Add(s);


      // Loop through all states and update them!:
      bool inputSent = false;
      while (statesToUpdate.Count > 0) {

        // pop topmost state:
        State s = statesToUpdate[statesToUpdate.Count - 1];
        statesToUpdate.RemoveAt(statesToUpdate.Count - 1);

        // First (topmost) state is the 'toplayer'. Send ONLY it the input:
        if (!inputSent) {
          s.topLayer = true;
          inputSent = true;
        }

        // UPDATE:
        if (s.active) {
          s.Update(gameTime, input);
        }

        // State is flagged for deletion, remove it now:
        if (s.flagForDeletion) {
          RemoveState(s);
        }
        
      } // end while
    } // end UPDATE


    public void Draw(GameTime gameTime) {
      foreach (State s in states) {

        // If visible, draw:
        if (s.visible) {
          s.Draw(gameTime);
        }
      } // end foreach
    } // end DRAW



    // TODO
    public void AddState(State state, int playerIndex) {
      states.Add(state);
    }

    // TODO
    public void RemoveState(State state) {
      states.Remove(state);
      statesToUpdate.Remove(state);
    }





  } // end class definition
} // end namespace
