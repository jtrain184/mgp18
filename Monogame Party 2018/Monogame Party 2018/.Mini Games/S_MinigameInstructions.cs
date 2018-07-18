using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
namespace Monogame_Party_2018
{
    public class S_MinigameInstructions : State
    {
        // Constructor
        public S_MinigameInstructions(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {

        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            S_Minigame1 minigame1 = new S_Minigame1(parentManager, 0, 0, false);
            parentManager.AddStateQueue(minigame1);
            this.flagForDeletion = true;
            Console.WriteLine("Showed the minigame instructions. Now to play the minigame");
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
