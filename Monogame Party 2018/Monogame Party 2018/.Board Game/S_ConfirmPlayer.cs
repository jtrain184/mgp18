using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
namespace Monogame_Party_2018
{
    public class S_ConfirmPlayer : State
    {
        // Constructor
        public S_ConfirmPlayer(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {

        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            S_RollDice rollDice = new S_RollDice(parentManager, 0, 0);
            parentManager.AddStateQueue(rollDice);
            this.flagForDeletion = true;
            Console.WriteLine( parentManager.round.currPlayer.type + " confirmed its their turn and has chosen to roll the dice");
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
