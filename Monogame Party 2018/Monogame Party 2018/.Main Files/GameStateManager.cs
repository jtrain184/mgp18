
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame_Party_2018.Menu_Classes;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Monogame_Party_2018
{

    // This is where all the global constants will go
    public static class Constants
    {
        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 600;

        public const int SCREEN_CENTER_X = SCREEN_WIDTH / 2;
        public const int SCREEN_CENTER_Y = SCREEN_HEIGHT / 2;
    }


    public class GameStateManager
    {
        // State count for debugging
        private int stateCount;

        // List of states, we will loop over these:
        public List<State> states = new List<State>();
        public List<State> statesToCreate = new List<State>();

        // Shared by all:
        KeyboardState input;

        // References for easy access:
        public MonogameParty game;
        public SpriteBatch sb;
        public KeyboardManager km;
        public GameOptions gameOptions;
        

        // CONSTRUCTOR:
        public GameStateManager(MonogameParty game)
        {
            this.game = game;
            this.km = new KeyboardManager();
            this.gameOptions = new GameOptions();

            // Add the ** FIRST ** game state here:
            State mainMenu = new S_MainMenu(this, 0, 0);    // TODO, make eCounter static, NOT PASSED IN
            this.AddState(mainMenu, 0);
            this.stateCount = 1;
        }

        // Update
        public void Update(GameTime gameTime)
        {

            // Update the keyboard for BEGINNING of updates:
            km.KeysUpdateCurrent();

            //input = Keyboard.GetState();
            bool inputSent = false;
            var num = states.Count - 1;

            // Loop through all states and update them!:
            State s;
            while (num > -1)
            {

                // Start with topmost state:
                s = states[num];

                // First (topmost) state is the 'top layer'. Send ONLY it the input:
                if (!inputSent)
                {
                    s.isTopLayer = true;
                    inputSent = true;
                }
                else { s.isTopLayer = false; } // all other layers false (updates last frame if changed)

                // ** UPDATE ALL STATES **
                // Only update if State is 'active' and not flagged for deletion:
                if (s.active && !s.flagForDeletion) { s.Update(gameTime, input); }

                // State is flagged for deletion, remove it now:
                if (s.flagForDeletion) { RemoveState(s); }

                --num;
            } // end while


            // Create any new states?
            // Loop through states to create, creating and linking them to our states list
            foreach (State newState in statesToCreate.ToList())
            {
                states.Add(newState);
                statesToCreate.Remove(newState);
            }


            // Remove from statesToCreate list

            // Update New becomes Old states:
            km.KeysPushOld();

            // Log changes in state count
            if(states.Count != stateCount)
            {
                Console.WriteLine("Current State Count: " + states.Count + "\n");
                stateCount = states.Count;
            }

        } // end UPDATE


        public void Draw(GameTime gameTime)
        {

            // Start at bottom of state list, draw bottom up
            foreach (State s in states)
            {

                // If visible, draw:
                if (s.visible) { s.Draw(gameTime); }
            } // end foreach
        } // end DRAW



        // TODO
        public void AddState(State state, int playerIndex)
        {
            states.Add(state);
        }


        // TODO
        public void RemoveState(State state)
        {
            states.Remove(state);
        }


        public void AddStateQueue(State s)
        {
            statesToCreate.Add(s);
        }



    } // end class definition
} // end namespace
