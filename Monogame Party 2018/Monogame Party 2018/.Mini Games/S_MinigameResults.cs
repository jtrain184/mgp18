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
        public List<MenuItem.Characters> results;
        // Constructor
        public S_MinigameResults(GameStateManager creator, float xPos, float yPos, List<MenuItem.Characters> results) : base(creator, xPos, yPos)
        {
            this.results = results;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            Console.WriteLine("Results of minigame: ");
            for(int i = 3; i >= 0; i--)
            {
                Console.WriteLine(Math.Abs(i - 4).ToString() + ". " + results[i]);
            }

            //DEBUG:
            parentManager.boardGame.active = true;      //Make S_Board Active
            this.flagForDeletion = true;
            Console.WriteLine("Performed minigame results. Going back to S_Board");
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
