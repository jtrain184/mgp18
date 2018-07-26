using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
namespace Monogame_Party_2018
{
    public class S_MinigameResults : State
    {
        public List<Player> results;
        // Constructor
        public S_MinigameResults(GameStateManager creator, float xPos, float yPos, List<Player> results) : base(creator, xPos, yPos)
        {
            this.results = results;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // Wait till player presses enter to start the next round
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one))
            {
                // Begin next round:
                parentManager.round.currRound++;
                parentManager.round.playerIsPlaying = false;
                this.flagForDeletion = true;
                parentManager.round.active = true;
            }
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
