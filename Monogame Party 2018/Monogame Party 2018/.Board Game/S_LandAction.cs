using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_LandAction : State
    {
        // Constructor
        public S_LandAction(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {

        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
            //DEBUG:
            parentManager.round.active = true;      //Make S_Round Active
            parentManager.round.playerIsPlaying = false;   // Allow S_Round to get next player
            this.flagForDeletion = true;
            Console.WriteLine("Performed land action code. Going back to S_Round");
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
