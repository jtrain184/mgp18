using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
namespace Monogame_Party_2018
{
    public class GameSetup : State
    {
        public GameOptions gameOptions;
        public int roundLimit;

        // constructor
        public GameSetup(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            gameOptions = creator.gameOptions;
            roundLimit = gameOptions.numRounds;

            // Build Map

            // Build Players

            // Build 
        }

        

    }
}
